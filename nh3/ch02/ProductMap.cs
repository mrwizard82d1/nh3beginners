using FluentNHibernate.Mapping;

namespace ch02
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.UnitPrice);
            Map(x => x.ReorderLevel);
            Map(x => x.Discontinued);

            References(x => x.Category);
        }
    }
}
