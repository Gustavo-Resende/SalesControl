using Ardalis.Result;
using Ardalis.Specification;
using MediatR;
using SalesControl.Application.Clients.Dtos;
using SalesControl.Application.Clients.Extensions;
using SalesControl.Application.Common;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ClientAggregate;
using SalesControl.Domain.ClientAggregate.Specifications;
using System.Threading;
using System.Threading.Tasks;

namespace SalesControl.Application.Clients.Commands
{
    public record RegisterClientCommand(string Name, string Email, string Phone) : ICommand<Result<ClientOutput>>;

    public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Result<ClientOutput>>
    {
        private readonly IRepository<Client> _repository;
        public RegisterClientCommandHandler(IRepository<Client> repository)
        {
            _repository = repository;
        }

        public async Task<Result<ClientOutput>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<ClientOutput>.Error("Name is required.");
            if (string.IsNullOrWhiteSpace(request.Email))
                return Result<ClientOutput>.Error("Email is required.");

            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var spec = new GetClientByEmailSpec(normalizedEmail);
            var existing = await _repository.FirstOrDefaultAsync(spec, cancellationToken);
            if (existing is not null)
                return Result<ClientOutput>.Error("A client with the same email already exists.");

            var client = new Client(request.Name.Trim(), request.Email.Trim(), string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim());

            await _repository.AddAsync(client, cancellationToken);

            var clientOutput = client.ToDto();
            return Result<ClientOutput>.Success(clientOutput);
        }
    }
}
