using Bussiness.Exceptions;
using Bussiness.Helper;
using Bussiness.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
namespace src.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer_BUS bus;
        public CustomerController(ICustomer_BUS bus)
        {
            this.bus = bus;
        }
        [HttpPost]
        [Route("PlacingAnOrder")]
        public ActionResult<ResponseResult> PlacingAnOrder([FromBody] string note)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                bus.PlacingAnOrder(HttpContext, note);
                result.StatusCode = 200;
            }

            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("RemoveProductsFromCart")]
        public ActionResult<ResponseResult> RemoveProductsFromCart(BillingDetail detail)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                bus.RemoveProductsFromCart(HttpContext, detail);
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("getCartForCustomer")]
        public async Task<ActionResult<ResponseResult<BillingDetail[]>>> GetCartForCustomer()
        {
            ResponseResult<BillingDetail[]> result = new ResponseResult<BillingDetail[]>();
            try
            {
                BillingDetail[] billingDetails = await bus.GetCartForCustomer(HttpContext);
                result.Data = billingDetails;
                result.StatusCode = 200;
                return Ok(result);
            }
            catch (DataNotFoundException ex)
            {
                result.StatusCode = 404;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
                return BadRequest(result);
            }


        }
        [HttpPost]
        [Route("purchase")]
        public ActionResult<ResponseResult> Purchase(BillingDetail billing)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                bus.Purchase(HttpContext, billing);
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            result.StatusCode = 200;
            return Ok(result);
        }
    }
}