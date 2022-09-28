using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class DataBase
    {
        //static fields
        public static ArrayList products = new ArrayList();


        //static constructor
        static DataBase()
        {

            var deliveryTimes = new List<TimeRange>
            {
                new TimeRange { day = 1, Min = 0, Max = 10 },
                 new TimeRange { day = 2, Min = 10, Max = 20 },
                  new TimeRange { day = 3, Min = 20, Max = 100 },
            };

            Products pp = new Products();
            pp.SetName("Adhesive");
            pp.SetUnit("kg");
            pp.deliveryTimes = deliveryTimes;
           // pp.deliveryTimes.Add("10 - 20 kg", 2);
           // pp.deliveryTimes.Add("20 - 100 kg", 3);
            products.Add(pp);

            var deliveryTimes2 = new List<TimeRange>
            {
                new TimeRange { day = 1, Min = 0, Max = 50 },
                 new TimeRange { day = 2, Min = 50, Max = 200 },
                  new TimeRange { day = 10, Min = 200, Max = 1000 },
            };

            pp = new Products();
            pp.SetName("Ink");
            pp.SetUnit("Liter");
            pp.deliveryTimes = deliveryTimes2;
            //pp.deliveryTimes.Add("0 - 50 L", 1);
            //pp.deliveryTimes.Add("50 - 200 L", 2);
            //pp.deliveryTimes.Add("200 - 1000 L", 10);
            products.Add(pp);
        }
    }

    class TimeRange
    {
        public int day;
        public int Min;
        public int Max;
    }
}
