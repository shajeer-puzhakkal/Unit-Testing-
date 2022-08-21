using CloudCustomers.Controllers;
using CloudCustomers.Models;
using CloudCustomers.Services;
using CloudCustomers.UnitTest.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTest.Systems.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //arrange
            var mockUserServices = new Mock<IUserService>();
            mockUserServices
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Name ="Shajeer",
                        Address= new Address()
                        {
                            City ="MBZ",
                            Street="Z4",
                            ZipCode="154"

                        },
                        Email ="test@gmail.com"
                    }
                });

            var sut = new UserController(mockUserServices.Object);

            //act
            var result =(OkObjectResult) await sut.Get();

            //assert
            result.StatusCode.Should().Be(200);


        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUserServicesExactlyOnce()
        {
            //arrange
            var mockUserServices = new Mock<IUserService>();
            mockUserServices
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UserController(mockUserServices.Object);

            //act
            var result = await sut.Get();

            //assert
            mockUserServices.Verify(
                service => service.GetAllUsers(), 
                Times.Once()
                );
        }

        [Fact]
        public async Task Get_OnSuccess_ResturnUserList()
        {
            //arrange
            var mockUserServices = new Mock<IUserService>();
            mockUserServices
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UserFixture.GetTestUsers());

            var sut = new UserController(mockUserServices.Object);

            //act
            var result = await sut.Get();

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var objResult=(OkObjectResult) result;
            objResult.Value.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task Get_OnNoUsersFound_Return404()
        {
            //arrange
            var mockUserServices = new Mock<IUserService>();
            mockUserServices
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UserController(mockUserServices.Object);

            //act
            var result = await sut.Get();

            //assert
            result.Should().BeOfType<NotFoundResult>(); 
            var objResult = (NotFoundResult)result;
            objResult.StatusCode.Should().Be(404);
        }

    }
}