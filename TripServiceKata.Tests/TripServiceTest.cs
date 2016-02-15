using Moq;
using NFluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceTest
    {
        private TripServiceTestable _target;
        private Mock<ITripDaoWrapper> _mockDaoWrapper;

        [SetUp]
        public void Setup()
        {
            _mockDaoWrapper = new Mock<ITripDaoWrapper>();

            _target = new TripServiceTestable(_mockDaoWrapper.Object);
        }

        [Test]
        [ExpectedException(typeof(UserNotLoggedInException))]
        public void When_logged_user_is_null_throw_exception()
        {
            _target.GetTripsByUser(null);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void When_user_is_null_and_logged_user_note_null_throw_exception()
        {
            _target.SetLoggerUser(new User.User());

            _target.GetTripsByUser(null);
        }

        [Test]
        public void When_user_has_no_friend_return_empty_list()
        {
            // ARRANGE
            _target.SetLoggerUser(new User.User());

            User.User original = new User.User();

            // ACT
            List<Trip.Trip> actual = _target.GetTripsByUser(original);

            // ASSERT
            Check.That(actual).IsEmpty();
        }

        [Test]
        public void When_user_is_not_friend_with_loggedUser_return_empty_list()
        {
            // ARRANGE
            _target.SetLoggerUser(new User.User());

            User.User original = new User.User();
            original.AddFriend(new User.User());

            // ACT
            List<Trip.Trip> actual = _target.GetTripsByUser(original);

            // ASSERT
            Check.That(actual).IsEmpty();
        }

        [Test]
        public void When_user_is_friend_with_loggedUser_return_user_trip()
        {
            // ARRANGE
            User.User originalLoggedUser = new User.User();

            _target.SetLoggerUser(originalLoggedUser);

            List<Trip.Trip> originaltripList 
                = new List<Trip.Trip>() { new Trip.Trip() };

            _mockDaoWrapper.Setup(m => 
                m.FindTripsByUser(It.IsAny<User.User>()))
                .Returns(originaltripList);

            //_target.SetTripList(originaltripList);

            User.User original = new User.User();
            original.AddFriend(originalLoggedUser);

            // ACT
            List<Trip.Trip> actual = _target.GetTripsByUser(original);

            // ASSERT
            Check.That(actual).IsOnlyMadeOf(originaltripList);
        }
    }
}
