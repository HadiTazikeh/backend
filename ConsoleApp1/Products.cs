using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Products
    {
        public string name;
        public string unit;

        //public IDictionary<string, int> deliveryTimes;
        //= new Dictionary<string, int>();

        public List<TimeRange> deliveryTimes;


        public string GetName()
        {
            return name;
        }

        public void SetName(string value)
        {
            name= value;
        }

        public string GetUnit()
        {
            return unit;
        }

        public void SetUnit(string value)
        {
            unit = value;
        }

        public Products()
        {
            // deliveryTimes = new Dictionary<string,int>();
            deliveryTimes = new List<TimeRange>();
        }


    }
}
