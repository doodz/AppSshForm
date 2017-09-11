using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApptestSsh.Core.View.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestContentView : ContentView
    {
        // public event handler to expose 
        // the Ok button's click event
        public EventHandler CloseButtonEventHandler { get; set; }
        // public string to expose the 
        // text Entry input's value
        public string TextInputResult { get; set; }

        public TestContentView(string titleText,
            string placeHolderText, string closeButtonText,
            string validationLabelText)
        {
            InitializeComponent();

            // update the Element's textual values
            TitleLabel.Text = titleText;
            InputEntry.Placeholder = placeHolderText;
            CloseButton.Text = closeButtonText;
            ValidationLabel.Text = validationLabelText;

            // handling events to expose to public
            CloseButton.Clicked += CloseButton_Clicked;
            InputEntry.TextChanged += InputEntry_TextChanged;
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            CloseButtonEventHandler?.Invoke(this, e);
        }

        private void InputEntry_TextChanged(object sender,
            TextChangedEventArgs e)
        {
            // update the public string value 
            // accordingly to the text Entry's value
            TextInputResult = InputEntry.Text;
        }

        public static readonly BindableProperty
            IsValidationLabelVisibleProperty =
                BindableProperty.Create(
                    nameof(IsValidationLabelVisible),
                    typeof(bool),
                    typeof(TestContentView),
                    false, BindingMode.OneWay, null,
                    (bindable, value, newValue) =>
                    {
                        if ((bool)newValue)
                        {

                            ((TestContentView)bindable).ValidationLabel
                                .IsVisible = true;
                        }
                        else
                        {

                            ((TestContentView)bindable).ValidationLabel
                                .IsVisible = false;
                        }
                    });

        /// <summary>
        /// Gets or Sets if the ValidationLabel is visible
        /// </summary>
        public bool IsValidationLabelVisible
        {
            get
            {
                return (bool)GetValue(
                    IsValidationLabelVisibleProperty);
            }
            set
            {
                SetValue(
                    IsValidationLabelVisibleProperty, value);
            }
        }
    }
}