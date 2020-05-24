using AutoMapper;
using CarlZeiss.Movies.Api.Controllers;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Helpers;
using CarlZeiss.Movies.Api.Repository;
using CarlZeiss.Movies.Api.Test.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace CarlZeiss.Movies.Api.Test
{
    public class BookingsControllerTest
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        Mock<IMovieBookingRepository> mockRepo;

        public BookingsControllerTest()
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
        public async void GetBookingsByUser_WhenCalled_ShowBookedMovies()
        {
            try
            {
                //Arrange
               
                _httpContextAccessor.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<string>()))
                .Returns(new Claim(ClaimTypes.NameIdentifier, "1"));

                
                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.GetBookings();

                //Assert
                var okResult = result as OkObjectResult;
                if (okResult != null)
                    Assert.NotNull(okResult);

                var model = okResult.Value as IEnumerable<UserBookingReturnDto>;
                if (model.Count() > 0)
                {
                    Assert.NotNull(model);

                    var expected = 1;
                    var actual = model?.FirstOrDefault().UserId;

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
        public async void GetBookingsByUser_ValidDetails_NoContent()
        {
            try
            {
                //Arrange
                _httpContextAccessor.Setup(x => x.HttpContext.User.FindFirst(It.IsAny<string>()))
                .Returns(new Claim(ClaimTypes.NameIdentifier, "5"));

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.GetBookings();

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

        [Fact]
        public async void GetBookingsByBookingId_ValidDetails_ShowBookedMovie()
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

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.GetBookings(1);

                //Assert
                var okResult = result as OkObjectResult;
                if (okResult != null)
                    Assert.NotNull(okResult);

                var model = okResult.Value as IEnumerable<UserBookingReturnDto>;
                if (model.Count() > 0)
                {
                    Assert.NotNull(model);

                    var expected = 1;
                    var actual = model?.FirstOrDefault().Id;

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
        public async void GetBookingsByBookingId_ValidDetails_NoContent()
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

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.GetBookings(2);

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

        [Fact]
        public async void BookMovie_ValidDetails_ShowBookedMovie()
        {
            try
            {
                //Arrange
                UserBookingDto userBookingDto =
                    new UserBookingDto() { UserId=1,
                                            ShowId =1,
                                            Seat = new List<SeatDto>() { new SeatDto() { SeatId=1 }, new SeatDto() { SeatId=2 } },
                                            MultiplexId = 1,
                                            BookingDate = DateTime.Now
                                            };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.BookMovie(userBookingDto);

                //Assert
                var okResult = result as OkObjectResult;
                if (okResult != null)
                    Assert.NotNull(okResult);

                var model = okResult.Value as IEnumerable<UserBookingReturnDto>;
                if (model.Count() > 0)
                {
                    Assert.NotNull(model);
                    Assert.NotNull(model?.FirstOrDefault().Id);
                    var expected = userBookingDto?.UserId;
                    var actual = model?.FirstOrDefault().UserId;

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
        public async void BookMovie_InvalidShowId_NotFound()
        {
            try
            {
                //Arrange
                UserBookingDto userBookingDto =
                    new UserBookingDto()
                    {
                        UserId = 1,
                        ShowId = 99,
                        Seat = new List<SeatDto>() { new SeatDto() { SeatId = 1 }, new SeatDto { SeatId = 2 } },
                        MultiplexId = 1,
                        BookingDate = DateTime.Now
                    };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.BookMovie(userBookingDto);

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

        [Fact]
        public async void BookMovie_InvalidMultiplexId_NotFound()
        {
            try
            {
                //Arrange
                UserBookingDto userBookingDto =
                    new UserBookingDto()
                    {
                        UserId = 1,
                        ShowId = 1,
                        Seat = new List<SeatDto>() { new SeatDto() { SeatId = 1 }, new SeatDto() { SeatId = 2 } },
                        MultiplexId = 10,
                        BookingDate = DateTime.Now
                    };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.BookMovie(userBookingDto);

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

        [Fact]
        public async void BookMovie_InvalidSeatList_NotFound()
        {
            try
            {
                //Arrange
                UserBookingDto userBookingDto =
                    new UserBookingDto()
                    {
                        UserId = 1,
                        ShowId = 1,
                        Seat = new List<SeatDto>() { new SeatDto() { SeatId = 1 },
                                                     new SeatDto() { SeatId = 50 }
                        },
                        MultiplexId = 1,
                        BookingDate = DateTime.Now
                    };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.BookMovie(userBookingDto);

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

        [Fact]
        public async void AddShow_MoreThen5Seats_BadRequest()
        {
            try
            {
                //Arrange
                UserBookingDto userBookingDto =
                    new UserBookingDto()
                    {
                        UserId = 1,
                        ShowId = 99,
                        Seat = new List<SeatDto>() { new SeatDto() { SeatId = 1 },
                                                     new SeatDto() { SeatId = 2 },
                                                     new SeatDto() { SeatId = 3 },
                                                     new SeatDto() { SeatId = 4 },
                                                     new SeatDto() { SeatId = 5 },
                                                     new SeatDto() { SeatId = 6 },
                        },
                        MultiplexId = 1,
                        BookingDate = DateTime.Now
                    };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.BookMovie(userBookingDto);

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
        public async void AddShow_ModelValidationFail_BadRequest()
        {
            try
            {
                //Arrange
                UserBookingDto userBookingDto =
                    new UserBookingDto()
                    {
                        UserId = 1,
                        ShowId = 1,
                        Seat = new List<SeatDto>() { new SeatDto() { SeatId = 1 },
                                                     new SeatDto() { SeatId = 2 }
                        },
                        BookingDate = DateTime.Now
                    };

                //auto mapper configuration
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfiles());
                });
                var mapper = mockMapper.CreateMapper();

                BookingsController controller = new BookingsController(repo: mockRepo.Object, mapper: mapper);

                //Act
                var result = await controller.BookMovie(userBookingDto);

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
