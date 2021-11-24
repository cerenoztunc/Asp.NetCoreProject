using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.Core.Services;
using Project.MVC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Filters
{
    public class CategoryNotFoundFilter:ActionFilterAttribute
    {
        private readonly ICategoryService _categoryService;

        public CategoryNotFoundFilter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = (int) context.ActionArguments.Values.FirstOrDefault();
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null) await next();
            else
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Errors.Add($"{id} id'li kategori bulunamadı..");
                context.Result = new RedirectToActionResult("Error", "Home",errorDto);
            }
        }
    }
}
