using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.Order;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Domain.Dtos.UserDtos;
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
        private readonly ICategoryService _categoryService;


        public ProductService(IProductRepository productRepository, ICategoryService categoryService, IMapper mapper, ISanitizerService sanitizerService, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _sanitizerService = sanitizerService;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
        }


        //public async Task<ReadProductDto> CreateProductAsync(CreateProductDto createProductDto)
        //{
        //    try
        //    {
        //        var sanitizedDto = _sanitizerService.SanitizeDto(createProductDto);
        //        if (sanitizedDto == null)
        //        {
        //            throw new ArgumentNullException(nameof(createProductDto));
        //        }

        //        var productDtoProperties = typeof(CreateProductDto).GetProperties();
        //        foreach (var property in productDtoProperties)
        //        {
        //            var dtoValue = property.GetValue(sanitizedDto);
        //            if (property.Name.ToLower() == "imageurl")
        //            {
        //                Console.WriteLine($"{property.Name} : value is {dtoValue}");
        //            }
        //            if (dtoValue is null or (object)"")
        //            {
        //                throw new ArgumentException($"{property.Name} is required.");
        //            }
        //        }

        //        var category = await _categoryRepository.GetByNameAsync(sanitizedDto.CategoryName);
        //        if (category == null)
        //        {
        //            throw new ArgumentException($"Category with Name {sanitizedDto.CategoryName} not found.");
        //        }
        //        var newProduct = _mapper.Map<Product>(sanitizedDto);
        //        newProduct = await _productRepository.AddAsync(newProduct);

        //        //var category = await _categoryRepository.GetByIdAsync(newProduct.CategoryId);
        //        var readProductDto = _mapper.Map<ReadProductDto>(newProduct);
        //        readProductDto.Category = _mapper.Map<ReadCategoryDto>(category);
        //        return readProductDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Mapping error: " + ex.Message);

        //        if (ex.InnerException != null)
        //        {
        //            Console.WriteLine("Inner exception: " + ex.InnerException.Message);
        //        }
        //        throw;
        //    }

        //}
        public async Task<bool> DeleteProductAsync(Guid productId)
        {

            var result = await _productRepository.DeleteByIdAsync(productId);
            return result;
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

                var category = await _categoryRepository.GetByNameAsync(sanitizedDto.CategoryName);
                if (category == null)
                {
                    throw new ArgumentException($"Category with Name {sanitizedDto.CategoryName} not found.");
                }

                var newProduct = _mapper.Map<Product>(sanitizedDto);
                newProduct.CategoryId = category.Id; // Utilisation de l'ID de la catégorie trouvée

                newProduct = await _productRepository.AddAsync(newProduct);

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

        public async Task<IEnumerable<ReadProductDto>> GetProductByCategoryIdAsync(Guid categoryId)
        {
            Console.WriteLine("Category ID received in service layer.");
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (category == null)
            {
                throw new ArgumentException($"Category with ID {categoryId} not found.");
            }

            var productsInCategory = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            var readProductDtos = _mapper.Map<IEnumerable<ReadProductDto>>(productsInCategory);

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

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            return await _productRepository.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string name)
        {
            // Récupérer la catégorie par son nom
            var category = await _categoryRepository.GetByNameAsync(name);

            if (category == null)
            {
                throw new ArgumentException($"Category with name {name} not found.");
            }

            // Récupérer les produits de cette catégorie
            var productsInCategory = await _productRepository.GetProductsByCategoryIdAsync(category.Id);

            return productsInCategory;
        }


        public async Task<ReadProductDto> UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(productId)
                    ?? throw new ArgumentException($"No Product with ID `{productId}` was found.");

                // Mise à jour des propriétés du produit avec les données du DTO
                existingProduct.Name = updateProductDto.Name;
                existingProduct.Description = updateProductDto.Description;
                existingProduct.Price = updateProductDto.Price;
                existingProduct.CategoryId = updateProductDto.CategoryId; // Mise à jour de l'ID de la catégorie

                // Mettez à jour le produit dans le repository
                var updatedProduct = await _productRepository.UpdateAsync(productId, existingProduct);

                // Mapper les données mises à jour vers le DTO de lecture
                var readProductDto = _mapper.Map<ReadProductDto>(updatedProduct);

                return readProductDto;
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                throw new ApplicationException("An error occurred while updating the product.", ex);
            }
        }

    }
}
