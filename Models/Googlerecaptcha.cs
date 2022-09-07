using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Googlerecaptcha
    {
        public int Id { get; set; }
        public string? HostName { get; set; }
        public string SiteKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string? Response { get; set; }
        
        // internal static async Task<bool> Validate(HttpResponse request)
        // {
        //     string recaptchaResponse = request.HttpContext.Request.Form["g-recaptcha-response"];
        //     if (string.IsNullOrEmpty(recaptchaResponse))
        //     {
        //         return false;
        //     }
        //     using (var client = new HttpClient { BaseAddress = new Uri("https://www.google.com") })
        //     {
        //         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         var content = new FormUrlEncodedContent(new[]
        //         {
        //             new KeyValuePair<string, string>("secret", ConfigurationManager.AppSettings["RecaptchaSecret"]),
        //             new KeyValuePair<string, string>("response", recaptchaResponse),
        //             new KeyValuePair<string, string>("remoteip", request.UserHostAddress)
        //         });
        //         var result = await client.PostAsync("/recaptcha/api/siteverify", content);
        //         result.EnsureSuccessStatusCode();
        //         string jsonString = await result.Content.ReadAsStringAsync();
        //         var response = JsonConvert.DeserializeObject<RecaptchaResponse>(jsonString);
        //         return response.Success;
        //     }
        // }
    }
}
