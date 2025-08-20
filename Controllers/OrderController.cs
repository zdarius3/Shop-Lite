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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderService.AddOrderAsync(orderDTO);
            return Ok(order);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderService.UpdateOrderAsync(orderDTO);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        [HttpPut("/AddOrderItem")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddOrderItemToOrder(int orderId,
            [FromBody] CreateOrderItemDTO oiDTO)
        {
            var updatedOrder = await _orderService.AddOrderItemToOrderAsync(orderId, oiDTO);
            return Ok(updatedOrder);
        }

        [HttpPut("/DeleteOrderItem")]
        [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderItemFromOrder(int orderId, int orderItemId)
        {
            var updatedOrder = await _orderService.DeleteOrderItemFromOrderAsync(orderId, orderItemId);
            return Ok(updatedOrder);
        }
    }
}