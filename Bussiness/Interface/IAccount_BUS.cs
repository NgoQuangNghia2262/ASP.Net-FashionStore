using Microsoft.AspNetCore.Http;
using Model;

namespace Bussiness.Interface
{
	public interface IAccount_BUS
	{
		void ChangePassword(Account account, string? newPass);
		Task Regist(Model.Account account, HttpContext context);
		Account GetLoggedInUser(HttpContext context);

	}
}
