using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
            //serializacja obiektu register na json
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

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var resoult = JsonConvert.DeserializeObject<Token>(jsonResult);

                Preferences.Set("accessToken", resoult.access_token);
                Preferences.Set("userId", resoult.user_id);
                Preferences.Set("userName", resoult.user_Name);
                return true;
            }

        }

        public static async Task<List<RecepiesPartial>> GetAllRecepiesPartial(int pageNuber, int pageSize)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
                                        $"Recipes/AllRecepiesPartial?pageNumber={pageNuber}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<List<RecepiesPartial>>(response);
        }

        public static async Task<IEnumerable<Recepies>> GetRecepieDetail(int id)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
                                                           $"Recipes/RecepieDetail/{id}");

            var Output = JsonConvert.DeserializeObject<IEnumerable<Recepies>>(response);

            var test = JsonConvert.DeserializeObject<ICollection<Recepies>>(response);

            //return JsonConvert.DeserializeObject<Recepies>(response);
            return Output;
        }


        //public static async Task<Recepies> GetRecepieDetail(int id)
        //{
        //    var httpClient = new HttpClient();

        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
        //                                                   $"Recipes/RecepieDetail/{id}");

        //    IEnumerable<Recepies> Output = JsonConvert.DeserializeObject<IEnumerable<Recepies>>(response);
        //    //return JsonConvert.DeserializeObject<Recepies>(response)

        //    return Output;
        //}

        public static async Task<List<FindRecepie>> FindRecepies(string title)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
                                                           $"Recipes/FindRecipe?recipeName={title}");
            return JsonConvert.DeserializeObject<List<FindRecepie>>(response);
        }
    }
}
