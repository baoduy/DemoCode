using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDemo
{
	public interface IDataStore<TKey,T>
	{
		string LastErrorMessage { get;}
		Task<bool> AddItemAsync(T item);
		Task<bool> UpdateItemAsync(T item);
		Task<bool> DeleteItemAsync(TKey id);
		Task<T> GetItemAsync(TKey id);
		Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
	}

	public interface IDataStore<T>:IDataStore<int,T>
	{
		
	}
}
