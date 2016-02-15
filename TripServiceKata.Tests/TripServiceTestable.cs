using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Tests
{
    public class TripServiceTestable : TripService
    {
        private User.User _loggerUser;
        private List<Trip.Trip> _tripList;

        public TripServiceTestable(ITripDaoWrapper tripDao) : base(tripDao)
        {
        }

        protected override User.User GetLoggedUser()
        {
            return _loggerUser;
        }

        //protected override List<Trip.Trip> GetTripListByUser(User.User user)
        //{
        //    return _tripList;
        //}

        internal void SetLoggerUser(User.User user)
        {
            _loggerUser = user;
        }

        internal void SetTripList(List<Trip.Trip> originaltripList)
        {
            _tripList = originaltripList;
        }
    }
}
