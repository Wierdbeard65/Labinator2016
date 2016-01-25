namespace Labinator2016.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Labinator2016.Lib.Models;
    class TestDataCenterData
    {
        public static IQueryable<DataCenter> DataCenters
        {
            get
            {
                var dataCenters = new List<DataCenter>();
                for (int i = 0; i < 5; i++)
                {
                    var dataCenter = new DataCenter();
                    dataCenter.DataCenterId = i;
                    dataCenter.Name = "Test" + i;
                    dataCenter.Type = (i / 3 == Math.Ceiling((double)i / 3));
                    dataCenter.GateWayIP = "123.123.123." + i;
                    dataCenters.Add(dataCenter);
                }
                return dataCenters.AsQueryable();
            }
        }
    }
}