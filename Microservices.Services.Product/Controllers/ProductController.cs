using AutoMapper;
using Microservices.Services.Product.Data;
using Microservices.Services.Product.Models.Dtos;
using Microservices.Services.Product.Models.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        protected ResponseDto _response;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _response = new();
            _mapper = mapper;
        }

      
        [HttpGet]
        public  IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();
                var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
                _response.Result = productsDto;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                _response.Message = "Produto não localizado.";
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            try
            {
                var productDto = _mapper.Map<ProductDto>(product);
                _response.Result = productDto;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            try
            {
                var productEntity = _mapper.Map<Models.Product>(productDto);
                var resultCreate = _context.Add(productEntity).Entity;
                await _context.SaveChangesAsync();

                _response.Result = resultCreate;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var productEntity = _mapper.Map<Models.Product>(productDto);
                var resultUpdate = _context.Update(productEntity).Entity;
                await _context.SaveChangesAsync();

                _response.Result = resultUpdate;
                
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    _response.Message = "Produto não localizado.";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }


                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
    }
}
