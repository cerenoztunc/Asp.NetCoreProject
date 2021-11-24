using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Models;
using Project.Core.Services;
using Project.MVC.ApiServices;
using Project.MVC.DTOs;
using Project.MVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper, CategoryApiService categoryApiService)
        {
            _mapper = mapper;
            _categoryApiService = categoryApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiService.GetAllAsync();
            return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryApiService.AddAsync(categoryDto);
            return RedirectToAction("Index");
        }
        [ServiceFilter(typeof(CategoryNotFoundFilter))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryApiService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(category));
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await _categoryApiService.Update(categoryDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryApiService.Remove(id);
            return RedirectToAction("Index");
        }
        
    }
}
