﻿using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Recepies_M.Models;
using Recepies_M.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

                Preferences.Set("email", email);

                Preferences.Set("accessToken", resoult.access_token);
                Preferences.Set("userId", resoult.user_id);
                Preferences.Set("userName", resoult.user_Name);
                Preferences.Set("tokenExpirationTime", resoult.expiration_Time);
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                Preferences.Set("currentTime", unixTimestamp);
                
                return true;
            }

        }

        public static async Task<List<RecepiesPartial>> GetAllRecepiesPartial(int pageNuber, int pageSize)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
                                        $"Recipes/AllRecepiesPartial?pageNumber={pageNuber}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<List<RecepiesPartial>>(response);
        }

        public static async Task<List<RecepiesPartial>> GetAllRecepiesPartialById(int id, int pageNuber, int pageSize)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
                                                           $"Recipes/AllRecepiesPartialByUserId/{id}?pageNumber={pageNuber}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<List<RecepiesPartial>>(response);
        }

        public static async Task<ICollection<Recepies>> GetRecepieDetail(int id)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
                                                           $"Recipes/RecepieDetail/{id}");

            return JsonConvert.DeserializeObject<ICollection<Recepies>>(response);
        }

        public static async Task<List<FindRecepie>> FindRecepies(string title)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl +
                                                           $"Recipes/FindRecipe?recipeName={title}");
            return JsonConvert.DeserializeObject<List<FindRecepie>>(response);
        }

        public static async Task<bool> EditUser( int id, string name, string email, string password)
        {
            var user = new User()
            {
                Id = id,
                Name = name,
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(user);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(AppSettings.ApiUrl + "users/Edit", content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static async Task<bool> AddRecepie(Recepies recepies)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();
           
            var json = JsonConvert.SerializeObject(recepies);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));


            var response = await httpClient.PostAsync(AppSettings.ApiUrl +
                                                           "Recipes/AddRecepie", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        public static async Task<bool> AddRecepieIMG(string title, MediaFile mediafile)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));

            var recepiesContext = new MultipartFormDataContent();

            recepiesContext.Add(new StringContent(title),"Title");
            recepiesContext.Add(new StreamContent(mediafile.GetStream()), "Image" , "fdfd");
            

            var response = await httpClient.PutAsync(AppSettings.ApiUrl +
                                                      "Recipes/AddRecepieIMG", recepiesContext);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static async Task<bool> EditRecepie(int id, Recepies recepies)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(recepies);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));


            var response = await httpClient.PutAsync(AppSettings.ApiUrl +
                                                      $"Recipes/EditRecepie/{id}", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public static async Task<bool> EditRecepieIMG(int id, string title, MediaFile mediafile)
        {
            await TokenValidator.Check();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));

            var recepiesContext = new MultipartFormDataContent();

            recepiesContext.Add(new StringContent(title), "Title");
            recepiesContext.Add(new StreamContent(mediafile.GetStream()), "Image", "fdfd");


            var response = await httpClient.PutAsync(AppSettings.ApiUrl +
                                                     $"Recipes/EditRecepieIMG/{id}", recepiesContext);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static async Task<bool> DeleteRecepie(int id)
        {

            await TokenValidator.Check();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl +
                                                           $"Recipes/DeleteRecepie/{id}");

            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

    }
}