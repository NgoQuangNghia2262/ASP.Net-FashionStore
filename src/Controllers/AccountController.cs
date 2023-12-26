using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Interface;
using src.Interface;
using Bussiness;
using Microsoft.Extensions.Configuration;
using Bussiness.Exceptions;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Bussiness.Interface;

namespace src.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase, ICRUD<Account>
    {
        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            Bussiness<Account>.Test();
            return Ok(0);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ResponseResult>> Create(Account obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                await Bussiness<Account>.Save(obj);
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
        public async Task<ActionResult<ResponseResult>> Delete(Account obj)
        {
            ResponseResult res = new ResponseResult();
            try
            {
                await Bussiness<Account>.Delete(obj);
                res.StatusCode = 200;
                res.Message = "Delete Successfully !!";
            }
            // catch (ArgumentNullException ex) (InvalidCastException ex){
            //     res.StatusCode = 400;
            //     res.Message = ex.Message;
            // }
            catch (LengthPropertyException ex)
            {
                res.StatusCode = 32;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (DataConstraintViolationException ex)
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
        [HttpGet]
        [Route("find-all")]
        public async Task<ActionResult<ResponseResult<Account[]>>> FindAll(int PageSize, int PageNumber)
        {
            ResponseResult<Account[]> result = new ResponseResult<Account[]>();
            try
            {
                result = await Bussiness<Account>.FindAll(PageSize, PageNumber);
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

        [HttpPost]
        [Route("get-one")]
        public async Task<ActionResult<ResponseResult<Account>>> FindOne(Account key)
        {
            ResponseResult<Account> result = new ResponseResult<Account>();
            try
            {
                result = await Bussiness<Account>.FindOne(key);
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
        public async Task<ActionResult<ResponseResult>> Update(Account obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                await Bussiness<Account>.Save(obj);
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

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ResponseResult>> Login(Account account)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                Bussiness.Interface.IAuthentication authen = new Account_BUS();
                await authen.LoginAsync(HttpContext, account);
                result.StatusCode = 200;
                result.Message = "Successfully !!";
            }
            catch (LengthPropertyException ex)
            {
                result.StatusCode = 32;
                result.Message = ex.Message;
                return BadRequest(result);

            }
            catch (InvalidCredentialsException ex)
            {
                result.StatusCode = 15;
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
        [Route("auth")]
        // public async Task<ActionResult<ResponseResult>> Auth()
        // {
        //     ResponseResult result = new ResponseResult();
        //     try
        //     {
        //         Bussiness.Interface.IAuthentication authen = new Account_BUS();
        //         await authen.AdminAuth(HttpContext);
        //         result.StatusCode = 200;
        //         result.Message = "Successfully !!";
        //         return Ok(result);
        //     }
        //     catch (NotAuthenticated ex)
        //     {
        //         result.StatusCode = 16;
        //         result.Message = ex.Message;
        //         return BadRequest(result);

        //     }
        //     catch (SecurityTokenSignatureKeyNotFoundException ex)
        //     {
        //         result.StatusCode = 41;
        //         result.Message = ex.Message;
        //         return BadRequest(result);

        //     }
        //     catch (Exception ex)
        //     {
        //         result.StatusCode = 500;
        //         result.Message = ex.Message;
        //         return BadRequest(result);

        //     }
        // }
        [HttpGet]
        [Route("log-out")]
        public ActionResult<ResponseResult> LogOut()
        {
            ResponseResult result = new ResponseResult();
            try
            {
                Bussiness.Interface.IAuthentication authen = new Account_BUS();
                authen.Logout(Response);
                result.StatusCode = 200;
                result.Message = "Successfully !!";
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
        [Route("change-password")]
        public ActionResult<ResponseResult> ChangePasswrod(object obj)
        {
            ResponseResult res = new ResponseResult();
            try
            {
                JsonDocument jsonDocument = JsonDocument.Parse(obj.ToString() ?? "");
                JsonElement root = jsonDocument.RootElement;
                string? username = "";
                string? pass = "";
                string? per = "";
                string? newpass = "";
                if (root.TryGetProperty("username", out JsonElement nameElement))
                {
                    username = nameElement.GetString();
                }
                if (root.TryGetProperty("password", out JsonElement passElement))
                {
                    pass = passElement.GetString();
                }
                if (root.TryGetProperty("permissions", out JsonElement perElement))
                {
                    per = perElement.GetString();
                }
                if (root.TryGetProperty("newpass", out JsonElement newpassElement))
                {
                    newpass = newpassElement.GetString();
                }
                Account ac = new Account(username ?? "", pass, per);
                Bussiness.Interface.IAccount_BUS bus = new Account_BUS();
                bus.ChangePassword(ac, newpass);
                res.StatusCode = 200;
                res.Message = "Change Password Successfully !!";
            }
            catch (DataNotFoundException ex)
            {
                res.StatusCode = 404;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            catch (InvalidCredentialsException ex)
            {
                res.StatusCode = 15;
                res.Message = ex.Message;
                return BadRequest(res);
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
        [Route("regist")]
        public async Task<ActionResult<ResponseResult>> Regist(Account account)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                IAccount_BUS bus = new Account_BUS();
                await bus.Regist(account);
                result.StatusCode = 200;
                result.Message = "Successfully !!!";
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
            catch (DuplicateDataException ex)
            {
                result.StatusCode = 21;
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
