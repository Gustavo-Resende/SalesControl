using SalesControl.Application.Clients.Dtos;
using SalesControl.Domain.ClientAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesControl.Application.Clients.Extensions
{
    public static class ClientExtension
    {
        public static ClientOutput ToDto(this Client client)
            => new(client.Id, client.Name, client.Email, client.Phone);
    }
}
