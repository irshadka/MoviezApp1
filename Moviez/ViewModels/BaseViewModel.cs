// using System;
// using System.ComponentModel;
// using CommunityToolkit.Mvvm.ComponentModel;

// namespace Moviez.ViewModels
// {
//     public class BaseViewModel : ObservableObject
//     {
//         //#region Public Methods
//         //public new event PropertyChangedEventHandler PropertyChanged;
//         //#endregion

//         //#region Protected Methods
//         ///// <summary>
//         ///// OnPropertyChanged Event
//         ///// </summary>
//         ///// <param name="propertyName"></param>
//         //protected new void OnPropertyChanged(string propertyName = null)
//         //{
//         //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//         //}
//         ///// <summary>
//         ///// SetProperty Event
//         ///// </summary>
//         ///// <typeparam name="T"></typeparam>
//         ///// <param name="field"></param>
//         ///// <param name="value"></param>
//         ///// <param name="propertyName"></param>
//         ///// <param name="onChanged"></param>
//         ///// <returns></returns>
//         //protected bool SetProperty<T>(ref T field, T value, string propertyName = null, Action onChanged = null)
//         //{
//         //    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
//         //    field = value;

//         //    onChanged?.Invoke();

//         //    OnPropertyChanged(propertyName);
//         //    return true;
//         //}
//         //#endregion

//         #region Constructor
//         /// <summary>
//         /// ViewModelBase Constructor
//         /// </summary>
// 		public BaseViewModel()
//         {
//         }
//         #endregion
//     }
// }


