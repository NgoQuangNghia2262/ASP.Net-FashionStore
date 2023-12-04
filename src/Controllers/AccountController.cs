﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Interface;
using src.Interface;
using Bussiness;
using Microsoft.Extensions.Configuration;
using Bussiness.Exceptions;
using System.Security.Principal;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase , ICRUD<Account>
    {
        [HttpPost]
        [Route("create")]
        public ActionResult<ResponseResult> Create(Account obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                Bussiness<Account>.Save(obj);
                result.StatusCode = 200;
            }
            catch (ArgumentNullException ex)
            {
                result.StatusCode = 400;
                result.Message = ex.Message;
            }
            catch (InvalidAccountException)
            {
                result.StatusCode = 32;
                result.Message = "Account truyền vào không hợp lệ (Ví dụ username có độ dài phải > 2 và < 15)";

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
        public ActionResult<ResponseResult> Delete(Account obj)
        {
<<<<<<< Updated upstream
            throw new NotImplementedException();
=======
            ResponseResult res = new ResponseResult();
            try{
                Bussiness<Account>.Delete(obj);
                res.StatusCode = 200;
                res.Message = "Delete Successfully !!";
            }
            // catch(ArgumentNullException ex) or (InvalidCastException ex){
            //     res.StatusCode = 400;
            //     res.Message = ex.Message;
            // }
            catch(InvalidAccountException ex){
                res.StatusCode = 32;
                res.Message = ex.Message;
                return BadRequest(res);

            }
            catch (Exception ex){
                res.StatusCode = 500;
                res.Message = ex.Message;
                return BadRequest(res);

            }
            return Ok(res);
>>>>>>> Stashed changes
        }
        [HttpGet]
        [Route("get-all")]
        public ActionResult<ResponseResult<Account[]>> FindAll()
        {
            ResponseResult<Account[]> result = new ResponseResult<Account[]>();
            try
            {
                Account[] accounts = Bussiness<Account>.FindAll();
                result.StatusCode = 200;
                result.Data = accounts;
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
        public ActionResult<ResponseResult<Account>> FindOne(Account key)
        {
            ResponseResult<Account> result = new ResponseResult<Account>();
            try
            {
                Account account = Bussiness<Account>.FindOne(key);
                result.StatusCode = 200;
                result.Data = account;
            }
            catch (ArgumentException ex)
            {
                result.StatusCode = 400;
                result.Data = null;
                result.Message = ex.Message;
                return BadRequest(result);

            }
            catch (DataNotFoundException ex)
            {
                result.StatusCode = 404;
                result.Data = null;
                result.Message = ex.Message;
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Data = null;
                result.Message = ex.Message;
                return BadRequest(result);

            }
            return Ok(result);
        }
        [HttpPut]
        [Route("update")]
        public ActionResult<ResponseResult> Update(Account obj)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                Bussiness<Account>.Save(obj);
                result.StatusCode = 200;
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
        [Route("login")]
        public ActionResult<ResponseResult> Login(Account account)
        {
            ResponseResult result = new ResponseResult();
            try
            {
               
                Bussiness.Interface.IAuthentication authen = new Account_BUS();
                authen.Login(HttpContext , account);
                result.StatusCode = 200;
                result.Message = "Successfully !!";
            }catch(Exception ex) {
                result.StatusCode = 500;
                result.Message = ex.Message;
                return BadRequest(result);

            }
            return Ok(result);
        }
        [HttpGet]
        [Route("auth")]
        public ActionResult<ResponseResult> Auth()
        {
            ResponseResult result = new ResponseResult();
            try
            {
                Bussiness.Interface.IAuthentication authen = new Account_BUS();
                authen.AdminAuth(HttpContext);
                result.StatusCode = 200;
                result.Message = "Successfully !!";
                return Ok(result);
            }
            catch (NotAuthenticated ex)
            {
                result.StatusCode = 16;
                result.Message = ex.Message;
                return BadRequest(result);

            }
<<<<<<< Updated upstream
=======
            catch (SecurityTokenSignatureKeyNotFoundException ex)
            {
                result.StatusCode = 41;
                result.Message = ex.Message;
                return BadRequest(result);

            }
>>>>>>> Stashed changes
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Message = ex.Message;
                return BadRequest(result);

            }
        }
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

    }
}