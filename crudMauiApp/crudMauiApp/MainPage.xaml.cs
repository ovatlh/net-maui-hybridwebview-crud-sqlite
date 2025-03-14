﻿namespace crudMauiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void btnMauiCrudPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MauiCrudPage());
        }

        private async void btnWebCrudPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebCrudPage());
        }

        private async void btnQuasar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuasarPage());
        }
    }

}
