using E_Commerce.Context;
using E_Commerce.Model;
using E_Commerce.Service;
using E_Commerce.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IService<Brand> _repo;
        public BrandController(IService<Brand> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await _repo.GetAll();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(Guid id)
        {
            var response = await _repo.GetById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(Brand Brand)
        {
            var response = await _repo.Create(Brand);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(Guid id, Brand Brand)
        {
            Brand.Id = id; // Ensure the ID matches
            var response = await _repo.Update(id,Brand);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var response = await _repo.Delete(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
