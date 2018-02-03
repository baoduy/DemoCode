using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AzureDemo
{
	public class UserDetailViewModel : BaseViewModel
	{
		public User Item { get; set; }
		public ICommand EditCommand { get; private set; }

		public UserDetailViewModel(User item)
		{
			EditCommand = new Command(async (object view) => await ExecuteEditCommand(view as Page));

			Title = item.FirstName;
			Item = item;
		}

		private async Task ExecuteEditCommand(Page view)
		{
			await view.Navigation.PushAsync(new NewUserPage(new EditUserViewModel(Item)));
		}

		int quantity = 1;
		public int Quantity
		{
			get { return quantity; }
			set { SetProperty(ref quantity, value); }
		}
	}
}
