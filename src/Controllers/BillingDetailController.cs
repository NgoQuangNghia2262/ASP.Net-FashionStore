using Bussiness;
using Microsoft.AspNetCore.Mvc;
using Model;
using src.Interface;

namespace src.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class BillingDetailController : ControllerBase, ICRUD<BillingDetail>
    {
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ResponseResult>> Create(BillingDetail obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                await Bussiness<BillingDetail>.Save(obj);
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

        public Task<ActionResult<ResponseResult>> Delete(BillingDetail obj)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ResponseResult<BillingDetail[]>>> FindAll(int PageSize, int PageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ResponseResult<BillingDetail>>> FindOne(BillingDetail key)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ResponseResult>> Update(BillingDetail obj)
        {
            throw new NotImplementedException();
        }
    }
}