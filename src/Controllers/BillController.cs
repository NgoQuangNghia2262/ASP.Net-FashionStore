using Bussiness;
using Bussiness.Exceptions;
using Bussiness.Helper;
using Bussiness.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using src.Interface;

namespace src.Controllers
{
    [Route("api/bill")]
    [ApiController]
    public class BillController : ControllerBase, ICRUD<Bill>
    {
        private readonly IBill_BUS bus;
        public BillController(IBill_BUS bus)
        {
            this.bus = bus;
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ResponseResult>> Create(Bill obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                await Bussiness<Bill>.Save(obj);
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
        [Route("delete")]
        public async Task<ActionResult<ResponseResult>> Delete(Bill obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                await Bussiness<Bill>.Delete(obj);
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
        [Route("find-all")]
        public async Task<ActionResult<ResponseResult<Bill[]>>> FindAll(int PageSize, int PageNumber)
        {
            ResponseResult<Bill[]> result = new ResponseResult<Bill[]>();
            try
            {
                result = await Bussiness<Bill>.FindAll(PageSize, PageNumber);
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
        [HttpPost]
        [Route("get-one")]
        public async Task<ActionResult<ResponseResult<Bill>>> FindOne(Bill key)
        {
            ResponseResult<Bill> result = new ResponseResult<Bill>();
            try
            {
                result = await Bussiness<Bill>.FindOne(key);
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
        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<ResponseResult>> Update(Bill obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                await Bussiness<Bill>.Save(obj);
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
        [Route("GetBillingDetails")]
        public async Task<ActionResult<ResponseResult<BillingDetail[]>>> GetBillingDetails(int idbill)
        {
            ResponseResult<BillingDetail[]> result = new ResponseResult<BillingDetail[]>();
            try
            {
                BillingDetail[] billingDetails = await bus.GetBillingDetails(idbill);
                result.Data = billingDetails;
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
    }
}