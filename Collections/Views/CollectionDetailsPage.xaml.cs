using System;
using System.Collections.ObjectModel;
using Collections.Models;
using Collections.Services;
using Microsoft.Maui.Controls;

namespace Collections.Views
{
    public partial class CollectionDetailsPage : ContentPage
    {
        private UserCollection _currentCollection;
        private ObservableCollection<UserCollection> _allCollections;
        private FileStorageService _storageService;

        public CollectionDetailsPage(UserCollection collection, ObservableCollection<UserCollection> allCollections, FileStorageService storage)
        {
            InitializeComponent();
            _currentCollection = collection;
            _allCollections = allCollections;
            _storageService = storage;

            
            Title = collection.Name;

            ItemsListView.ItemsSource = _currentCollection.Items;
        }

        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemEditPage(null, _currentCollection, _allCollections, _storageService));
        }

        private async void OnEditItemClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button?.CommandParameter is CollectionItem itemToEdit)
            {
                await Navigation.PushAsync(new ItemEditPage(itemToEdit, _currentCollection, _allCollections, _storageService));
            }
        }

        private void OnDeleteItemClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button?.CommandParameter is CollectionItem itemToDelete)
            {
                _currentCollection.Items.Remove(itemToDelete);
                _storageService.SaveData(_allCollections);
            }
        }
    }
}