using E_Commerce.Model;
using E_Commerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IService<Category> _repo;
        public CategoryController(IService<Category> repo)
        {
            _repo = repo;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllCategorys()
        {
            var response = await _repo.GetAll();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var response = await _repo.GetById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category Category)
        {
            var response = await _repo.Create(Category);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, Category Category)
        {
            Category.Id = id; // Ensure the ID matches
            var response = await _repo.Update(id, Category);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
