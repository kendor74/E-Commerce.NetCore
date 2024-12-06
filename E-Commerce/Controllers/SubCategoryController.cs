using E_Commerce.Model;
using E_Commerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {

        private readonly IService<SubCategory> _repo;
        private readonly IService<Category> _categoryRepo;

        public SubCategoryController(IService<SubCategory> repo, IService<Category> categoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllSubCategorys()
        {
            var response = await _repo.GetAll();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(Guid id)
        {
            var response = await _repo.GetById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(SubCategory SubCategory)
        {
            var categoryResponse = await _categoryRepo.Exists(SubCategory.CategoryId);
            if (categoryResponse.StatusCode != StatusCodes.Status200OK)
            {
                return NotFound(categoryResponse.Message);
            }
            var response = await _repo.Create(SubCategory);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(Guid id, SubCategory SubCategory)
        {
            var categoryResponse = await _categoryRepo.Exists(SubCategory.CategoryId);
            if (categoryResponse.StatusCode != StatusCodes.Status200OK)
            {
                return NotFound(categoryResponse.Message);
            }

            var response = await _repo.Update(id, SubCategory);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
