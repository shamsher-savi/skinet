using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypesAndSpecifications : BaseSpecification<Product>
    {
        public ProductWithTypesAndSpecifications(ProductSpecParams productParams) :
        base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1), productParams.PageSize);
            
            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;

                    case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;

                    default:
                    AddOrderBy(n => n.Name);
                    break;
                }
            }
        }

        public ProductWithTypesAndSpecifications(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductBrand);
            AddIncludes(x => x.ProductType);
        }
    }
}