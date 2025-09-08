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
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService buyerService;

        public BuyerController(IBuyerService buyerService)
        {
            this.buyerService = buyerService;
        }

        [Authorize(Roles = "BUYER")]
        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrderAsync(AddOrderDto orderDto)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int buyerId))
                throw new Exception("Bad ID. Logout and login.");

            await buyerService.AddOrderAsync(orderDto, buyerId);

            return Ok();
        }

        [Authorize(Roles = "BUYER")]
        [HttpPost("cancel-order/{orderId}")]
        public async Task<ActionResult> CancelOrderAsync(int orderId)
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int buyerId))
                throw new Exception("Bad ID. Logout and login.");

            await buyerService.CancelOrderAsync(orderId, buyerId);

            return Ok();
        }

        [Authorize(Roles = "BUYER")]
        [HttpGet("get-old-orders")]
        public async Task<ActionResult> GetOldOrdersAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int buyerId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetOrderDto> orders = await buyerService.GetOldOrdersAsync(buyerId);

            return Ok(orders);
        }

        [Authorize(Roles = "BUYER")]
        [HttpGet("get-new-orders")]
        public async Task<ActionResult> GetNewOrdersAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int buyerId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetOrderDto> orders = await buyerService.GetNewOrdersAsync(buyerId);

            return Ok(orders);
        }

        [Authorize(Roles = "BUYER")]
        [HttpGet("get-products")]
        public async Task<ActionResult> GetProductsAsync()
        {
            List<GetProductDto> products = await buyerService.GetAllProductsAsync();

            return Ok(products);
        }
    }
}
