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

        //Async Method
        public async Task<Person> GetPersonAsync(string url)
        {
            //    HttpResponseMessage response = await _httpClient.GetAsync(url);
            //    if (response.IsSuccessStatusCode)  
            //    {
            //        //was a success
            //        Person person = await response.Content.ReadAsAsync<Person>();
            //        return person;
            //    }
            //    return null;  //was not a success
            return await GetAsync<Person>(url);
        }

        public async Task<Vehicle> GetVehicleAsync (string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            return response.IsSuccessStatusCode  //same as if statement above
                ? await response.Content.ReadAsAsync<Vehicle>()   //if true
                : null;                                           //if false
        }

        public async Task<T> GetAsync<T>(string url) where T: class   //where T: class is optional but helps improve code
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                T content = await response.Content.ReadAsAsync<T>();
                return content;
            }
            return null;  //return default if nonnullable is possible
        }
        public async Task<SearchResult<Person>> GetPersonSearchAsync(string query)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://swapi.dev/api/people/?search=" + query);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<Person>>();
            }
            return null;
        }

        public async Task<SearchResult<T>> GetSearchAsync<T>(string query, string category)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://swapi.dev/api/{category}/?search={query}");

            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<SearchResult<T>>()
                : default;
        }

        public async Task<SearchResult<Vehicle>> GetVehicleSearchAsync(string query)
        {
            return await GetSearchAsync<Vehicle>(query, "vehicles");
        }
    }
}
