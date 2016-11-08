// Author: Mads Mikkel Rasmussen.

using Client.Controllers;
using CrossCutting.Entities;
using CrossCutting.Networking;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.UI
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml. Inherits System.Windows.Window. This class cannot be inherited.
	/// </summary>
	public sealed partial class LoginWindow : Window
	{

		#region Fields
		/// <summary>
		/// The controller used to log in on the remote server.
		/// </summary>
		private LoginController controller;
		#endregion


		#region Constructor
		/// <summary>
		/// Initializes a new instance of this class and displays the window.
		/// </summary>
		public LoginWindow()
		{
			InitializeComponent();
			logInButton.IsEnabled = false;
			controller = LoginController.Instance;
		}
		#endregion


		#region Event listeners
		/// <summary>
		/// Attempts server login. If success, the window belonging to the user type is opened.
		/// </summary>
		/// <param name="sender">The button control.</param>
		/// <param name="e">Not used.</param>
		private void LogInButton_Click( object sender, RoutedEventArgs e )
		{
			errorMessageLabel.Content = "Logging in...";
			UserCredentials credentials = new UserCredentials( usernameTextBox.Text, passwordBox.Password );
			User user = null;
			LogInRequestStatus loginResult = LogInRequestStatus.Default;
			try
			{
				loginResult = controller.TryLogin( credentials, out user );
			}
			catch( ArgumentException )
			{
				// Maybe log locally and transmit to server when possible.

			}

			switch( loginResult )
			{
				case LogInRequestStatus.Success:
					if( user == null )
					{
						throw new NullReferenceException( "The instance reference to the logged in user was null" );
					}
					else if( user is TestUserType )
					{
						TestWindow newWindowForTestUsers = new TestWindow( user );
						newWindowForTestUsers.Show();
						this.Close();
					}
					// else if user is secretary, chairman etc.: Open the appropriate window.
					break;
				case LogInRequestStatus.WrongCredentials:
					errorMessageLabel.Content = "Username or password incorrect. Please try again.";
					break;
				//case LoginAttemptResult.NetworkError:
				//	errorMessageLabel.Content = "Could not contact server du to a network error. Please try again later.";
				//	break;
				case LogInRequestStatus.Default: // Fall through:
				default:
					errorMessageLabel.Content = "An unexpected error occurred. Please try again.";
					break;
			}
		}
		#endregion

		#region Helpers
		// Helper to disable user from clicking the button before something is entered.
		private void UsernameTextBox_TextChanged( object sender, TextChangedEventArgs e )
		{

			if( BothBoxesAreEmpty() )
			{
				logInButton.IsEnabled = false;
			}
			else
			{
				logInButton.IsEnabled = true;
			}
		}

		// Helper to disable user from clicking the button before something is entered.
		private void PasswordBox_PasswordChanged( object sender, RoutedEventArgs e )
		{
			if( BothBoxesAreEmpty() )
			{
				logInButton.IsEnabled = false;
			}
			else
			{
				logInButton.IsEnabled = true;
			}
		}


		private bool BothBoxesAreEmpty()
		{
			return String.IsNullOrWhiteSpace( usernameTextBox.Text ) || String.IsNullOrWhiteSpace( passwordBox.Password );
		}
		#endregion
	}
}