using Microsoft.AspNetCore.Mvc;
using ShopLite.Data;
using ShopLite.Entities;
using ShopLite.Services;
using ShopLite.Interfaces;
using ShopLite.DTOs;

namespace ShopLite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService,
                IOrderService orderService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderItemDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderItemDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            return Ok(orderItem);
        }
    }
}