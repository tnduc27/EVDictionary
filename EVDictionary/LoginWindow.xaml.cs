using EVDictionary.Models;
using EVDictionary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EVDictionary
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;
        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void textUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser.Focus();
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text) && txtUser.Text.Length > 0)
            {
                textUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUser.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }


        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User account = _userService.GetUserByName(txtUser.Text); 

                if (account == null)
                {
                    MessageBox.Show("User not found.");
                    return;
                }

                if (!account.Password.Equals(txtPassword.Password))
                {
                    MessageBox.Show("Incorrect password.");
                    return;
                }

                if (account.RoleId == 1) 
                {
                    this.Hide();
                    AdminWindow adminWindow = new AdminWindow(txtUser.Text);
                    adminWindow.Show();
                }
                else if (account.RoleId == 2) 
                {
                    this.Hide();
                    UserWindow userWindow = new UserWindow(txtUser.Text, account.UserId); 
                    userWindow.Show();
                }
                else
                {
                    MessageBox.Show("You do not have access to this application.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();

            registerWindow.Show();

            this.Close();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

