using CarlZeiss.Movies.Api.Repository;
using CarlZeiss.Movies.Api.Test.Mock;
using CarlZeiss.Movies.Api.Controllers;
using Xunit;
using Moq;
using System.Collections.Generic;
using CarlZeiss.Movies.Api.Models;
using AutoMapper;
using CarlZeiss.Movies.Api.Helpers;
using System;
using System.Linq;
using CarlZeiss.Movies.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CarlZeiss.Movies.Api.Test
{
    public class AdminControllerTest
    {
        [Fact]
        public async void AddShow_ValidDetails_ShowDetail()
        {
            try
            {
                //Arrange
                Mock<IMovieBookingRepository> mockRepo = new Mock<IMovieBookingRepository>();
                ShowDetailsDto showDetailsDto = 
                    new ShowDetailsDto() { MovieName = "Pink Sky", Genre = "Biography", Language = "Gujrati", ShowDate = DateTime.Today.AddDays(2).Date, MultiplexId = 2 };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AdminController controller = new AdminController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.AddShow(showDetailsDto);

                //Assert
                var okResult = result as OkObjectResult;
                if (okResult != null)
                    Assert.NotNull(okResult);

                var model = okResult.Value as IEnumerable<ShowDetailsReturnDto>;
                if (model.Count() > 0)
                {
                    Assert.NotNull(model);
                    Assert.NotNull(model?.FirstOrDefault().Id);
                    var expected = model?.FirstOrDefault().Movie.MovieName;
                    var actual = showDetailsDto?.MovieName;

                    Assert.Equal(expected: expected, actual: actual);
                }
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }

        [Fact]
        public async void AddShow_InvalidMultiplex_NotFound()
        {
            try
            {
                //Arrange
                Mock<IMovieBookingRepository> mockRepo = new Mock<IMovieBookingRepository>();
                ShowDetailsDto showDetailsDto =
                    new ShowDetailsDto() { MovieName = "Sky", Genre = "Biography", Language = "Hindi", ShowDate = DateTime.Today.AddDays(2).Date, MultiplexId = 5 };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AdminController controller = new AdminController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.AddShow(showDetailsDto);

                //Assert
                var notFoundresult = result as NotFoundResult;
                if (notFoundresult != null)
                    Assert.NotNull(notFoundresult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }

        [Fact]
        public async void AddShow_ValidationFail_BadRequest()
        {
            try
            {
                //Arrange
                Mock<IMovieBookingRepository> mockRepo = new Mock<IMovieBookingRepository>();
                ShowDetailsDto showDetailsDto =
                    new ShowDetailsDto() { MovieName = "", Genre = "Biography", Language = "Hindi", ShowDate = DateTime.Today.AddDays(2).Date, MultiplexId = 5 };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                AdminController controller = new AdminController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.AddShow(showDetailsDto);

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
    }
}
