using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Test
{
    class TransportData
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string RouteId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Direction { get; set; }
        

        public override string ToString()
        {
            return  GetTypeName() + "," + RouteId + "," + "Latitude: " + Latitude + "," + "Longitude: " + Longitude;

        }

        public string GetTypeName()
        {
            if (Type == 1)
            {
                return "Troll";
            }
            else if (Type == 2)
            {
                return "Buss";
            }

            else if(Type == 3)
            {
                return "Tramm";
            }
            else
            {
                return "unknown";
            }
            
        }
    }

}
