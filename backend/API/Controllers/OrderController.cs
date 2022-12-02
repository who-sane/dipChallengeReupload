using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Classes;

namespace API
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderHandler _OrderHandler = new OrderHandler();
        // list orders
        [HttpGet]
        [EnableCors("MyPolicy")]
        [Route("/order")]
        public IEnumerable<Order> Test()
        {
            return _OrderHandler.GetOrders();
        }
        // get total 
        [HttpGet]
        [EnableCors("MyPolicy")]
        [Route("/order/findTotal")]
        public float findTotal([FromBody]Order order)
        {
            return _OrderHandler.findTotal(order);
        }

        [HttpGet]
        [EnableCors("MyPolicy")]
        [Route("/order/getGST")]
        public float getGST([FromBody]Order order)
        {
            return _OrderHandler.getGST(order);
        }

        // update
        [HttpPut]
        [EnableCors("MyPolicy")]
        [Route("/order")]
        public float AddOrder([FromBody]Order order)
        {
            return _OrderHandler.AddOrder(order);
        }

        // delete
        [HttpDelete]
        [EnableCors("MyPolicy")]
        [Route("/order")]
        public float Delete([FromBody]Order order)
        {
            return _OrderHandler.Delete(order);
        }


        
    }
}