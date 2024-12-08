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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly IWordService wordService;
        private readonly IWordTypeService wordTypeService;
        private int userId;
        private string userName;

        public UserWindow(string username,int userId) 
        {
            InitializeComponent();
            txtWelcome.Text = "Welcome,"; 
            txtUserName.Text = username; 
            this.userId = userId;
            this.userName = username;
            wordService = new WordService();
            wordTypeService = new WordTypeService();
          ;
        }

        private void txtSearchWord_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPlaceholder.Visibility = Visibility.Hidden;
        }

        private void txtSearchWord_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchWord.Text))
            {
                txtPlaceholder.Visibility = Visibility.Visible; 
            }
        }
        public void LoadWordList()
        {
            try
            {
                var wordList = wordService.GetWords();
                dgWords.ItemsSource = wordList;
                txtNoResults.Visibility = wordList.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWordList();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
             string searchTerm = txtSearchWord.Text.Trim();

        if (string.IsNullOrEmpty(searchTerm))
        {
            MessageBox.Show("Vui lòng nhập từ cần tìm.");
            return;
        }

        List<Word> results = wordService.SearchWords(searchTerm);
        dgWords.ItemsSource = results;
        txtNoResults.Visibility = results.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            UserFeedbackWindow feedbackWindow = new UserFeedbackWindow(userId, userName);
            feedbackWindow.Show();
            this.Close();
        }


        private void SignOut_Click(object sender, RoutedEventArgs e)
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
