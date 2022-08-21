using CloudCustomers.Config;
using CloudCustomers.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomers.Services
{

    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UserApiOptions _apiConfig;

        public UserService(HttpClient httpClient, IOptions<UserApiOptions> apiConfig)
        {
            _httpClient=httpClient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await _httpClient.GetAsync(_apiConfig.EndPoint);

            if(userResponse.StatusCode== System.Net.HttpStatusCode.NotFound)
                return new List<User> { };

            var responseContent = userResponse.Content;
            var response = await responseContent.ReadFromJsonAsync<List<User>>();
            return response.ToList();

        }
    }
}
