using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndSpecifications : BaseSpecification<Product>
    {
        public ProductWithTypesAndSpecifications()
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
        }

        public ProductWithTypesAndSpecifications(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
        }
    }
}