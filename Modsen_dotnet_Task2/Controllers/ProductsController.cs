using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using BusinessLogicLayer.Dtos.Product;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto> _productsService;
        public ProductsController(IService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto> productsService)
        {
            _productsService = productsService;
        }

    }
}
