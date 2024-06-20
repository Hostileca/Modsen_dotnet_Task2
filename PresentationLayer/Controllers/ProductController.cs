﻿using BusinessLogicLayer.Dtos.Products;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        //[HttpGet("search/{name}")]
        //public async Task<IActionResult> GetProductsByName(string name)
        //{
        //    var products = await _productService.GetProductByPredicateAsync(product => product.Name == name);
        //    return Ok(products);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = await _productService.CreateProductAsync(productCreateDto);
            return Ok(product);
        }

        [HttpPut("{productUpdateDto.Id}")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var product = await _productService.UpdateProductAsync(productUpdateDto);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var deletedProduct = await _productService.DeleteProductByIdAsync(id);
            return Ok(deletedProduct);
        }
    }
}
