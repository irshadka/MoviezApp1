using System;
using Moviez.Common;

namespace Moviez.ViewModels
{
	public class PageBaseViewModel : BaseViewModel
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
	}
}

