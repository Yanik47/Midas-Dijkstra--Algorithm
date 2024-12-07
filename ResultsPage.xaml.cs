using AiSD2_4.DijkstraAlgorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AiSD2_4.Algorithms;
using System.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AiSD2_4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class ResultsPage : Page
    {

        class FormInfo : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private string dataGold = "";
            private string data = "";
            public string DataGold
            {
                get => dataGold;
                set { dataGold = value; OnPropertyChanged("DataGold"); }
            }
            public string Data
            {
                get => data;
                set { data = value; OnPropertyChanged("Data"); }
            }


            void OnPropertyChanged(params string[] propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    foreach (var name in propertyName)
                    {
                        handler(this, new PropertyChangedEventArgs(name));

                    }
                }
            }
        }

        private FormInfo formInfo;
        public ResultsPage()
        {
            this.formInfo = new FormInfo();
            this.InitializeComponent();
            this.DataContext = this.formInfo;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Response response)
            {
                SetData(response);
            }
        }

        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Content = null;
        }

        public void SetData(Response response)
        {

            string zarobek = response.zarobek.ToString();
            string zarobekGold = response.zarobekGold.ToString();

            Dictionary<int, int> Ile = response.Ile;
            Dictionary<int, int> IleGold = response.IleAleGold;

            string goldData = "ZarobekGold: " + zarobekGold + "\n";
            string similarData = "Zarobek: " + zarobek + "\n";

            System.Diagnostics.Debug.WriteLine("Zarobek: " + zarobek + "\n");
            System.Diagnostics.Debug.WriteLine("ZarobekGold: " + zarobekGold + "\n");

            foreach (KeyValuePair<int, int> entry in Ile)
            {
                System.Diagnostics.Debug.WriteLine("Ile: Numer metalu: " + entry.Key + " Ilosc kg: " + entry.Value + "\n");
                similarData += "Ile: Numer metalu: " + entry.Key + " Ilosc kg: " + entry.Value + "\n";
            }
            foreach (KeyValuePair<int, int> entry in IleGold)
            {
                System.Diagnostics.Debug.WriteLine("IleGold: Numer metalu: " + entry.Key + " Ilosc kg: " + entry.Value);
                goldData += "IleGold: Numer metalu: " + entry.Key + " Ilosc kg: " + entry.Value;
            }
            //System.Diagnostics.Debug.WriteLine("Final gold data: " + goldData);
            //System.Diagnostics.Debug.WriteLine("Final similar data: " + similarData);
            formInfo.Data = similarData;
            formInfo.DataGold = goldData;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
    }
}
