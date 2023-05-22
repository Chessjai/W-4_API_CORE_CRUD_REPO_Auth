using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W_4_API_CORE_CRUD_REPO.Repository;

namespace W_4_API_CORE_CRUD_REPO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly Iproduct _iproduct;

        public ProductController(Iproduct productRepo)

        {

            _iproduct = productRepo;

        }


       

        [HttpGet]

        public async Task<IEnumerable<Product>> GetProducts()

        {

            return await _iproduct.Get();

        }

        [HttpGet("{ProductId}")]

        public async Task<ActionResult<Product>> GetProducts(int ProductId)

        {

            return await _iproduct.Get(ProductId);

        }


        [HttpPost]

        public async Task<ActionResult<Product>> PostProducts([FromBody] Product product)

        {

            var newProduct = await _iproduct.Create(product);

            return CreatedAtAction(nameof(GetProducts), new { EmpId = newProduct.ProductId }, newProduct);

        }

     

        [HttpPut("{ProductId}")]

        public async Task<ActionResult> PutProducts(int ProductId, [FromBody] Product product)

        {


            if (ProductId != product.ProductId)

            {

                return BadRequest();

            }

            await _iproduct.Update(product);

            return NoContent();

        }


        [HttpDelete("{ProductId}")]

        public async Task<ActionResult> Delete(int ProductId)

        {

            var productToDelete = await _iproduct.Get(ProductId);

            

            if (productToDelete == null)

                return NotFound();

            await _iproduct.Delete(productToDelete.ProductId);

            return NoContent();

        }

    }

}

