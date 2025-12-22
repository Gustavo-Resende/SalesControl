using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesControl.Domain.ClientAggregate.Specifications
{
    public class GetClientByEmailExcludingIdSpec : Specification<Client>
    {
        public GetClientByEmailExcludingIdSpec(string email, Guid excludeId)
        {
            Query.Where(c => c.Email == email && c.Id != excludeId);
        }
    }
}
