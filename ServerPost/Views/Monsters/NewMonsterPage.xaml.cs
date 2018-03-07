using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ServerPost.Models;

namespace ServerPost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMonsterPage : ContentPage
    {
        public Monster Data { get; set; }

        public NewMonsterPage()
        {
            InitializeComponent();

            Data = new Monster
            {
                Name = "Monster name",
                Description = "This is a Monster description.",
                Id = Guid.NewGuid().ToString()
            };

            BindingContext = this;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}