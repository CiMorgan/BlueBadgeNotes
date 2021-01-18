using IntroToAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI
{
    class SWAPIService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<Person> GetPersonAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)  
            {
                //was a success
                Person person = await response.Content.ReadAsAsync<Person>();
                return person;
            }
            return null;  //was not a success
        }

        public async Task<Vehicle> GetVehicleAsync (string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            return response.IsSuccessStatusCode  //same as if statement above
                ? await response.Content.ReadAsAsync<Vehicle>()   //if true
                : null;                                           //if false
        }
    }
}
