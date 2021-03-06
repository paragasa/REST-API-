﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ServerPost.Models;
using ServerPost.Views;
using System.Linq;

namespace ServerPost.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ItemsViewModel _instance;

        public static ItemsViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemsViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Item> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public ItemsViewModel()
        {

            Title = "Item List";
            Dataset = new ObservableCollection<Item>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            MessagingCenter.Subscribe<DeleteItemPage, Item>(this, "DeleteData", async (obj, data) =>
            {
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Item(data);
            });

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddData", async (obj, data) =>
            {
                Dataset.Add(data);
                await DataStore.AddAsync_Item(data);
            });

            MessagingCenter.Subscribe<EditItemPage, Item>(this, "EditData", async (obj, data) =>
            {
                // Find the Item, then update it
                var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
                if (myData == null)
                {
                    return;
                }

                myData.Update(data);
                await DataStore.UpdateAsync_Item(myData);

                _needsRefresh = true;

            });
        }

        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }

            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Item(true);

                // Example of how to sort the database output using a linq query.

                //Sort the list by Name.  What else to add here???
                dataset = dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.Location)
                    .ThenBy(a => a.Attribute)
                    .ThenByDescending(a =>a.Value)
                    .ToList();

                // Then load the data structure
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }
    }
}