using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recepies_M.Models;
using Recepies_M.Settings;
using Xamarin.Essentials;

namespace Recepies_M.Services
{
    public static class ApiService
    {
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "users/register", content);

            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> LoginUser(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "users/login", content);

            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var resoult = JsonConvert.DeserializeObject<Token>(jsonResult);

            Preferences.Set("accessToken", resoult.access_token);
            Preferences.Set("userId", resoult.user_id);
            Preferences.Set("userName", resoult.user_Name);
            return true;
        }
    }
}
