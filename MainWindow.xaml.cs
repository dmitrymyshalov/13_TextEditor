using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void MenuItemSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToggleButtonBold_Checked(object sender, RoutedEventArgs e)
        {
            textBox.FontWeight = FontWeights.Bold;
        }

        private void ToggleButtonBold_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox.FontWeight = FontWeights.Regular;
        }

        private void ToggleButtonItalic_Checked(object sender, RoutedEventArgs e)
        {
            textBox.FontStyle = FontStyles.Italic;
        }

        private void ToggleButtonItalic_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox.FontStyle = FontStyles.Normal;
        }

        private void ToggleButtonUnderline_Checked(object sender, RoutedEventArgs e)
        {
            textBox.TextDecorations.Add(TextDecorations.Underline);
        }

        private void ToggleButtonUnderline_Unchecked(object sender, RoutedEventArgs e)
        {
            TextDecoration decoration = TextDecorations.Underline.First(item => item.Location == TextDecorationLocation.Underline);
            textBox.TextDecorations.Remove(decoration);
        }

        private void RadioButtonBlack_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.Black;
            }
        }

        private void RadioButtonRed_Checked(object sender, RoutedEventArgs e)
        {
            textBox.Foreground = Brushes.Red;
        }

        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            string tag = ((sender as ComboBox).SelectedItem as ComboBoxItem).Tag as string;
            string uriString;
            switch (tag)
            {
                case "Light":
                    uriString = "LightTheme.xaml";
                    break;
                case "Dark":
                    uriString = "DarkTheme.xaml";
                    break;
                default:
                    MessageBox.Show("Для выбранной темы не задан набор стилей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
            }
            Uri uri = new Uri(uriString, UriKind.Relative);

            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
    }
}