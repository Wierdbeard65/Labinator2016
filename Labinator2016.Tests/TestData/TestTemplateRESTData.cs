
namespace Labinator2016.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Labinator2016.Lib.Models;
    using Labinator2016.Lib.REST;
    using Labinator2016.Lib.Utilities;
    class TestTemplateRESTData
    {
        public static IQueryable<Template> templates
        {
            get
            {
                var templates = new List<Template>();
                for (int i = 0; i < 10; i++)
                {
                    var template = new Template();
                    template.id = "" + i + i + i + i + i + i + i + i + "";
                    template.name = "Test" + i;
                    template.VMs = TestVmRESTData.vms.ToList<Vm>();
                    templates.Add(template);
                }
                return templates.AsQueryable();
            }
        }
    }
}