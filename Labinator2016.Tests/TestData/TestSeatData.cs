using Labinator2016.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Labinator2016.Tests.TestData
{
    class TestSeatData
    {
        public static IQueryable<Seat> Seats
        {
            get
            {
                var seats = new List<Seat>();
                for (int i = 1; i < 10; i++)
                {
                    var seat = new Seat();
                    seat.SeatId = i;
                    seat.ClassroomId = i * 10;
                    seat.ConfigurationId = "" + i + i + i + i + i + i + i + i + "";
                    seat.UserId = i * 1000;
                    seats.Add(seat);
                }
                return seats.AsQueryable();
            }
        }
    }
}
