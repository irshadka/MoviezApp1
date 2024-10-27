using System.Diagnostics;
using Camera.MAUI;
using Camera.MAUI.ZXing;
using Moviez.Common;
using Moviez.ViewModels;

namespace Moviez.Views;

public partial class HomePage : ContentPage
{
    private HomePageViewModel _vmodel;

    public HomePage(HomePageViewModel viewModel)
    {
        InitializeComponent();
        _vmodel = viewModel;
        this.BindingContext = viewModel;
    }

    void cameraView_CamerasLoaded(System.Object sender, System.EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                //await Task.Delay(500);
                //await cameraView.StopCameraAsync();
                //await cameraView.StartCameraAsync();
                cameraView.BarcodeDetected += CameraView_BarcodeDetected;
                cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();
                cameraView.BarCodeOptions = new BarcodeDecodeOptions
                {
                    AutoRotate = true,
                    PossibleFormats = { BarcodeFormat.QR_CODE },
                    ReadMultipleCodes = true,
                    TryHarder = true,
                    TryInverted = true
                };
                cameraView.BarCodeDetectionFrameRate = 10;
                cameraView.BarCodeDetectionMaxThreads = 5;
                cameraView.ControlBarcodeResultDuplicate = true;
                cameraView.BarCodeDetectionEnabled = true;
            });
        }
    }
    private async void CameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            Debug.WriteLine("BarcodeText=" + args.Result[0].Text);
            SearchBar.Text = args.Result[0].Text;
            await cameraView.StopCameraAsync();
            cameraView.IsVisible = false;
            _vmodel.SearchMoviesCommand.Execute(SearchBar.Text);
        });
      
    }

    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        if (cameraView.IsVisible)
        {
            cameraView.IsVisible = false;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
            });
            return;
        }
            
        bool isCameraAvailable = MediaPicker.Default.IsCaptureSupported;
        if (isCameraAvailable)
        {
            Console.WriteLine("Camera is available.");
            // You can proceed with capturing an image
            cameraView.IsVisible = true;
            cameraView.BarCodeDetectionEnabled = true;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StartCameraAsync();
            });
                //await cameraView.StopCameraAsync();
                //await cameraView.StartCameraAsync();
            }
        else
        {
            Console.WriteLine("Camera is not available on this device.");
           await Shell.Current.DisplayAlert(AppResources.AppTitle, "Camera is not available on this device.", AppResources.OkButton);
            // Handle the case where the camera is unavailable
        }
    }

    void SearchBar_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }
}
