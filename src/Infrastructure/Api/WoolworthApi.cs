using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Woolworth.Application.Common.Exceptions;
using Woolworth.Application.Common.Interfaces;
using Woolworth.Application.Products.Models;
using Woolworth.Application.Trolley.Models;
using Woolworth.Application.User.Queries;

namespace Woolworth.Infrastructure.Api
{
    public class WoolworthApi : IWoolworthApi
    {
        private readonly HttpClient _httpClient;
        private readonly UserDto _user;

        public WoolworthApi(HttpClient client, IUserService userService)
        {
            _httpClient = client;
            _user = userService.GetCurrentUser();
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var url = GetApiUrl(ApiCatalog.GetProducts);
            var response = await _httpClient.GetAsync(url);
            var products = await ReadApiResponse<IEnumerable<ProductDto>>(response);
            return products;
        }

        public async Task<IEnumerable<HistoryDto>> GetShopperHistory()
        {
            var url = GetApiUrl(ApiCatalog.GetShoppingHistory);
            var response = await _httpClient.GetAsync(url);
            var history = await ReadApiResponse<IEnumerable<HistoryDto>>(response);
            return history;
        }

        public async Task<decimal> GetTrolleyTotal(TrolleyDto trolley)
        {
            var url = GetApiUrl(ApiCatalog.GetTrolleyTotal);
            var response = await PostAsync<TrolleyDto>(url, trolley);
            var total = await ReadApiResponse<decimal>(response);
            return total;
        }

        private string GetApiUrl(string endpoint)
        {
            var url = $"{endpoint}?token={_user.Token}";
            return url;
        }

        private async Task<T> ReadApiResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseException(response);
            }
            var s = await response.Content.ReadAsStreamAsync();
            using var sr = new StreamReader(s);
            using var reader = new JsonTextReader(sr);
            var serializer = new JsonSerializer();
            return serializer.Deserialize<T>(reader);
        }

        private async Task<HttpResponseMessage> PostAsync<T>(string url, T bodyContent)
        {
            var serializerSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(bodyContent, serializerSetting);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response;
        }
    }
}
