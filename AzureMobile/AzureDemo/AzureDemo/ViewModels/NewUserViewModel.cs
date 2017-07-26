using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AzureDemo
{
	public class NewUserViewModel:EditUserViewModel
	{

		public NewUserViewModel():base(new AzureDemo.User())
		{
			Title = "Add User";
			IsEditMode = false;
		}

		protected override async Task ExecuteSaveItemsCommand(Page view)
		{
			if (await DataStore.AddItemAsync(Item))
			{
				MessagingCenter.Send(this, "UserAdded", Item);
				await view.Navigation.PopToRootAsync(); 
			}
			else await view.DisplayAlert("Error", DataStore.LastErrorMessage, "Ok");
		}
	}
}
