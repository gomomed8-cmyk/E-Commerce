using E_Commerce.Application.Common;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entites.Products;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandSpec : BaseSpecification<Product, int>
    {

        public ProductWithTypeAndBrandSpec(ProductQueryParams queryParams) :
            base(p=>(!queryParams.BrandId.HasValue || p.BrandId== queryParams.BrandId.Value)
            &&(!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || (p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch(queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderDescBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderDescBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderDescBy(p=>p.Id);
                    break;
            }
            ApplyPaging(queryParams.PageSize,queryParams.pageIndex);
        }
        public ProductWithTypeAndBrandSpec(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
