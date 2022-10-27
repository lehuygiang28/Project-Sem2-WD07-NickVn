using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Runtime.Serialization;


public class RecaptchaServices
{
    
    //ActionFilterAttribute has no async for MVC 5 therefore not using as an actionfilter attribute - needs revisiting in MVC 6
    internal static async Task<bool> Validate(HttpRequest request, string SecretKey)
    {
        // return true; // Disable recaptcha check for dev

        string recaptchaResponse = "";
        try
        {
            recaptchaResponse = request.HttpContext.Request.Form["g-recaptcha-response"];
        }
        catch (Exception e)
        {
            Console.WriteLine("EX: " + e.Message);
            return false;
        }
        if (string.IsNullOrEmpty(recaptchaResponse))
        {
            return false;
        }
        using (var client = new HttpClient { BaseAddress = new Uri("https://www.google.com") })
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", SecretKey),
                new KeyValuePair<string, string>("response", recaptchaResponse),
                new KeyValuePair<string, string>("remoteip", request.HttpContext.Connection.RemoteIpAddress != null ? request.HttpContext.Connection.RemoteIpAddress.ToString() : "")
            });
            var result = await client.PostAsync("/recaptcha/api/siteverify", content);
            result.EnsureSuccessStatusCode();
            string jsonString = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<RecaptchaResponse>(jsonString);

            if (response == null)
            {
                return false;
            }
            return response.Success;
        }
    }

    [DataContract]
    internal class RecaptchaResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
        [DataMember(Name = "challenge_ts")]
        public DateTime ChallengeTimeStamp { get; set; }
        [DataMember(Name = "hostname")]
        public string? Hostname { get; set; }
        [DataMember(Name = "error-codes")]
        public IEnumerable<string>? ErrorCodes { get; set; }
    }

}
