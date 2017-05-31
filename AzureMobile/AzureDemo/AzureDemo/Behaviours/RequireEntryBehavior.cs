using System;
using Xamarin.Forms;

namespace AzureDemo.Behaviors
{
	public class RequireEntryBehavior:Behavior<Entry>
	{
		protected override void OnAttachedTo(Entry bindable)
		{
			bindable.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(bindable);
		}

		protected override void OnDetachingFrom(Entry bindable)
		{
			bindable.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(bindable);
		}

		private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{
			bool isValid = args.NewTextValue.Length > 0;
			((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.Yellow;
		}
	}
}
