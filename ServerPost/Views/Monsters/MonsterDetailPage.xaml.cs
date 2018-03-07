using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ServerPost.Models;
using ServerPost.ViewModels;

namespace ServerPost.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MonsterDetailPage : ContentPage
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private MonstersDetailViewModel _viewModel;

        public MonsterDetailPage(MonstersDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public MonsterDetailPage()
        {
            InitializeComponent();

            var data = new Monster
            {
                Name = "Item 1",
                Description = "This is an item description."
            };

            _viewModel = new MonstersDetailViewModel(data);
            BindingContext = _viewModel;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditMonsterPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeleteMonsterPage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}