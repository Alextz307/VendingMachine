using Microsoft.AspNetCore.Mvc;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain;

namespace Nagarro.VendingMachine.RestApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        [Route("product")]
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
                    
        [HttpGet]
        [Route("product/{id}")]
        public ActionResult GetById(int id)
        {
            Product? product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("product/{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (_productRepository.UpdateProduct(id, updatedProduct))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("product")]
        public ActionResult AddProduct([FromBody] Product newProduct)
        {
            if (_productRepository.GetById(newProduct.Id) != null)
            {
                return NotFound();
            }

            _productRepository.AddProduct(newProduct);
            return CreatedAtAction(nameof(AddProduct), new { id = newProduct.Id });
        }

        [HttpDelete]
        [Route("product/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            if (_productRepository.DeleteProduct(id))
            {
                return Ok();
            }
            
            return NotFound();
        }
    }
}
