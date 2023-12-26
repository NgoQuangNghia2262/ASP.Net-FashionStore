namespace Bussiness.Interface
{
	public interface IAccount_BUS
	{
		void ChangePassword(Model.Account account, string? newPass);
		Task Regist(Model.Account account);
	}
}
