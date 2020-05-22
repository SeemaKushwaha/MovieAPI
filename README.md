# MovieAPI

Login
POST - https://localhost:44322/api/auth/login
{
	"username": "sonali",
	"password": "password"
}

Register
POST - https://localhost:44322/api/auth/register
{
	"username": "Seema",
	"password": "password",
	"role": "User"
}

AddShow (Admin)
POST - https://localhost:44322/api/Admin/show
{
	"MovieName":"Avenger",
	"Genre":"Action",
	"Language":"Spanish",
	"MultiplexId":"3",
	"ShowDate":"06-06-2020"
}

GetCities (Admin,User)
GET - https://localhost:44322/api/values/city

GetMultiplexes (Admin,User)
GET - https://localhost:44322/api/values/multiplexes/{cityId}

Odata Enabled - GetShow (Admin,User)
GET - https://localhost:44322/api/movies/{multiplexId}?$filter=movie/Genre eq 'Action'

GetShows (Admin,User)
GET - https://localhost:44322/api/values/seats/{multiplexId}/{showId}

GetBookingOfCurrentUser
GET - https://localhost:44322/api/Bookings

GetBookingByMultiplex
GET - https://localhost:44322/api/Bookings/{multiplexId}

BookMovie
POST - https://localhost:44322/api/Bookings
{
	"ShowId": "3",
	"MultiplexId": "2",
	"Seat": [{
		"SeatId": 8
	}]
}
