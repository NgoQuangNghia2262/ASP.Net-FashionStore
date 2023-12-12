using Bussiness;
using Bussiness.Exceptions;
using Bussiness.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;
using src.Interface;

namespace src.Controllers
{
    [Route("/v1/data/product")]
    [ApiController]
    public class ProductController : ControllerBase, ICRUD<Product>
    {
        private IProduct_BUS bus;
        public ProductController(IProduct_BUS bus)
        {
            this.bus = bus;
        }

        [HttpGet]
        [Route("test")]
        public async Task<ActionResult> Test()
        {
            await bus.FindCategorys();
            return Ok(0);
        }

        [HttpGet]
        [Route("FindImgNamePriceProducts")]
        public async Task<ActionResult<ResponseResult<Product[]>>> FindImgNamePriceProducts(int PageSize, int PageNumber)
        {
            ResponseResult<Product[]> res = new ResponseResult<Product[]>();
            try
            {
                res = await bus.FindImgNamePriceProducts(PageSize, PageNumber);
                res.StatusCode = 200;
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<ResponseResult> Create(Product obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                Bussiness<Product>.Save(obj);
                result.StatusCode = 200;
            }
            catch (DuplicateDataException ex)
            {
                result.StatusCode = 21;
                result.Message = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                result.StatusCode = 400;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            catch (LengthPropertyException ex)
            {
                result.StatusCode = 32;
                result.Message = ex.Message;
                return BadRequest(result);
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
        public ActionResult<ResponseResult> Delete(Product obj)
        {
            ResponseResult res = new ResponseResult();
            try
            {
                Bussiness<Product>.Delete(obj);
                res.StatusCode = 200;
                res.Message = "Delete Successfully !!";
            }
            catch (LengthPropertyException ex)
            {
                res.StatusCode = 32;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Message = ex.Message;
                return BadRequest(res);

            }
            return Ok(res);
        }
        [HttpPost]
        [Route("find-one")]
        public async Task<ActionResult<ResponseResult<Product>>> FindOne(Product key)
        {
            ResponseResult<Product> result = new ResponseResult<Product>();
            try
            {
                result = await Bussiness<Product>.FindOne(key);
                result.StatusCode = 200;
            }
            catch (ArgumentException ex)
            {
                result.StatusCode = 400;
                result.Message = ex.Message;
                return BadRequest(result);

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
            return Ok(result);
        }
        [HttpPut]
        [Route("update")]
        public ActionResult<ResponseResult> Update(Product obj)
        {

            ResponseResult result = new ResponseResult();
            try
            {
                Bussiness<Product>.Save(obj);
                result.StatusCode = 200;
            }
            catch (ArgumentException ex)
            {
                result.StatusCode = 400;
                result.Message = ex.Message;
                return BadRequest(result);

            }
            catch (LengthPropertyException ex)
            {
                result.StatusCode = 32;
                result.Message = ex.Message;
                return BadRequest(result);

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
        public async Task<ActionResult<ResponseResult<Product[]>>> FindAll(int PageSize, int PageNumber)
        {
            ResponseResult<Product[]> result = new ResponseResult<Product[]>();
            try
            {
                result = await Bussiness<Product>.FindAll(PageSize, PageNumber);
                result.StatusCode = 200;
            }
            catch (DataNotFoundException ex)
            {
                result.StatusCode = 404;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            catch (ArgumentException ex)
            {
                result.StatusCode = 400;
                result.Message = ex.Message;
                return BadRequest(result);

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