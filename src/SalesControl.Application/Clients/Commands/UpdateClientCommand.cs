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
    public record UpdateClientCommand(Guid Id, string Name, string Email, string Phone) : ICommand<Result<ClientOutput>>;

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Result<ClientOutput>>
    {
        private readonly IRepository<Client> _repository;
        public UpdateClientCommandHandler(IRepository<Client> repository) => _repository = repository;

        public async Task<Result<ClientOutput>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<ClientOutput>.Error("Invalid client id.");
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<ClientOutput>.Error("Name is required.");
            if (string.IsNullOrWhiteSpace(request.Email))
                return Result<ClientOutput>.Error("Email is required.");

            var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (client is null)
                return Result<ClientOutput>.Error("Client not found.");

            var normalizedEmail = request.Email.Trim().ToLowerInvariant();
            // check duplicate email excluding current id
            var dupSpec = new GetClientByEmailExcludingIdSpec(normalizedEmail, request.Id);
            var other = await _repository.FirstOrDefaultAsync(dupSpec, cancellationToken);
            if (other is not null)
                return Result<ClientOutput>.Error("Another client with same email already exists.");

            client.UpdateContact(request.Name.Trim(), request.Email.Trim(), string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim());

            await _repository.UpdateAsync(client, cancellationToken);

            return Result<ClientOutput>.Success(client.ToDto());
        }
    }
}
