using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? SearchValue { get; set; }
        public ProductSortingOptions Sort { get; set; }
        public int pageIndex { get; set; } = 1;
       
        private const int DefultPageSize = 5;
        private const int MaxPageSize = 10;

        private int pageSize = DefultPageSize;
        public int PageSize
        {
            get => pageSize;

            set=> pageSize= value>MaxPageSize?MaxPageSize:(value<1 ? DefultPageSize : value);

        }
    }
}
