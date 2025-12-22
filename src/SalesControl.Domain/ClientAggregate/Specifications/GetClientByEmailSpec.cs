using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesControl.Domain.ClientAggregate.Specifications
{
    public class GetClientByEmailSpec : Specification<Client>
    {
        public GetClientByEmailSpec(string email)
        {
            Query.Where(c => c.Email == email);
        }
    }
}
