using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PictureAPI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ApiKey = "37878959-9b15c86ad3efb897de8dddc94";
        private const string ApiUrl = "https://pixabay.com/api/";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FetchImagesButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text;

            // Составляем URL-адрес запроса к API Pixabay
            string url = $"{ApiUrl}?key={ApiKey}&q={WebUtility.UrlEncode(query)}";

            try
            {
                using (WebClient client = new WebClient())
                {
                    // Загружаем данные JSON
                    string json = client.DownloadString(url);

                    // Десериализуем данные JSON в объект
                    PixabayResponse response = JsonConvert.DeserializeObject<PixabayResponse>(json);

                    // Очищаем предыдущие изображения в ListBox
                    ImageListBox.Items.Clear();

                    // Добавляем каждое изображение в ListBox
                    foreach (var image in response.Hits)
                    {
                        ImageListBox.Items.Add(image.WebformatURL);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке изображений: " + ex.Message);
            }
        }
    }

    public class PixabayResponse
    {
        public PixabayImage[] Hits { get; set; }
    }

    public class PixabayImage
    {
        public string WebformatURL { get; set; }
    }
}
