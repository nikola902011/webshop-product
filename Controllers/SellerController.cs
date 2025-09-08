using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsControl.DTO;
using ProductsControl.Interfaces;
using System.Data;

namespace ProductsControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService sellerService;

        public SellerController(ISellerService sellerService)
        {
            this.sellerService = sellerService;
        }

        [Authorize(Roles = "SELLER")]
        [HttpPost("add-product")]
        public async Task<ActionResult> AddProductAsync([FromForm] AddProductDto addProductDto)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            await sellerService.AddProductAsync(addProductDto, sellerId);

            return Ok();
        }

        [Authorize(Roles = "SELLER")]
        [HttpDelete("remove-product/{productId}")]
        public async Task<ActionResult> RemoveProductAsync(int productId)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            await sellerService.RemoveProductAsync(productId, sellerId);

            return Ok();
        }

        [Authorize(Roles = "SELLER")]
        [HttpGet("get-product/{id}")]
        public async Task<ActionResult> GetProductAsync(int id)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            GetProductDto product = await sellerService.GetProductAsync(id, sellerId);

            return Ok(product);
        }

        [Authorize(Roles = "SELLER")]
        [HttpGet("get-products")]
        public async Task<ActionResult> GetProductsAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetProductDto> products = await sellerService.GetProductsAsync(sellerId);

            return Ok(products);
        }

        [Authorize(Roles = "SELLER")]
        [HttpPut("update-product")]
        public async Task<ActionResult> UpdateProductAsync([FromForm] UpdateProductDto updateProductDto)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            await sellerService.UpdateProductAsync(updateProductDto, sellerId);

            return Ok();
        }

        [Authorize(Roles = "SELLER")]
        [HttpGet("get-old-orders")]
        public async Task<ActionResult> GetOldOrdersAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetOrderDto> orders = await sellerService.GetOldOrdersAsync(sellerId);

            return Ok(orders);
        }

        [Authorize(Roles = "SELLER")]
        [HttpGet("get-new-orders")]
        public async Task<ActionResult> GetNewOrdersAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetOrderDto> orders = await sellerService.GetNewOrdersAsync(sellerId);

            return Ok(orders);
        }
    }
}
