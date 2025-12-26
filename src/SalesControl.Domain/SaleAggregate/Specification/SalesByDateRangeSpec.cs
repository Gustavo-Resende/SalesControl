using Ardalis.Specification;

namespace SalesControl.Domain.SaleAggregate.Specification
{
    public class SalesByDateRangeSpec : Specification<Sale>
    {
        public SalesByDateRangeSpec(DateTime dataInicio, DateTime dataFim)
        {
            Query.Where(s => s.CreatedAt >= dataInicio && s.CreatedAt <= dataFim);
        }
    }
}
