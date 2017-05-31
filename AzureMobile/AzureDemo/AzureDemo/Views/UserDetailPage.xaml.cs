using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AzureDemo
{
	public partial class UserDetailPage : ContentPage
	{
		//ItemDetailViewModel viewModel;

		public UserDetailPage(UserDetailViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = viewModel;
		}
	}
}
