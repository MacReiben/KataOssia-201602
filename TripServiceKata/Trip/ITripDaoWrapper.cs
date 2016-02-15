using System.Collections.Generic;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public interface ITripDaoWrapper
    {
        List<Trip> FindTripsByUser(User.User user);
    }
}