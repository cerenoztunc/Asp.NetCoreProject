using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Models;
using Project.Core.Services;
using Project.MVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class ProductController : Controller
    {   
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            foreach (var item in products)
            {
                item.Category = await _categoryService.GetByIdAsync(item.CategoryId);
            }
            
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(new ProductDto
            {
                Categories = categories
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var newProduct = await _productService.AddAsync(_mapper.Map<Product>(productDto));

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;
            var categories = _categoryService.GetAllAsync().Result;
            var articleDto = _mapper.Map<ProductDto>(product);
            articleDto.Categories = categories;
            return View(articleDto);
        }
        [HttpPost]
        public IActionResult Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            var updatedProduct = _productService.Update(product);
            return RedirectToAction("Index");
        }

    }
}
