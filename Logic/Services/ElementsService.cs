using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Crm.Logic.Models;
using Crm.Logic.Services.Interfaces;

namespace Crm.Logic.Services;

public class ElementsService : IElementsService
{
	private readonly ICrmElementRepository _elementsRepository;
	private readonly IProjectsRepository _projectsRepository;

	public ElementsService(
		ICrmElementRepository elementsRepository,
		IProjectsRepository projectsRepository)
	{
		_elementsRepository = elementsRepository;
		_projectsRepository = projectsRepository;
	}

	public async Task<ElementModel> GetElementByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);

		if (entity == null)
			throw new Exception($"������� � ID {id} �� ������.");

		return new ElementModel
		{
			Id = entity.Id,
			Json = entity.Json,
			LastModified = entity.LastModified
		};
	}

	public async Task<IReadOnlyCollection<ElementModel>> GetAllElementsAsync(CancellationToken cancellationToken)
	{
		var entities = await _elementsRepository.GetAllAsync(cancellationToken);

		return entities.Select(e => new ElementModel
		{
			Id = e.Id,
			Json = e.Json,
			LastModified = e.LastModified
		}).ToList();
	}

	public async Task<IReadOnlyCollection<ElementModel>> GetElementsByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
	{
		// 1. �������� ������, ����� ������ ID ��� ���������
		var project = await _projectsRepository.GetProjectByIdAsync(projectId, cancellationToken);
		if (project == null || project.Elements == null || !project.Elements.Any())
			return new List<ElementModel>();

		// 2. �������� ���� �������� �� ������ ID
		var entities = await _elementsRepository.GetByIdsAsync(project.Elements, cancellationToken);

		return entities.Select(e => new ElementModel
		{
			Id = e.Id,
			Json = e.Json,
			LastModified = e.LastModified
		}).ToList();
	}

	public async Task<ElementModel> CreateElementAsync(string? json, CancellationToken cancellationToken)
	{
		var entity = new CrmElementEntity
		{
			Id = Guid.NewGuid(),
			Json = json ?? "{}",
			LastModified = DateTime.UtcNow
		};

		await _elementsRepository.AddAsync(entity, cancellationToken);
		await _elementsRepository.SaveChangesAsync(cancellationToken);

		return new ElementModel
		{
			Id = entity.Id,
			Json = entity.Json,
			LastModified = entity.LastModified
		};
	}

	public async Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken)
	{
		var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);
		if (entity != null)
		{
			entity.Json = json;
			entity.LastModified = DateTime.UtcNow;

			_elementsRepository.Update(entity);
			await _elementsRepository.SaveChangesAsync(cancellationToken);
		}
	}

	public async Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken)
	{
		var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);
		if (entity == null)
			return false;

		await _elementsRepository.DeleteAsync(id, cancellationToken);
		await _elementsRepository.SaveChangesAsync(cancellationToken);

		return true;
	}

	public async Task DeleteAllElementsAsync(CancellationToken cancellationToken)
	{
		await _elementsRepository.DeleteAllAsync(cancellationToken);
	}
}