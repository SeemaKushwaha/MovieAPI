using AutoMapper;
using AutoMapper.Configuration;
using CarlZeiss.Movies.Api.Controllers;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Helpers;
using CarlZeiss.Movies.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CarlZeiss.Movies.Api.Test
{
    public class AuthControllerTest
    {
        private readonly string tokenSecret= "this is a super long super secret";

        [Fact]
        public async void Register_ValidUser_UserName()
        {
            try
            {
                //Arrange
                Mock<IAuthRepository> mockRepo = new Mock<IAuthRepository>();
                UserRegisterDto userRegisterDto =
                    new UserRegisterDto() { Username = "Anu", Password="password", Role="Admin" };

                var inMemorySettings = new Dictionary<string, string>
                    {
                        {"AppSettings:Token", tokenSecret},
                    };

                Microsoft.Extensions.Configuration.IConfiguration _configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AuthController controller = new AuthController(repo: mockRepo.Object, config: _configuration, mapper: mapper);

                //Act
                var result = await controller.Register(userRegisterDto);

                //Assert
                var okResult = result as OkObjectResult;
                if (okResult != null)
                    Assert.NotNull(okResult);

                var model = okResult.Value as UserLoginDto;
                
                    Assert.NotNull(model);

                    var expected = model?.Username;
                    var actual = userRegisterDto?.Username;

                    Assert.Equal(expected: expected, actual: actual);
               
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }

        [Fact]
        public async void Register_AlreadyExist_BadRequest()
        {
            try
            {
                //Arrange
                Mock<IAuthRepository> mockRepo = new Mock<IAuthRepository>();
                UserRegisterDto userRegisterDto =
                    new UserRegisterDto() { Username = "Anu", Password = "password", Role = "Admin" };

                var inMemorySettings = new Dictionary<string, string>
                    {
                        {"AppSettings:Token", tokenSecret},
                    };

                Microsoft.Extensions.Configuration.IConfiguration _configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AuthController controller = new AuthController(repo: mockRepo.Object, config: _configuration, mapper: mapper);

                //Act
                var result = await controller.Register(userRegisterDto);

                //Assert
                var badRequestResult = result as BadRequestObjectResult;
                if (badRequestResult != null)
                    Assert.NotNull(badRequestResult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }

        [Fact]
        public async void Register_ValidationFail_BadRequest()
        {
            try
            {
                //Arrange
                Mock<IAuthRepository> mockRepo = new Mock<IAuthRepository>();
                UserRegisterDto userRegisterDto =
                    new UserRegisterDto() { Username = "", Password = "password", Role = "Admin" };

                var inMemorySettings = new Dictionary<string, string>
                    {
                        {"AppSettings:Token", tokenSecret},
                    };

                Microsoft.Extensions.Configuration.IConfiguration _configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AuthController controller = new AuthController(repo: mockRepo.Object,config:_configuration, mapper: mapper);

                //Act
                var result = await controller.Register(userRegisterDto);

                //Assert
                var badRequestResult = result as BadRequestObjectResult;
                if (badRequestResult != null)
                    Assert.NotNull(badRequestResult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }

        [Fact]
        public async void Login_ValidUser_Token()
        {
            try
            {
                //Arrange
                Mock<IAuthRepository> mockRepo = new Mock<IAuthRepository>();
                UserLoginDto userLoginDto =
                    new UserLoginDto() { Username = "Anu", Password = "password"};

                var inMemorySettings = new Dictionary<string, string>
                    {
                        {"AppSettings:Token", tokenSecret},
                    };

                Microsoft.Extensions.Configuration.IConfiguration _configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AuthController controller = new AuthController(repo: mockRepo.Object, config: _configuration, mapper: mapper);

                //Act
                var result = await controller.Login(userLoginDto);

                //Assert
                var okResult = result as OkObjectResult;
                if (okResult != null)
                    Assert.NotNull(okResult);

                var model = okResult.Value;

                Assert.NotNull(model);

                Assert.IsType<string>(model);

            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }

        [Fact]
        public async void Login_InvalidUser_Unauthorized()
        {
            try
            {
                //Arrange
                Mock<IAuthRepository> mockRepo = new Mock<IAuthRepository>();
                UserLoginDto userLoginDto =
                    new UserLoginDto() { Username = "Anu", Password = "password" };

                var inMemorySettings = new Dictionary<string, string>
                    {
                        {"AppSettings:Token", tokenSecret},
                    };

                Microsoft.Extensions.Configuration.IConfiguration _configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AuthController controller = new AuthController(repo: mockRepo.Object, config: _configuration, mapper: mapper);

                //Act
                var result = await controller.Login(userLoginDto);

                //Assert
                var unauthorizedResult = result as UnauthorizedResult;
                if (unauthorizedResult != null)
                    Assert.NotNull(unauthorizedResult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }

    }
}
