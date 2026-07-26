using E_Commerce.Domain.Entites.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithIdSpecification : BaseSpecification<Product,int>
    {
        public ProductWithIdSpecification(HashSet<int> ProductIds):base(p=>ProductIds.Contains(p.Id))
        {

        }
    }
}
