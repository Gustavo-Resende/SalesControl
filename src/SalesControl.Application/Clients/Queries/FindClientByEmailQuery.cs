using Ardalis.Result;
using MediatR;
using SalesControl.Application.Clients.Dtos;
using SalesControl.Application.Clients.Extensions;
using SalesControl.Application.Common;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ClientAggregate;
using SalesControl.Domain.ClientAggregate.Specifications;

namespace SalesControl.Application.Clients.Queries
{
    public record FindClientByEmailQuery(string Email) : IQuery<Result<ClientOutput?>>;

    public class FindClientByEmailQueryHandler : IRequestHandler<FindClientByEmailQuery, Result<ClientOutput?>>
    {
        private readonly IReadRepository<Client> _repository;
        public FindClientByEmailQueryHandler(IReadRepository<Client> repository) => _repository = repository;

        public async Task<Result<ClientOutput?>> Handle(FindClientByEmailQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return Result<ClientOutput?>.Error("Email is required.");

            var normalizedEmail = request.Email.Trim();

            var spec = new GetClientByEmailSpec(normalizedEmail);
            var client = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

            if (client is null)
                return Result<ClientOutput?>.Success(null);

            return Result<ClientOutput?>.Success(client.ToDto());
        }
    }
}
