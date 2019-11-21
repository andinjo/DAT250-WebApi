using Models.Core;

namespace Services
{
    public interface IUserService
    {
        User Auth();

        string Id();

        bool Is(string userId);

        bool Exists();
    }
}
