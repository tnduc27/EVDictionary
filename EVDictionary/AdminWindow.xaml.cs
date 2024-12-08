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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EVDictionary
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {      
        private readonly IWordService wordService;
        private readonly IWordTypeService wordTypeService;
        private readonly string _userName;
        public AdminWindow(string userName)
        {
            InitializeComponent();         
            txtWelcome.Text = "Welcome,"; 
            txtAdminName.Text = userName; 
            wordService = new WordService();
            wordTypeService = new WordTypeService();
            _userName = userName;
          
        }
        public void LoadWordTypeList()
        {
            try
            {
                var wtList = wordTypeService.GetWordTypes();
                cbWordType.ItemsSource = wtList;
                cbWordType.DisplayMemberPath = "TypeName";
                cbWordType.SelectedValuePath = "WordTypeId";
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Eror");
            }
            finally
            {
                ClearFields();
            }
        }
        public void LoadWordList()
        {
            try
            {
                var wordList = wordService.GetWords();
                dgWords.ItemsSource = wordList;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Eror");
            }
            finally
            {
                ClearFields();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWordList();
            LoadWordTypeList();
        }
        private void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {          
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid.SelectedItem == null) 
                {
                    return;
                }
                Word selectedWord = dataGrid.SelectedItem as Word;
                if(selectedWord != null) 
                {
                    txtId.Text = selectedWord.WordId.ToString();
                    txtWord.Text = selectedWord.WordText;
                    cbWordType.SelectedValue = selectedWord.WordTypeId;
                    Definition selectedDefinition = selectedWord.Definitions?.FirstOrDefault();
                    if (selectedDefinition != null)
                    {
                        txtMeaning.Text = selectedDefinition.DefinitionText;
                        txtExampleEL.Text = selectedDefinition.ExampleEnglish;
                        txtExampleTV.Text = selectedDefinition.ExampleVietnamese;
                    }
                }

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Eror");
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Word word = new Word();
                word.WordText = txtWord.Text.Trim(); 
                word.WordTypeId = (int)cbWordType.SelectedValue; 

                Definition definition = new Definition
                {
                    DefinitionText = txtMeaning.Text.Trim(),
                    ExampleEnglish = txtExampleEL.Text.Trim(),
                    ExampleVietnamese = txtExampleTV.Text.Trim()
                };

                word.Definitions = new List<Definition> { definition };

                wordService.AddWords(word);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                LoadWordList(); 
            }
        }
        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtId.Text.Length > 0)
                {
                    Word word = new Word();
                    int wordId = int.Parse(txtId.Text);
                    word.WordId  = wordId; 
                    word.WordText = txtWord.Text.Trim(); 
                    word.WordTypeId = (int)cbWordType.SelectedValue; 

                    Definition definition = new Definition
                    {
                        DefinitionText = txtMeaning.Text.Trim(),
                        ExampleEnglish = txtExampleEL.Text.Trim(),
                        ExampleVietnamese = txtExampleTV.Text.Trim()
                    };

                    word.Definitions = new List<Definition> { definition };

                    wordService.UpdateWords(word);
                }
                else
                {
                    MessageBox.Show("You must select a Word!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
            finally
            {
                LoadWordList(); 
            }
        }


        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtId.Text.Length > 0)
                {
                    int wordId = int.Parse(txtId.Text); 

                    wordService.DeleteWords(wordId);
                    MessageBox.Show("Word deleted successfully!");
                }
                else
                {
                    MessageBox.Show("You must select a Word to delete!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                LoadWordList(); 
            }
        }

        public void btnFeedback(object sender, RoutedEventArgs e)
        {
            AdminFeedbackWindow adminFeedbackWindow = new AdminFeedbackWindow(_userName);
            adminFeedbackWindow.Show();
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
        private void ClearFields()
        {
            txtId.Text = string.Empty;
            txtWord.Text = string.Empty;
            cbWordType.SelectedIndex = -1; 
            txtMeaning.Text = string.Empty;
            txtExampleEL.Text = string.Empty;
            txtExampleTV.Text = string.Empty;
        }
        public void SetApprovedWord(string wordText)
        {
            txtWord.Text = wordText;
        }
    }
}
