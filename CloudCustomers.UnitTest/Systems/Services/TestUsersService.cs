using CloudCustomers.Config;
using CloudCustomers.Models;
using CloudCustomers.Services;
using CloudCustomers.UnitTest.Fixtures;
using CloudCustomers.UnitTest.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHTTPMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });

            var sut = new UserService(httpClient, config);

            //act
            await sut.GetAllUsers();

            //assert
            //verify http request is made
            handlerMock.Protected().Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfUsers()
        {
            //arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHTTPMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });

            var sut = new UserService(httpClient, config);

            //act
            var result= await sut.GetAllUsers();

            //assert
            result.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenHit404_ReturnEmptyList()
        {
            //arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHTTPMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);

            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });

            var sut = new UserService(httpClient, config);

            //act
            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfUsersOfExpectedSize()
        {
            //arrange
            var expectedResponse = UserFixture.GetTestUsers();
            var handlerMock = MockHTTPMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });

            var sut = new UserService(httpClient, config);

            //act
            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalURL()
        {
            //arrange
            var expectedResponse = UserFixture.GetTestUsers();

            var endPoint = "https://example.com/users";

            var handlerMock = MockHTTPMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endPoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UserApiOptions
            {
                EndPoint=endPoint
            });

            var sut = new UserService(httpClient, config);

            //act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endPoint);

            //assert
            handlerMock.Protected().Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri== uri),
                ItExpr.IsAny<CancellationToken>()
                );
        }

    }
}
