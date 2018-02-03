using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AzureDemo
{
	public class UsersViewModel : BaseViewModel
	{
		private User _selectedUser;

		public ObservableRangeCollection<User> Items { get; private set; }
		public ICommand LoadItemsCommand { get; private set; }
		public ICommand AddNewCommand { get; private set; }

		public User SelectedUser
		{
			get { return _selectedUser; }
			set { SetProperty(ref _selectedUser, value); }
		}

		public UsersViewModel()
		{
			Title = "List Users";
			Items = new ObservableRangeCollection<User>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			AddNewCommand = new Command(async (object view) => await ExecuteAddNewCommand(view as Page));

			MessagingCenter.Subscribe<NewUserViewModel, User>(this, "UserAdded", (sender, user) =>
			{
				Items.Add(user);
				this.SelectedUser = user;
			});

			MessagingCenter.Subscribe<EditUserViewModel, User>(this, "UserUpdated", (sender, user) =>
			{
				this.SelectedUser = user;
				Items.Replace(user);
			});

			MessagingCenter.Subscribe<EditUserViewModel, User>(this, "UserDeleted", (sender, user) =>
			{
				Items.Remove(user);
				this.SelectedUser = null;
			});
		}

		private async Task ExecuteAddNewCommand(Page view)
		{
			await view.Navigation.PushAsync(new NewUserPage());
		}

		private async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Items.Clear();
				var items = await DataStore.GetItemsAsync(true);
				Items.ReplaceRange(items);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				MessagingCenter.Send(new MessagingCenterAlert
				{
					Title = "Error",
					Message = "Unable to load items.",
					Cancel = "OK"
				}, "message");
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
