using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstMauiApp.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        IConnectivity _connectivity;
        public MainViewModel(IConnectivity connectivity)
        {
            Items = new ObservableCollection<string>();
            _connectivity = connectivity;
        }

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [RelayCommand]
        async void Add()
        {
            if(string.IsNullOrEmpty(Text))
            {
                return; 
            }

            if(_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Oh!", "No Internet", "OK");
                return;
            }

            Items.Add(Text);
            // add our item
            Text = string.Empty;
        }


        [RelayCommand]
        void Delete(string s)
        {
            if(Items.Contains(s))
            {
                Items.Remove(s);
            }
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }
    }
}
