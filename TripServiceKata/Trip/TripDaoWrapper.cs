using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripServiceKata.Trip
{
    public class TripDaoWrapper : ITripDaoWrapper
    {
        public List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
