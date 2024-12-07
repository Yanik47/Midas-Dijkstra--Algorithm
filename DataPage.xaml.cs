using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using Windows.Storage.Pickers;
using Windows.Storage;
using AiSD2_4.Algorithms;
using AiSD2_4.DijkstraAlgorithms;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AiSD2_4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataPage : Page
    {
        
        class FormInfo : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            
            private string dataSourceSize = "Count of alchemistry operation";
            private string dataAlchemistrySize = "Count of metal";
            public List<string> SourceData = new List<string>();
            public List<string> AlchemistryData = new List<string>();
            public string DataSourceSize 
            { 
                get => dataSourceSize;
                set { dataSourceSize = value; OnPropertyChanged("dataSourceSize"); } 
            }
            public string DataAlchemistrySize 
            {
                get => dataAlchemistrySize;
                set { dataAlchemistrySize = value; OnPropertyChanged("dataAlchemistrySize"); }
            }

            private double sliderValue;

            public double SliderValue
            {
                get => sliderValue;
                set
                {
                    sliderValue = value;
                    OnPropertyChanged("SliderValue");
                }
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
        public DataPage()
        {
            this.formInfo = new FormInfo();
            this.InitializeComponent();
            this.DataContext = this.formInfo;
        }


        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Content = null;
        }

        private void txt_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void DataSourceButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fSourcePick = new FileOpenPicker();
            fSourcePick.ViewMode = PickerViewMode.Thumbnail;
            fSourcePick.SuggestedStartLocation = PickerLocationId.Desktop;
            fSourcePick.FileTypeFilter.Add(".txt");

            StorageFile file1 = await fSourcePick.PickSingleFileAsync();

            if (file1 != null)
            {
                Stream stream = await file1.OpenStreamForReadAsync();
                StreamReader reader = new StreamReader(stream);
                string line;
                while((line = await reader.ReadLineAsync()) != null)
                {
                    formInfo.SourceData.Add(line);
                }
                reader.Dispose();
                stream.Dispose();
            }
        }

        private async void DataAlchemistryButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fpick = new FileOpenPicker();
            fpick.ViewMode = PickerViewMode.Thumbnail;
            fpick.SuggestedStartLocation = PickerLocationId.Desktop;
            fpick.FileTypeFilter.Add(".txt");

            StorageFile file2 = await fpick.PickSingleFileAsync();

            if (file2 != null)
            {
                Stream stream = await file2.OpenStreamForReadAsync();
                StreamReader reader = new StreamReader(stream);
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    formInfo.AlchemistryData.Add(line);
                }
                reader.Dispose();
                stream.Dispose();
            }
           
        }

        private void MakeMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            Longshuuuu.Visibility = Visibility.Visible;

            Longshuuuu.Play();

            string sourceDataSize = formInfo.DataSourceSize;
            string alchemistryDataSize = formInfo.DataAlchemistrySize;
            List<string> sourceData = formInfo.SourceData;
            List<string> alchemistryData = formInfo.AlchemistryData;
            int countofMetal = 0;
            int countofAlchemistry = 0;
            double weight = formInfo.SliderValue;
            try
            {
                countofMetal = int.Parse(sourceDataSize);
                countofAlchemistry = int.Parse(alchemistryDataSize);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Data type error, type of mata must be int");
            }
            Response response;

            response = MakeMoneyClass.MakeMoney(countofAlchemistry, countofMetal, weight, sourceData, alchemistryData);

            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(ResultsPage), response);
            }
        }

    }
}
