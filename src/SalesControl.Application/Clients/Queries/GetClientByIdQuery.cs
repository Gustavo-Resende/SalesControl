using Ardalis.Result;
using MediatR;
using SalesControl.Application.Clients.Dtos;
using SalesControl.Application.Clients.Extensions;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ClientAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SalesControl.Application.Clients.Queries
{
    public record GetClientByIdQuery(Guid Id) : SalesControl.Application.Common.IQuery<Result<ClientOutput>>;

    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Result<ClientOutput>>
    {
        private readonly IReadRepository<Client> _repository;
        public GetClientByIdQueryHandler(IReadRepository<Client> repository) => _repository = repository;

        public async Task<Result<ClientOutput>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<ClientOutput>.Error("Invalid client id.");

            var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (client is null)
                return Result<ClientOutput>.Error("Client not found.");

            return Result<ClientOutput>.Success(client.ToDto());
        }
    }
}
