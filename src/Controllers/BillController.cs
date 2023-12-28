using Bussiness;
using Microsoft.AspNetCore.Mvc;
using Model;
using src.Interface;

namespace src.Controllers
{
    [Route("api/bill")]
    [ApiController]
    public class BillController : ControllerBase, ICRUD<Bill>
    {
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

        public Task<ActionResult<ResponseResult>> Delete(Bill obj)
        {
            throw new NotImplementedException();
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

        public Task<ActionResult<ResponseResult<Bill>>> FindOne(Bill key)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ResponseResult>> Update(Bill obj)
        {
            throw new NotImplementedException();
        }
    }
}