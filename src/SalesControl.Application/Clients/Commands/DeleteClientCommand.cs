using Ardalis.Result;
using MediatR;
using SalesControl.Application.Clients.Dtos;
using SalesControl.Application.Clients.Extensions;
using SalesControl.Application.Common;
using SalesControl.Application.Interfaces;
using SalesControl.Domain.ClientAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SalesControl.Application.Clients.Commands
{
    public record DeleteClientCommand(Guid Id) : ICommand<Result<bool>>;

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Result<bool>>
    {
        private readonly IRepository<Client> _repository;
        public DeleteClientCommandHandler(IRepository<Client> repository) => _repository = repository;

        public async Task<Result<bool>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return Result<bool>.Error("Invalid client id.");

            var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (client is null)
                return Result<bool>.Error("Client not found.");

            await _repository.DeleteAsync(client, cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
