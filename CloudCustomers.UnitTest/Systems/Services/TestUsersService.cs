using CloudCustomers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTest.Systems.Services
{
    public class TestUsersService
    {
        [Fact]  
        public async Task GetAllUsers_WhenCalled_InvokeHttpGetRequest()
        {
            //arrange
            var sut = new UserService();

            //act
            await sut.GetAllUsers();

            //assert
            //verify http request is made
        }
    }
}
