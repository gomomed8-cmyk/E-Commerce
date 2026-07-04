using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entites.Products;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings _urlSettings;

        public PictureUrlResolver(IOptions<UrlSettings> options)
        {
           _urlSettings = options.Value;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = _urlSettings.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{path}";
        }


        public class UrlSettings
        {
            public string BaseUrl { get; set; }
        }
    }
}
