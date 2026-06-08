using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Collections.Models;

namespace Collections.Services
{
    public class FileStorageService
    {
        private readonly string _filePath;

        public FileStorageService()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CollectionsData.txt");

            Debug.WriteLine("==================================================");
            Debug.WriteLine($"[ŚCIEŻKA DO PLIKU DANYCH]: {_filePath}");
            Debug.WriteLine("==================================================");
        }

        public ObservableCollection<UserCollection> LoadData()
        {
            var collections = new ObservableCollection<UserCollection>();
            if (!File.Exists(_filePath))
                return collections;

            string[] lines = File.ReadAllLines(_filePath);
            UserCollection currentCollection = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.StartsWith("COLLECTION|"))
                {
                    string name = line.Substring("COLLECTION|".Length);
                    currentCollection = new UserCollection { Name = name };
                    collections.Add(currentCollection);
                }
                else if (line.StartsWith("ITEM|") && currentCollection != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 4)
                    {
                        currentCollection.Items.Add(new CollectionItem
                        {
                            Id = parts[1],
                            Name = parts[2],
                            Details = parts[3]
                        });
                    }
                }
            }
            return collections;
        }

        public void SaveData(IEnumerable<UserCollection> collections)
        {
            using StreamWriter writer = new StreamWriter(_filePath, false);
            foreach (var collection in collections)
            {
                writer.WriteLine($"COLLECTION|{collection.Name}");
                foreach (var item in collection.Items)
                {
                    string safeName = item.Name?.Replace("|", " ") ?? "Brak nazwy";
                    string safeDetails = item.Details?.Replace("|", " ") ?? "Brak opisu";

                    writer.WriteLine($"ITEM|{item.Id}|{safeName}|{safeDetails}");
                }
            }
        }
    }
}