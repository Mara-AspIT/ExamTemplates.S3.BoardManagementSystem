using CrossCutting.Entities;
using System.Windows;

namespace Client.UI
{

	public partial class TestWindow : Window
	{
		private User user;

		public TestWindow( User user )
		{
			InitializeComponent();
			this.user = user;
			label.Content = $"Welcome, {user.Username}.";
		}
	}
}//