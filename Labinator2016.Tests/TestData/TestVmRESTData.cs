using System.Collections.Generic;
using System.Linq;
using Labinator2016.Lib.REST;

namespace Labinator2016.Tests.TestData
{
    class TestVmRESTData
    {
        public static IQueryable<Vm> vms
        {
            get
            {
                var vms = new List<Vm>();
                for (int i = 0; i < 10; i++)
                {
                    var vm = new Vm();
                    vm.id = "" + i + i + i + i + i + i + i + i + "";
                    vm.name = "Test" + i;
                    vms.Add(vm);
                }
                return vms.AsQueryable();
            }
        }
    }
}
