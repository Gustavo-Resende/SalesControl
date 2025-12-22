using Ardalis.Result;
using MediatR;
using SalesControl.Application.Clients.Dtos;
using SalesControl.Application.Clients.Extensions;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ClientAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesControl.Application.Clients.Queries
{
    public record GetClientsListQuery : SalesControl.Application.Common.IQuery<Result<IReadOnlyCollection<ClientOutput>>>;

    public class GetClientsListQueryHandler : IRequestHandler<GetClientsListQuery, Result<IReadOnlyCollection<ClientOutput>>>
    {
        private readonly IReadRepository<Client> _repository;
        public GetClientsListQueryHandler(IReadRepository<Client> repository) => _repository = repository;

        public async Task<Result<IReadOnlyCollection<ClientOutput>>> Handle(GetClientsListQuery request, CancellationToken cancellationToken)
        {
            var clients = await _repository.ListAsync(cancellationToken: cancellationToken);
            var dtos = clients.Select(c => c.ToDto()).ToArray();
            return Result<IReadOnlyCollection<ClientOutput>>.Success(dtos);
        }
    }
}
