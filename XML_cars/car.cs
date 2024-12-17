using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Xmltest
{
    internal class Car
    {
        internal string name;

        internal string CarName
        {
            get { return name; }
            set { name = value; }
        }


        internal int cylinders { get; set; }
        internal string? country { get; set; }
        public override string ToString()
        {
            return $"[Car: carname=\"{name}\", cylinders=\"{cylinders}\", country=\"{country}\"]";
        }
    }

}
