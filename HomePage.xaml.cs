using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XF_UsingCamera.Model;

namespace XF_UsingCamera
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string CurrentImageBase64 { get; set; }
        Users user = new Users();
        public HomePage()
        {
            InitializeComponent();

            DisplayAll.Clicked += (s,e) => Navigation.PushAsync(new DisplayAllUsersPage());
        }

        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                    var current = CrossMedia.Current;
                if (current.IsCameraAvailable && current.IsTakePhotoSupported)
                {
                    var photo = await current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        CompressionQuality = 70
                    });

                    img1.Source = ImageSource.FromStream(() =>
                    {
                        var stream = photo.GetStream();
                        CurrentImageBase64 = GetBase64(photo.GetStream());

                        user.Img1 = CurrentImageBase64;
                        photo.Dispose();
                        return stream;
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }

        private string GetBase64(Stream stream)
        {
            byte[] array;
            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                array = memory.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        private Stream GetStream(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            return new MemoryStream(array);
        }

        private async void BtnPickPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var current = Plugin.Media.CrossMedia.Current;
                if (current.IsPickPhotoSupported )
                {
                    var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new
                                  Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                    });
                    if (photo == null)
                        return;
                    img2.Source = ImageSource.FromStream(() =>
                    {
                        var stream = photo.GetStream();
                        CurrentImageBase64 = GetBase64(photo.GetStream());
                        user.Img2 = CurrentImageBase64;
                        photo.Dispose();
                        return stream;
                    });
                                    
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }
        }

        private async void BtnSaveProduct_Clicked(object sender, EventArgs e)
        {
            try
            {
                user.Name = Name.Text;
                await App.DBSQLite.SaveUserAsync(user);
                await Navigation.PushAsync(new DisplayAllUsersPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Save Product", ex.Message, "Ok");
            }
        }
    }
}