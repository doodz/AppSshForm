﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.TestUi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage1 : ContentPage
    {
        public ListViewPage1()
        {
            InitializeComponent();
            BindingContext = new ListViewPage1ViewModel();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect PluginFormsItem
            ((ListView)sender).SelectedItem = null;
        }
    }



    class ListViewPage1ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; }

        public ListViewPage1ViewModel()
        {
            Items = new ObservableCollection<Item>(new[]
            {
                new Item { Text = "Baboon", Detail = "Africa & Asia" },
                new Item { Text = "Capuchin Monkey", Detail = "Central & South America" },
                new Item { Text = "Blue Monkey", Detail = "Central & East Africa" },
                new Item { Text = "Squirrel Monkey", Detail = "Central & South America" },
                new Item { Text = "Golden Lion Tamarin", Detail= "Brazil" },
                new Item { Text = "Howler Monkey", Detail = "South America" },
                new Item { Text = "Japanese Macaque", Detail = "Japan" },
            });

            var sorted = from item in Items
                         orderby item.Text
                         group item by item.Text[0].ToString() into itemGroup
                         select new Grouping<string, Item>(itemGroup.Key, itemGroup);

            ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sorted);

            RefreshDataCommand = new Command(
                async () => await RefreshData());
        }

        public ICommand RefreshDataCommand { get; }

        async Task RefreshData()
        {
            IsBusy = true;
            //Load Data Here
            await Task.Delay(2000);

            IsBusy = false;
        }

        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public class Item
        {
            public string Text { get; set; }
            public string Detail { get; set; }

            public override string ToString() => Text;
        }

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }
    }
}