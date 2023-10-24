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
    public partial class DisplayAllUsersPage : ContentPage
    {
        
        private Users user = new Users();
        public DisplayAllUsersPage()
        {
            InitializeComponent();
        
            LoadUsers();
            LoadSelectedItem();
        }


        public void LoadSelectedItem()
        {
            listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                this.user = (Users)e.SelectedItem;

                // now you can reference item.Name, item.Location, etc
                Navigation.PushAsync(new DisplayOneUserPage(user));

            };
        }
        public async void LoadUsers()
        {
            try
            {
                var myList = await App.DBSQLite.GetAllUsersAsync();
                if (myList != null)
                    listView.ItemsSource = myList;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Take Photo", ex.Message, "Ok");
            }

        }

        private Stream GetStream(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            return new MemoryStream(array);
        }

    }
}