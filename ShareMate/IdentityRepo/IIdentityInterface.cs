using ShareMate.Models;

namespace ShareMate.IdentityRepo
{
    public interface IIdentityInterface
    {
        string GetUserID();

        string GetUserName();


        User GetUser();
    }
}
