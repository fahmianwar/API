namespace API.Repository.Interface
{
    interface IAccountRepository
    {
        int Login(int nik, string password);
        int CreatePassword(int nik, string password);
        int ChangePassword(int nik, string password);

    }
}
