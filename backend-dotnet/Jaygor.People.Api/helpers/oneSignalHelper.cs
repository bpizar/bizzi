using RestSharp;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace JayGor.People.Api.helpers
{
	public static class oneSignalHelper
	{
		//static string basicOauth = "Basic OTdlYzE2NzQtMTMwNi00N2UyLWE2YmYtZGIzMzUzNjgwNzEy";
		//static string appId = "75490c0d-eeca-4f44-adcc-1dac2d273114";

        static string basicOauth = string.Empty;
        static List<string> appIdList;
      
        public static void Config()
        {
            appIdList = new List<string>();

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			var configuration = builder.Build();
            var oneSignalSection = configuration.GetSection("OneSignal");
            basicOauth = string.Format("Basic {0}", oneSignalSection.GetValue<string>("RestApiKey"));


            appIdList.Add(oneSignalSection.GetValue<string>("AppId"));

            //appIdList.Add(oneSignalSection.GetValue<string>("AppAndroidId"));
            //appIdList.Add(oneSignalSection.GetValue<string>("AppIosId"));
        }

		public static void SendNotificationToPlayerId(string playerid, string message, data data)
		{        
            foreach(string appid in appIdList)
            {
                var client = new RestClient("https://onesignal.com");
                var request = new RestRequest("/api/v1/notifications", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", basicOauth);
                request.AddJsonBody(new rawDataBody
                {
                    app_id = appid,
                    include_player_ids = new List<string> { playerid },
                    contents = new contents
                    {
                        en = message
                    },
                    //template_id = "a",
                    content_available = true,
                    //android_visibility = -1,
                    data = data
                });

                client.Execute(request);
            }           
		}

		//public static void SendNotificationToAll(string message, data data)
		//{
		//	var client = new RestClient("https://onesignal.com");
		//	var request = new RestRequest("/api/v1/notifications", Method.POST);
		//	request.AddHeader("Content-Type", "application/json");
		//	request.AddHeader("Authorization", basicOauth);

		//	request.AddJsonBody(new rawDataBodyAll
		//	{
		//		app_id = appId,
		//		included_segments = new List<string> { "All" },
		//		contents = new contents
		//		{
		//			en = message
		//		},
               
		//		data = data
		//	});

		//	//// client.ExecuteAsync(request, response =>
		//	//// {
		//	////    //Console.WriteLine(response.Content);
		//	//// });
		//	client.Execute(request);
		//}
	}

	public class rawDataBody
	{
		public string app_id { get; set; }
		public List<string> include_player_ids { get; set; }
		public data data { get; set; }
		public contents contents { get; set; }
		// public string template_id { get; set; }
        public bool content_available { get; set; }
       // public int android_visibility { get; set; }
	}

	public class rawDataBodyAll
	{
		public string app_id { get; set; }
		public List<string> included_segments { get; set; }
		public data data { get; set; }
		public contents contents { get; set; }
	}

	public class contents
	{
		public string en { get; set; }
	}

	public class data
	{
		public string TypeMessage { get; set; }
		public string Value1 { get; set; }
		public string Value2 { get; set; }
		public string Value3 { get; set; }
		public string Value4 { get; set; }
		public string Value5 { get; set; }
	}
}