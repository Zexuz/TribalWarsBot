using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

using CsQuery.StringScanner.ExtensionMethods;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TribalWarsBot.Helpers;


namespace TribalWarsBot.Services {

    public class MapService {

        private readonly RequestManager _requestManager;

        public MapService(RequestManager requestManager) {
            _requestManager = requestManager;
        }

        public void GetMapForGrid(int x, int y) {
            var url = $"https://sv36.tribalwars.se/map.php?v=2&{x}_{y}=1";
            var req = _requestManager.GenerateGETRequest(url, null, null, false);
            var res = _requestManager.GetResponse(req);

            var json = RequestManager.GetResponseStringFromResponse(res);

            json = json.SubstringBetween(1, json.Length - 1);

            dynamic dynamicJsonTest = JObject.Parse(json);
            string str = dynamicJsonTest.data.villages.ToString();
            var jsondata = JObject.Parse(str);
            var listOfVillages = new List<MapVillage>();
            foreach (JProperty jsonElement in jsondata.Properties())
            {
                Console.WriteLine("Json key " +  jsonElement.Name + " artist: ");
                Console.WriteLine(jsonElement.Name);
                foreach (var nested in JObject.Parse(jsonElement.Value.ToString()).Properties()) {
                    var village = new MapVillage {
                        VillageId = (int) nested.Value[0],
                        DontKnow = (int) nested.Value[1],
                        VillageName = (string) nested.Value[2],
                        VillagePoints = (string) nested.Value[3],
                        DontKnow2 = (int) nested.Value[4],
                        NullObject = null,
                        Something =  (string) nested.Value[6],
                        StirngThatIsZero = (string) nested.Value[7]

                    };
                    listOfVillages.Add(village);


                }
            }

            Console.WriteLine(listOfVillages);
            Environment.Exit(0);
        }

        public class MapVillage {

            public int VillageId { get; set; }
            public int DontKnow { get; set; }
            public string VillageName { get; set; }
            public string VillagePoints { get; set; }
            public string Something { get; set; }
            public int DontKnow2 { get; set; }
            public object NullObject{ get; set; }
            public string StirngThatIsZero { get; set; }

        }

    }

}