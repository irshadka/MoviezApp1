using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Moviez.Common;

namespace Moviez.ViewModels
{
    public partial class PageBaseViewModel : ObservableObject
    {
        #region Variables
        private bool _isBusy;
        private bool noDataLabelVisible;
        private string _noDataMessage;
        #endregion


        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }


        public bool NoDataLabelVisible
        {
            get { return noDataLabelVisible; }
            set { SetProperty(ref noDataLabelVisible, value); }
        }


        public string NoDataMessage
        {
            get => _noDataMessage;
            set => SetProperty(ref _noDataMessage, value);
        }
        public PageBaseViewModel()
        {
        }

        public async Task DisplayAlert(string title, string message)
        {
            await Shell.Current.DisplayAlert(title, message, AppResources.OkButton);
        }
        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

