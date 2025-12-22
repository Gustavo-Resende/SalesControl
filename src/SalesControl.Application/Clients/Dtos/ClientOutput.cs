using System;
using System.Collections.Generic;
using System.Text;

namespace SalesControl.Application.Clients.Dtos
{
    public record ClientOutput(
        Guid Id,
        string Name,
        string Email,
        string Phone
    );
}
