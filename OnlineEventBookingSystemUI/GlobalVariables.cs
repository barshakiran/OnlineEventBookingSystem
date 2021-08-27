using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineEventBookingSystem
{
    public class GlobalVariables
    {
        public static HttpClient WebApiClient;

        static GlobalVariables()
        {
            var authCredential = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("Admin:admin"));
            WebApiClient = new HttpClient();
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authCredential);
            WebApiClient.BaseAddress = new Uri("https://localhost:44346/");
            WebApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));            
        }
    }
}