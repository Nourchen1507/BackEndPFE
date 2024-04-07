using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
   public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ISanitizerService _sanitizerService;
        private readonly ICategoryRepository _categoryRepository;


        public ProductService(IProductRepository productRepository, IMapper mapper, ISanitizerService sanitizerService, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _sanitizerService = sanitizerService;
            _categoryRepository = categoryRepository;
        }
      




        public async Task<ReadProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            try
            {
                var sanitizedDto = _sanitizerService.SanitizeDto(createProductDto);
                if (sanitizedDto == null)
                {
                    throw new ArgumentNullException(nameof(createProductDto));
                }

                var productDtoProperties = typeof(CreateProductDto).GetProperties();
                foreach (var property in productDtoProperties)
                {
                    var dtoValue = property.GetValue(sanitizedDto);
                    if (property.Name.ToLower() == "imageurl")
                    {
                        Console.WriteLine($"{property.Name} : value is {dtoValue}");
                    }
                    if (dtoValue is null or (object)"")
                    {
                        throw new ArgumentException($"{property.Name} is required.");
                    }
                }

                var category = await _categoryRepository.GetByIdAsync(sanitizedDto.CategoryId);
                if (category == null)
                {
                    throw new ArgumentException($"Category with ID {sanitizedDto.CategoryId} not found.");
                }
                var newProduct = _mapper.Map<Product>(sanitizedDto);
                newProduct = await _productRepository.AddAsync(newProduct);

                // var category = await _categoryRepository.GetByIdAsync(newProduct.CategoryId);
                var readProductDto = _mapper.Map<ReadProductDto>(newProduct);
                readProductDto.Category = _mapper.Map<ReadCategoryDto>(category);
                return readProductDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mapping error: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
                throw;
            }

        }


        public async Task<bool> DeleteProductByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return await _productRepository.DeleteByIdAsync(product.Id);
        }

        public async Task<IEnumerable<ReadProductDto>> GetAllProductsAsync(QueryOptions queryOptions)
        {
            var products = await _productRepository.GetAllAsync();
            var readProductDtos = new List<ReadProductDto>();

            foreach (var product in products)
            {
                var readProductDto = _mapper.Map<ReadProductDto>(product);

                var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                readProductDto.Category = _mapper.Map<ReadCategoryDto>(category);
                readProductDtos.Add(readProductDto);
            }
            return readProductDtos;
        }

        public async Task<ReadProductDto> GetProductByIdAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId)
                ?? throw new ArgumentException($"Product with ID {productId} not found.");
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var readProductDto = _mapper.Map<ReadProductDto>(product);
            readProductDto.Category = _mapper.Map<ReadCategoryDto>(category);
            return readProductDto;
        }

       













}
}
