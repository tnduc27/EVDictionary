using EVDictionary.DAO;
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
    /// Interaction logic for AdminFeedbackWindow.xaml
    /// </summary>
    public partial class AdminFeedbackWindow : Window
    {
        private readonly IFeedbackService feedbackService;
        private readonly string _userName;
        public AdminFeedbackWindow(string userName)
        {
            InitializeComponent();
            feedbackService = new FeedbackService();
            _userName = userName;
        }

        private void LoadFeedbackData()
        {
            var feedbackData = feedbackService.GetFeedbacks();
            FeedbackDataGrid.ItemsSource = feedbackData;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            LoadFeedbackData();
        }
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FeedbackDataGrid.SelectedItem != null)
            {
                var selectedFeedback = (Feedback)FeedbackDataGrid.SelectedItem;

                if (selectedFeedback.Status == "Approved")
                {
                    MessageBox.Show("Phản hồi này đã được phê duyệt.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;  
                }
                else
                {
                    int feedbackId = selectedFeedback.FeedbackId;
                    string feedbackWordText = selectedFeedback.WordText;

                    feedbackService.ApproveFeedback(feedbackId);  

                    var adminWindow = new AdminWindow(_userName);
                    adminWindow.Show();
                    adminWindow.SetApprovedWord(feedbackWordText); 

                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn phản hồi để phê duyệt.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (FeedbackDataGrid.SelectedItem != null)
            {
                var selectedFeedback = (Feedback)FeedbackDataGrid.SelectedItem;

                int feedbackId = selectedFeedback.FeedbackId;
                

                feedbackService.DeleteFeedback(feedbackId);
                LoadFeedbackData();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phản hồi để phê duyệt.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            AdminWindow adminWindow = new AdminWindow(_userName);
            adminWindow.Show();
        }



        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
