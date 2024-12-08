using EVDictionary.DAO;
using EVDictionary.Models;
using EVDictionary.Services;
using System;
using System.Windows;
using System.Windows.Media;

namespace EVDictionary
{
    public partial class UserFeedbackWindow : Window
    {
        private readonly IFeedbackService _feedbackService;
        private readonly int _userId; 
        private readonly string _userName;

        public UserFeedbackWindow(int userId, string userName)
        {
            InitializeComponent();
            _feedbackService = new FeedbackService();
            _userId = userId; 
            _userName = userName;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Feedback feedback = new Feedback
                {
                    UserId = _userId, 
                    WordText = txtWordText.Text.Trim(),
                    FeedbackText = txtFeedbackText.Text.Trim(),
                    Status = "Pending"
                };

                _feedbackService.SendFeedback(feedback);

                txtStatus.Text = "Feedback submitted successfully!";
                txtStatus.Foreground = new SolidColorBrush(Colors.Green);
                ClearForm();
            }
            catch (Exception ex)
            {
                txtStatus.Text = $"An error occurred: {ex.Message}";
                txtStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            UserWindow userWindow = new UserWindow(_userName, _userId);
            userWindow.Show();
        }


        private void ClearForm()
        {
            txtWordText.Clear();
            txtFeedbackText.Clear();
        }
    }
}
