using App.Domain.Entities;
using App.Domain.Interfaces.Application;
using App.Domain.Interfaces.Repositories;

namespace App.Application.Services
{
    public class UsersService : IUsersService
    {
        private IRepositoryBase<Users> _repository { get; set; }
        public UsersService(IRepositoryBase<Users> repository)
        {
            _repository = repository;
        }
        private void ValidateUser(Users user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentNullException(nameof(user.Name), "Nome não pode estar vazio.");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentNullException(nameof(user.Email), "Email não pode estar vazio.");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException(nameof(user.Password), "Senha não pode estar vazia.");
            }
        }

        public void AddUser(Users user)
        {
            ValidateUser(user);

            _repository.Save(user);
            _repository.SaveChanges();
        }

        public void UpdateUser(Users user)
        {
            var existingUser = _repository.Query(x => x.Id == user.Id).FirstOrDefault();

            if (existingUser == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            Users userUpdated = new Users();
            userUpdated.Id = existingUser.Id;

            userUpdated.Name = (user.Name != null) ? user.Name : existingUser.Name;
            userUpdated.Email = (user.Email != null) ? user.Email : existingUser.Email;
            userUpdated.Password = (user.Password != null) ? user.Password : existingUser.Password;

            _repository.Update(userUpdated);
            _repository.SaveChanges();
        }



        public void DeleteUser(int id)
        {
            var existingUser = _repository.Query(x => x.Id == id).FirstOrDefault();

            if (existingUser == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public Users GetUserById(int id)
        {
            var obj = _repository.Query(x => x.Id == id).FirstOrDefault();
            return obj;
        }

        public List<Users> GetAllUsers()
        {
            return _repository.Query(x => 1 == 1).ToList();
        }

    }
}