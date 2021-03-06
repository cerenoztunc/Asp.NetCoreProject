using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.DTOs;
using Project.API.Filters;
using Project.Core.Models;
using Project.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return Ok(_mapper.Map<CategoryDto>(category));
        }
        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(new Category
            {
                Name = categoryDto.Name
            });
            return Created(string.Empty,_mapper.Map<CategoryDto>(category));
        }
        [HttpPut]
        public IActionResult Update(CategoryDto categoryDto)
        {
            var updatedCategory = _categoryService.Update(_mapper.Map<Category>(categoryDto));
            return NoContent();
        }
        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteToCategory = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(deleteToCategory);
            return NoContent();
        }
        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProductsById(int id)
        {
            var category = await _categoryService.GetWithProductsByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductDto>(category));

        }
    }
}
