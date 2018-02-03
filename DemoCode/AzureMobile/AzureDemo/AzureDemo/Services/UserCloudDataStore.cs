using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace AzureDemo
{
	public class UserCloudDataStore : IDataStore<User>
	{
		readonly HttpClient client;
		IEnumerable<User> items;

		public string LastErrorMessage { get; private set;}

		public UserCloudDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{App.AzureMobileAppUrl}/");

			items = new List<User>();
		}

		public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
		{
			if (forceRefresh && CrossConnectivity.Current.IsConnected)
			{
				var json = await client.GetStringAsync($"api/users");
				items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<User>>(json));
			}

			return items;
		}

		public async Task<User> GetItemAsync(int id)
		{
			if (id > 0 && CrossConnectivity.Current.IsConnected)
			{
				var json = await client.GetStringAsync($"api/users/{id}");
				items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<User>>(json));
			}

			return null;
		}

		public async Task<bool> AddItemAsync(User item)
		{
			if (item == null || !CrossConnectivity.Current.IsConnected)
				return false;
			

			var serializedItem = JsonConvert.SerializeObject(item);
			var response = await client.PostAsync($"api/users", 
			                                      new StringContent(serializedItem,Encoding.UTF8,"application/json"));

			LastErrorMessage = await response.Content.ReadAsStringAsync();
			return response.IsSuccessStatusCode ? true : false;
		}

		public async Task<bool> UpdateItemAsync(User item)
		{
			if (item == null || item.Id <= 0 || !CrossConnectivity.Current.IsConnected)
				return false;

			var serializedItem = JsonConvert.SerializeObject(item);
			var response = await client.PutAsync($"api/users/{item.Id}", 
			                                     new StringContent(serializedItem, Encoding.UTF8, "application/json"));

			LastErrorMessage = await response.Content.ReadAsStringAsync();
			return response.IsSuccessStatusCode ? true : false;
		}

		public async Task<bool> DeleteItemAsync(int id)
		{
			if (id <= 0 && !CrossConnectivity.Current.IsConnected)
				return false;

			var response = await client.DeleteAsync($"api/users/{id}");

			LastErrorMessage = await response.Content.ReadAsStringAsync();
			return response.IsSuccessStatusCode ? true : false;
		}
	}
}
