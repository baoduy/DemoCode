namespace AzureDemo
{
	public class User : ObservableObject
	{
		int id = 0;

		public int Id
		{
			get { return id; }
			set { SetProperty(ref id, value); }
		}

		string userName = string.Empty;
		public string UserName
		{
			get { return userName; }
			set { SetProperty(ref userName, value); }
		}

		string firstName = string.Empty;
		public string FirstName
		{
			get { return firstName; }
			set { SetProperty(ref firstName, value); }
		}

		string lastName = string.Empty;
		public string LastName
		{
			get { return lastName; }
			set { SetProperty(ref lastName, value); }
		}

		string description = string.Empty;
		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}
	}
}
