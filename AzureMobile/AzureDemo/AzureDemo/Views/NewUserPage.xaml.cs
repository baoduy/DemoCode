using System;

using Xamarin.Forms;

namespace AzureDemo
{
	public partial class NewUserPage : ContentPage
	{
		//NewUserViewModel viewModel;

		public NewUserPage(BaseViewModel viewModel = null)
		{
			InitializeComponent();

			if (viewModel == null)
				viewModel = new NewUserViewModel();

			BindingContext = viewModel;
		}
	}
}
