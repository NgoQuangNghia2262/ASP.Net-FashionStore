using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Interface;

namespace src.Interface
{
    public interface ICRUD<T> where T : IKey
    {
        Task<ActionResult<ResponseResult<T[]>>> FindAll(int PageSize, int PageNumber);
        Task<ActionResult<ResponseResult<T>>> FindOne(T key);
        Task<ActionResult<ResponseResult>> Create(T obj);
        Task<ActionResult<ResponseResult>> Update(T obj);
        Task<ActionResult<ResponseResult>> Delete(T obj);

    }
}
