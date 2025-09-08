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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-all-orders")]
        public async Task<ActionResult> GetAllOrderAsync()
        {
            if (!int.TryParse(User.Claims.First(c => c.Type == "Id").Value, out int sellerId))
                throw new Exception("Bad ID. Logout and login.");

            List<GetOrderDto>? orders = await adminService.GetAllOrdersAsync();

            return Ok(orders);
        }
    }
}
