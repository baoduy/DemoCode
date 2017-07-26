using System;

using Xamarin.Forms;

namespace AzureDemo
{
	public partial class UsersPage : ContentPage
	{
		UsersViewModel viewModel;

		public UsersPage()
		{
			InitializeComponent();

			BindingContext = this.viewModel = DependencyService.Get<UsersViewModel>();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as User;
			if (item == null)
				return;

			await Navigation.PushAsync(new UserDetailPage(new UserDetailViewModel(item)));

			// Manually deselect item
			//ItemsListView.SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Items.Count == 0)
				viewModel.LoadItemsCommand.Execute(null);
		}
	}
}
