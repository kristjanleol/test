using System;
using System.Net;
using System.Collections.Generic;
using System.IO;

namespace Test
{

    //user chooses transport type
    //user  chooses route id
    //program shows transport type, route id, latitude and longitude
    class Program
    {
        static void Main()
        {
            int transportType = GetVehicle();
            UserTransport(transportType);
            var userRouteId = Console.ReadLine();
            ParseData(transportType, userRouteId);

        }

        private static void UserTransport(int transportType)
        {
            switch (transportType)
            {
                case 1:
                    Console.WriteLine("Please enter route id from 1 to 5");
                    break;
                case 2:
                    Console.WriteLine("Please enter Route Id:");
                    break;
                case 3:
                    Console.WriteLine("Please enter route id from 1 to 4");
                    break;
            }
        }

        public static int GetVehicle()
        {

            Console.WriteLine("Please choose type of transportation: 1(Trolleybus), 2(Bus) or 3(Tram)");
            var transportType =  Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            return transportType;

        }

        static void ParseData(int transportType, string userRouteId)
        {

            List<string> strContent = new List<string>();
            
            Dictionary <string, List<TransportData>> transportDataDictionary = new Dictionary<string, List<TransportData>>();

            string url = "https://transport.tallinn.ee/gps.txt";
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using var responseReader = new StreamReader(webStream ?? throw new InvalidOperationException());
            while (!responseReader.EndOfStream)
            {
                strContent.Add(responseReader.ReadLine());
            }

            foreach (var line in strContent)
            {
                
                string[] dataPiece = line.Split(',');
                TransportData newBusData = new TransportData();

                newBusData.Type = int.Parse(dataPiece[0]);
                newBusData.RouteId = dataPiece[1];               
                newBusData.Latitude = int.Parse(dataPiece[2]);
                newBusData.Longitude = int.Parse(dataPiece[3]);
                newBusData.Direction = int.Parse(dataPiece[5]);
                newBusData.Id = int.Parse(dataPiece[6]);

                
                bool exists = transportDataDictionary.TryGetValue(newBusData.RouteId, out List<TransportData> existingBusses);

                
                if (exists)
                {
                    existingBusses.Add(newBusData);
                }
                
                else
                {
                    List<TransportData> newList = new List<TransportData>();
                    newList.Add(newBusData);

                    transportDataDictionary.Add(newBusData.RouteId, newList);

                }
            }

            foreach (var busNumber in transportDataDictionary.Keys)
            {
                if (busNumber != userRouteId) continue;
                List<TransportData> resultsDataList;
                transportDataDictionary.TryGetValue(busNumber, out resultsDataList);
                if (resultsDataList != null)
                    foreach (var i in resultsDataList)
                    {
                        if (transportType == i.Type)
                        {
                            Console.WriteLine(i);
                        }
                    }
            }
        }
    }
}
