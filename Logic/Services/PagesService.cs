using Crm.Data.Repositories.Interfaces;
using Crm.Infrastructure.Hubs;
using Crm.Logic.Models;
using Crm.Logic.Services.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Crm.Logic.Services
{
    public class PagesService : IPagesService
    {
        private readonly IPagesRepository _pagesRepo;
        private readonly IHubContext<CrmConstructorHub> _hub;

        public PagesService(IPagesRepository repo, IHubContext<CrmConstructorHub> hub)
        {
            _pagesRepo = repo;
            _hub = hub;
        }

        public async Task<PageModel> CreateAsync(Guid projectId, string name, CancellationToken cancellationToken)
        {
            Guid pageId = Guid.NewGuid();
            var page = await _pagesRepo.CreateAsync(
                pageId,
                projectId,
                name,
                DateTime.UtcNow,
                cancellationToken);
            await _pagesRepo.SaveChangesAsync(cancellationToken);

            await _hub.Clients.All.SendAsync("CreatePage", pageId.ToString(), cancellationToken);

            return new(
                page.Id,
                page.Name,
                page.CreatedAt,
                page.ProjectId
                );
        }

        public async Task<IReadOnlyCollection<PageModel>> CreatePagesAsync(
            Guid projectId,
            IReadOnlyList<string> names,
            CancellationToken cancellationToken)
        {
            var entities = names.Select((name, i) => new PageEntity()
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow,
                ProjectId = projectId
            });

            foreach (var e in entities)
            {
                await _pagesRepo.CreateAsync(e.Id, e.ProjectId, e.Name, e.CreatedAt, cancellationToken);
                await _hub.Clients.All.SendAsync("CreatePage", e.Id, cancellationToken);
            }
            await _pagesRepo.SaveChangesAsync(cancellationToken);

            return [.. entities.Select(z => new PageModel(z.Id, z.Name, z.CreatedAt, z.ProjectId))];
        }

        public async Task DeleteAllAsync(CancellationToken cancellationToken)
        {
            await _pagesRepo.DeleteAllAsync(cancellationToken);
            await _hub.Clients.All.SendAsync("DeleteAllPages", cancellationToken);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            await _pagesRepo.DeleteByIdAsync(id, cancellationToken);
            await _hub.Clients.All.SendAsync("DeletePage", id.ToString(), cancellationToken);
        }

        public async Task<IReadOnlyCollection<PageModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var pages = await _pagesRepo.GetAllAsync(cancellationToken);
            return pages
                .Select(z => new PageModel(z.Id, z.Name, z.CreatedAt, z.ProjectId))
                .ToArray();
        }

        public async Task<PageModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var page = await _pagesRepo.GetByIdAsync(id, cancellationToken)
                ?? throw new Exception($"Page with ID {id} is not found.");

            return new(page.Id, page.Name, page.CreatedAt, page.ProjectId);
        }

        public async Task<IReadOnlyCollection<PageModel>?> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
        {
            var pages = await _pagesRepo.GetByProjectIdAsync(projectId, cancellationToken);
            return pages
                .Select(z => new PageModel(z.Id, z.Name, z.CreatedAt, z.ProjectId))
                .ToList();
        }

        // TODO Возможно здесь дописать взаимодействие с хабом.
        public async Task ChangeNameAsync(Guid pageId, string newName, CancellationToken cancellationToken)
        {
            await _pagesRepo.ChangeNameAsync(pageId, newName, cancellationToken);
            await _hub.Clients.All.SendAsync("RenamePage", pageId.ToString(), newName, cancellationToken);
        }
    }
}
