using Application.Dtos;
using Application.Orders.Commands.CreateOrder;
using Application.Orders.Commands.DeleteOrder;
using Application.Orders.Queries.GetOrder;
using Application.Orders.Queries.ListOrders;
using Domain.Errors;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.API.Requests;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender _sender;
        
        public OrderController(ISender sender)
        {
            _sender = sender;
        }
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAll()
        {
            var result = await _sender.Send(new GetOrdersQuery());

            return Ok(result.Value);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid orderId)
        {
            var query =new GetOrderByIdQuery(orderId);

            var result = await _sender.Send(query);

            if (!result.IsSuccess)
                return GetFailureResponse(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var command = new CreateOrderCommand(request.Adapt<OrderDto>());

            var result= await _sender.Send(command);

            if (!result.IsSuccess)
                return GetFailureResponse(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var command = new DeleteOrderCommand(orderId);

            var result = await _sender.Send(command);

            if (!result.IsSuccess)
                return GetFailureResponse(result.Error);

            return NoContent();
        }

        private ObjectResult GetFailureResponse(Error error)
        {
            return error.Code switch
            {
                "ORDER_NOT_FOUND" => NotFound(error),
                _ => StatusCode(500, error)
            };
        }
    }
}
