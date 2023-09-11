using AutoMapper;
using NLayered.Core.DTOs;
using NLayered.Core.Repositories;
using NLayered.Core.Services;
using NLayered.Core.UnitOfWorks;
using NLayered.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService // bu class ismini DI module eklemesin diye değiştirdik, çünkü bunun cache li modelini module manuel ekleiyoruz
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWitCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);

        }
    }
}
