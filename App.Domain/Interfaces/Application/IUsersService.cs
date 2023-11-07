using App.Domain.Entities;

namespace App.Domain.Interfaces.Application
{
    public interface IUsersService
    {
        void UpdateUser(Users obj);
        void DeleteUser(int id);
        void AddUser(Users obj);
        Users GetUserById(int id);
        List<Users> GetAllUsers();
    }
}
