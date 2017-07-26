using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AzureDemo
{
	public class EditUserViewModel:BaseViewModel
	{
		public User Item { get; set; }
		public bool IsEditMode { get; protected set;}
		public ICommand SaveCommand { get; private set; }
		public ICommand DeleteCommand { get; private set;}
		public ICommand CancelCommand { get; private set; }
		public bool IsSaved { get; private set; } = false;

		public string LastErrorMessage => DataStore.LastErrorMessage;

		public EditUserViewModel(User user)
		{
			Title = $"Edit {user.FirstName}";
			IsEditMode = true;
			Item = user;
			Item.PropertyChanged += (s, e) => ((Command)SaveCommand).ChangeCanExecute();
			SaveCommand = new Command(async (object view) => await ExecuteSaveItemsCommand(view as Page), (obj) => CanExecuteSaveCommand(obj));
			DeleteCommand = new Command(async (object view) => await ExecuteDeleteCommand(view as Page), (ob) => IsEditMode);
			CancelCommand = new Command(async (object view) => await ExecuteCancelCommand(view as Page), (ob) => IsEditMode);
		}

		private async Task ExecuteCancelCommand(Page page)
		{
			await page.Navigation.PopToRootAsync();
		}

		private async Task ExecuteDeleteCommand(Page page)
		{
			var answer = await page.DisplayAlert("Delete", $"Do you wan't to delete {Item.FirstName} {Item.LastName}?", "Yes", "No");
			if (answer)
			{
				await DataStore.DeleteItemAsync(Item.Id);
				MessagingCenter.Send(this, "UserDeleted", Item);
				await page.Navigation.PopToRootAsync();
			}
		}

		bool CanExecuteSaveCommand(object obj)
		{
			if (string.IsNullOrWhiteSpace(Item.FirstName)) return false;
			if (string.IsNullOrWhiteSpace(Item.UserName)) return false;
			return true;
		}

		protected virtual async Task ExecuteSaveItemsCommand(Page view)
		{
			if (await DataStore.UpdateItemAsync(Item))
			{
				MessagingCenter.Send(this, "UserUpdated", Item);
				await view.Navigation.PopToRootAsync();
			}
			else await view.DisplayAlert("Error", DataStore.LastErrorMessage, "Ok");
		}
	}
}
