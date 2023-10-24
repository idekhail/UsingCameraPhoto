using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XF_UsingCamera.Model;

namespace XF_UsingCamera
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayOneUserPage : CarouselPage
    {
        public DisplayOneUserPage(Users user)
        {
            InitializeComponent();


            img1.Source = ImageSource.FromStream(() =>
            {
                var stream = GetStream(user.Img1);
                return stream;
            });

            img2.Source = ImageSource.FromStream(() =>
            {
                var stream = GetStream(user.Img2);
                return stream;
            });
        }
        public Stream GetStream(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            return new MemoryStream(array);
        }
    }
}