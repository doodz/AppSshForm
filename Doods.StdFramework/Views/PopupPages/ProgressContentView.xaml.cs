using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.StdFramework.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressContentView : ContentView
    {

        /// <summary>
        /// public event handler to expose 
        /// the Ok button's click event
        /// </summary>
        public EventHandler CloseButtonEventHandler { get; set; }
        public ProgressContentView(string titleText)
        {
            InitializeComponent();
            TitleLabel.Text = titleText;
            // handling events to expose to public
            CloseButton.Clicked += CloseButton_Clicked;
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            CloseButtonEventHandler?.Invoke(this, e);
        }
    }
}