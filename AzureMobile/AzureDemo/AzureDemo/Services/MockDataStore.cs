using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDemo
{
	public class MockDataStore : IDataStore<User>
	{
		bool isInitialized;
		List<User> items;

		public string LastErrorMessage { get; private set;}

		public MockDataStore()
		{
			items = new List<User>();
			var _items = new List<User>
			{
				new User { Id = 1, FirstName = "First item", Description="This is a nice description"},
				new User { Id = 2, FirstName = "Second item", Description="This is a nice description"},
				new User { Id = 3, FirstName = "Third item", Description="This is a nice description"},
				new User { Id = 4, FirstName = "Fourth item", Description="This is a nice description"},
				new User { Id = 5, FirstName = "Fifth item", Description="This is a nice description"},
				new User { Id = 6, FirstName = "Sixth item", Description="This is a nice description"},
			};

			foreach (User item in _items)
			{
				items.Add(item);
			}
		}

		public async Task<bool> AddItemAsync(User item)
		{
			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(User item)
		{
			var _item = items.Where((User arg) => arg.Id == item.Id).FirstOrDefault();
			items.Remove(_item);
			items.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(int id)
		{
			var _item = items.Where((User arg) => arg.Id == id).FirstOrDefault();
			items.Remove(_item);

			return await Task.FromResult(true);
		}

		public async Task<User> GetItemAsync(int id)
		{
			return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(items);
		}
	}
}
