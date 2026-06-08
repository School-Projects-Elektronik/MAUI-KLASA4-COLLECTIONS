using System;
using System.Collections.ObjectModel;
using Collections.Models;
using Collections.Services;
using Microsoft.Maui.Controls;

namespace Collections.Views
{
    public partial class MainPage : ContentPage
    {
        private FileStorageService _storageService;
        private ObservableCollection<UserCollection> _myCollections;

        public MainPage()
        {
            InitializeComponent();
            _storageService = new FileStorageService();

          
            _myCollections = _storageService.LoadData();

            
            CollectionsListView.ItemsSource = _myCollections;
        }

        private void OnAddCollectionClicked(object sender, EventArgs e)
        {
            var newName = CollectionNameEntry.Text;
            if (!string.IsNullOrWhiteSpace(newName))
            {
                _myCollections.Add(new UserCollection { Name = newName });
                _storageService.SaveData(_myCollections);
                CollectionNameEntry.Text = string.Empty;
            }
        }

        private async void OnCollectionTapped(object sender, TappedEventArgs e)
        {
            
            if (e.Parameter is UserCollection selectedCollection)
            {
                await Navigation.PushAsync(new CollectionDetailsPage(selectedCollection, _myCollections, _storageService));
            }
        }
    }
}