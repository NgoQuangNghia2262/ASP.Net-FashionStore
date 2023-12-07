using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Interface;

namespace src.Interface
{
    public interface ICRUD<T> where T : IKey
    {
        ActionResult<ResponseResult<T[]>> FindAll(int PageSize, int PageNumber);
        ActionResult<ResponseResult<T>> FindOne(T key);
        ActionResult<ResponseResult> Create(T obj);
        ActionResult<ResponseResult> Update(T obj);
        ActionResult<ResponseResult> Delete(T obj);

    }
}
