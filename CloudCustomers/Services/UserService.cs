using CloudCustomers.Models;

namespace CloudCustomers.Services
{

    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
