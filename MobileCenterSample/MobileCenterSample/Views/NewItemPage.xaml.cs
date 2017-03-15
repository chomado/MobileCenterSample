using System;

using MobileCenterSample.Models;

using Xamarin.Forms;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Analytics;
using System.Collections.Generic;

namespace MobileCenterSample.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is a nice description"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }

        async void Crash_Clicked(object sender, EventArgs e)
        {
            Analytics.TrackEvent(name: "NewItem_ Crash Clicked", properties: new Dictionary<string, string> { { "Category", "Crash" } });
            Crashes.GenerateTestCrash();
        }
    }
}