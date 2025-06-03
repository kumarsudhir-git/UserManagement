using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementServices.BO.Interfaces;

namespace UserManagementServices.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManagementBO _orderManagementBO;
        public OrdersController(IOrderManagementBO orderBO) 
        {
            _orderManagementBO = orderBO;
        }

        [Authorize]
        [HttpGet("api/status/{orderNumber}")]
        public async Task<IActionResult> GetOrderStatus(string orderNumber)
        {
            var status = await _orderManagementBO.GetOrderStatusAsync(orderNumber);
            return Ok(status);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var order = await _repository.GetByIdAsync(id);
        //    if (order == null)
        //        return NotFound();
        //    return Ok(order);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Order order)
        //{
        //    order.Id = Guid.NewGuid();
        //    await _repository.AddAsync(order);
        //    return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        //{
        //    if (id != order.Id)
        //        return BadRequest();

        //    await _repository.UpdateAsync(order);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _repository.DeleteAsync(id);
        //    return NoContent();
        //}
    }
}
