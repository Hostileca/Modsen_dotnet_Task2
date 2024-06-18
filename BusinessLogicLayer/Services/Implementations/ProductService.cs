using BusinessLogicLayer.Dtos.Products;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;
//using AutoMapper;

namespace BusinessLogicLayer.Services.Implementations
{
    public class ProductService : IService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;
        //private readonly IMapper _mapper;


        public ProductService(IRepository<Product> productRepo, IRepository<Category> categoriesRepo/*, IMapper mapper*/)
        {
            _productRepo = productRepo;
            _categoryRepo = categoriesRepo;
            //_mapper = mapper;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            return null;
        }
        public async Task<ProductReadDto> GetByPredicateAsync(Expression<Func<ProductReadDto, bool>> predicate)
        {
            return null;
        }
        public async Task CreateItemAsync(ProductCreateDto itemCreateDto)
        {

        }
        public async Task UpdateItemAsync(ProductUpdateDto itemUpdateDto)
        {

        }
        public async Task DeleteItemAsync(Product item)
        {

        }
        public async Task SaveChangesAsync()
        {

        }
    }
}
