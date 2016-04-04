using Labinator2016.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Labinator2016.Tests.TestData
{
    class TestSeatTempData
    {
        public static IQueryable<SeatTemp> SeatTemps
        {
            get
            {
                var seatTemps = new List<SeatTemp>();
                for (int i = 1; i < 10; i++)
                {
                    var seatTemp = new SeatTemp();
                    seatTemp.SeatTempId = i;
                    seatTemp.ClassroomId = i * 10;
                    seatTemp.SessionId = "12345";
                    seatTemp.ConfigurationId = "" + i + i + i + i + i + i + i + i + "";
                    seatTemp.SeatId = i * 100;
                    seatTemp.TimeStamp = DateTime.Now;
                    seatTemp.UserId = i * 1000;
                    seatTemps.Add(seatTemp);
                }
                return seatTemps.AsQueryable();
            }
        }
    }
}