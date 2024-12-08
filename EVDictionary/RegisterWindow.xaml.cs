using EVDictionary.Models;
using EVDictionary.Services;
using Microsoft.Data.SqlClient;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void textUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser.Focus();
        }

        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            textUser.Visibility = Visibility.Collapsed;
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            textUser.Visibility = string.IsNullOrEmpty(txtUser.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = Visibility.Collapsed;
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            textPassword.Visibility = string.IsNullOrEmpty(txtPassword.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void textRPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtRPassword.Focus();
        }

        private void txtRPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            textRPassword.Visibility = Visibility.Collapsed;
        }

        private void txtRPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            textRPassword.Visibility = string.IsNullOrEmpty(txtRPassword.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password cannot be empty.");
                return;
            }

            User newUser = new User
            {
                Username = username,
                Password = password, 
                RoleId = 2
            };

            UserService userService = new UserService();
            bool isRegistered = userService.RegisterUser(newUser); 

            if (isRegistered)
            {              
                LoginWindow loginWindow = new LoginWindow();

                loginWindow.Show();

                MessageBox.Show("Registration successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed. Username already exists.");
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();

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

