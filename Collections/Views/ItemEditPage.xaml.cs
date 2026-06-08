using System;
using System.Collections.ObjectModel;
using Collections.Models;
using Collections.Services;
using Microsoft.Maui.Controls;

namespace Collections.Views
{
    public partial class ItemEditPage : ContentPage
    {
        private CollectionItem _itemBeingEdited;
        private UserCollection _currentCollection;
        private ObservableCollection<UserCollection> _allCollections;
        private FileStorageService _storageService;
        private bool _isNewItem;

        public ItemEditPage(CollectionItem item, UserCollection collection, ObservableCollection<UserCollection> allCollections, FileStorageService storage)
        {
            InitializeComponent();
            _currentCollection = collection;
            _allCollections = allCollections;
            _storageService = storage;

            if (item == null)
            {
                _isNewItem = true;
                _itemBeingEdited = new CollectionItem();
            }
            else
            {
                _isNewItem = false;
                _itemBeingEdited = item;

           
                ItemNameEntry.Text = _itemBeingEdited.Name;
                ItemDetailsEditor.Text = _itemBeingEdited.Details;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            
            _itemBeingEdited.Name = ItemNameEntry.Text;
            _itemBeingEdited.Details = ItemDetailsEditor.Text;

            
            if (_isNewItem)
            {
                _currentCollection.Items.Add(_itemBeingEdited);
            }

            
            _storageService.SaveData(_allCollections);

            
            await Navigation.PopAsync();
        }
    }
}