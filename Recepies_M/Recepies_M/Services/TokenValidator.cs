using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Recepies_M.Services
{
    public static class TokenValidator
    {
        public static async Task Check()
        {
            var exTime = Preferences.Get("tokenExpirationTime", 0);

            var curTime = Preferences.Get("currentTime", 0);

            if (exTime < curTime)
            {
                var email = Preferences.Get("email", String.Empty);
                var passwort = Preferences.Get("password", String.Empty);
                await ApiService.LoginUser(email, passwort);
            }

        }
    }
}
