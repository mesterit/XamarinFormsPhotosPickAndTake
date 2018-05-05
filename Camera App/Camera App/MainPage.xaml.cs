using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
namespace CameraApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        async void TakeAPhotoButton_OnClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            //    AllowCropping = true,
            //    Name = "test.jpg"
            //});

            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            string FileName = "photo" +DateTime.Now.Ticks.ToString() + ".png";
            var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Name = FileName,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CompressionQuality = 92
            };

        var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        async void PickAPhotoButton_OnClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        async void TakeAVideoButton_OnClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Name = "video.mp4",
                Directory = "DefaultVideos",
            });

            if (file == null)
                return;

            await DisplayAlert("Video Recorded", "Location: " + file.Path, "OK");

            file.Dispose();
        }

        async void PickAVideoButton_OnClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                await DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return;

            await DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
            file.Dispose();
        }
    }
}
