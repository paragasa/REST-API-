
using System;
using ServerPost.Services;
using ServerPost.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ServerPost.ViewModels;
using ServerPost.Models;

namespace ServerPost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            UseMockDataSource.IsToggled = false;
            SetDataSource(UseMockDataSource.IsToggled);

            // Example of how to add an view to an existing set of XAML. 
            // Give the Xaml layout you want to add the data to a good x:Name, so you can access it.  Here "DateRoot" is what I am using.
            var dateLabel = new Label
            {
                Text = DateTime.Now.ToShortDateString(),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "Helvetica Neue",
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                TextColor = Color.Black,
            };

            DateRoot.Children.Add(dateLabel);

        }
        
        private void SetDataSource(bool isMock)
        {
            if (isMock == true)
            {
                ItemsViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                MonstersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                CharactersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
                ScoresViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Mock);
            }
            else
            {
                ItemsViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                MonstersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                CharactersViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
                ScoresViewModel.Instance.SetDataStore(BaseViewModel.DataStoreEnum.Sql);
            }

            // Have data refresh...
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        private void UseMockDataSourceSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.
            SetDataSource(e.Value);
        }

        private async void ClearDatabase_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Sure you want to Delete All Data, and start over?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables.
                SQLDataStore.Instance.InitializeDatabaseNewTables();
            }
        }


        // Add code for GetItems_Command
        // add your code here

        private async void GetItems_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Get", "Sure you want to Get Items from the Server?", "Yes", "No");
            if (answer)
            {
                // Call to the Item Service and have it Get the Items
                ItemsController.Instance.GetItemsFromServer();
            }
        }

        private async void GetItemsPost_Command(object sender, EventArgs e)
        {
            var number = 10; // Want to get 10 items from the server
            var level = 20;
            var attribute = AttributeEnum.Unknown;
            var location = ItemLocationEnum.Unknown;
            var random = true;
            var updateDataBase = true;

            var myDataList = await ItemsController.Instance.GetItemsFromServerPost(number, level, attribute, location, random, updateDataBase);

            var myOutput = string.Empty;
            foreach (var item in myDataList)
            {
                // Build up the output list by appending.
                // use "\n"; to add a line seperator at the end of each item
                myOutput += item.FormatOutput() + "\n";
            }

            var answer = await DisplayAlert("Returned List", myOutput, "Yes", "No");
        }
    }
}