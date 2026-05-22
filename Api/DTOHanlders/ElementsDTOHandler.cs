using Crm.Api.Controllers.Elements.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;

namespace Crm.Api.DTOHanlders;

public class ElementsDTOHandler : IElementsDTOHandler
{
    //_

    public Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ElementResponse> CreateElementAsync(string? json, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAllElementsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ElementListResponse> GetAllElementsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ElementResponse> GetElementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ElementListResponse> GetElementsByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
