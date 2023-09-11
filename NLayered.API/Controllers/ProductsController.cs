using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayered.API.Filters;
using NLayered.Core;
using NLayered.Core.DTOs;
using NLayered.Core.Services;

namespace NLayered.API.Controllers
{
    // [ValidateFilterAttribute] // validate filter'i böyle te tek controllerlara eklemek yerine Program.cs'de global ekledik.
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;

        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            //_service = service;
            _productService = productService;
        }


        /// GET api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        //[HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {

            return CreateActionResult(await _productService.GetProductsWithCategory());
        }


        /// GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList()); ///cliente productDTO döneceğiz
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos)); //hem ok, hem de 200 dönmemek için CustomBaseControllerda bir dönüş belirledik.
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        // GET /api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            //böyle bir null kontrolünü id ihtiyacı olan her yerde yapıyorsak remove getbyid vs, bu kod tekrarıdır ve iyi değildir. Burada data servis katmanı üzerindne çağrıldığından, servis katmaında null kontrolü yapmak daha doğru olur.
            //if(product == null) // bu kontrolü filter ile yapacağız.
            //{
            //    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "No product with this id"));
            //}

            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateRequestDto productCreateRequestDto)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(productCreateRequestDto));
            var productCreateResponseDto = _mapper.Map<ProductCreateResponseDto>(product);
            return CreateActionResult(CustomResponseDto<ProductCreateResponseDto>.Success(201, productCreateResponseDto));
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(productDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
