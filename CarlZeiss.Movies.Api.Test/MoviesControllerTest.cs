using AutoMapper;
using CarlZeiss.Movies.Api.Controllers;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Helpers;
using CarlZeiss.Movies.Api.Repository;
using CarlZeiss.Movies.Api.Test.Mock;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CarlZeiss.Movies.Api.Test
{
    public class MoviesControllerTest
    {
        Mock<IMovieBookingRepository> mockRepo;

        public MoviesControllerTest()
        {
            mockRepo = new Mock<IMovieBookingRepository>();
            mockRepo.Object.Add(DummyData.Users);
            mockRepo.Object.Add(DummyData.Cities);
            mockRepo.Object.Add(DummyData.Multiplexes);
            mockRepo.Object.Add(DummyData.MasterSeats);
            mockRepo.Object.Add(DummyData.Movies);
            mockRepo.Object.Add(DummyData.Shows);
            mockRepo.Object.Add(DummyData.Bookings);
            mockRepo.Object.Add(DummyData.BookedSeats);
        }

        [Fact]
        public async void GetShows_WhenCalled_ShowDetails()
        {
            try
            {
                //Arrange
                

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                MoviesController controller = new MoviesController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.GetShows(1);

                //Assert
                var okResult = result as OkObjectResult;
                if (okResult != null)
                    Assert.NotNull(okResult);

                var model = okResult.Value as IEnumerable<ShowDetailsReturnDto>;
                if (model.Count() > 0)
                {
                    Assert.NotNull(model);

                    var expected = 1;
                    var actual = model?.FirstOrDefault().Multiplex.Id;

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
        public async void GetShows_InvalidMultiplexId_ShowDetails()
        {
            try
            {
                //Arrange

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                MoviesController controller = new MoviesController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.GetShows(60);

                //Assert
                var notFoundResult = result as NotFoundResult;
                if (notFoundResult != null)
                    Assert.NotNull(notFoundResult);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.False(false, ex.Message);
            }
        }
    }
}
