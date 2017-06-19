using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Doods.StdFramework.System.ComponentModel;
using Xamarin.Forms;

namespace Doods.StdFramework.Views
{
    public partial class DoodsEntryBox : ContentView, INotifyPropertyChanged
    {

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(DoodsEntryBox), true,propertyChanged: OnHasBorderChanged);

	    private static void OnHasBorderChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(HasBorder));

		}

	    public bool HasBorder
        {
            get => (bool)GetValue(HasBorderProperty);
	        set => SetValue(HasBorderProperty, value);
        }

        public static readonly BindableProperty FontProperty =
            BindableProperty.Create(nameof(Font), typeof(Font), typeof(DoodsEntryBox), new Font(), propertyChanged: OnFontChanged);

	    private static void OnFontChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(Font));
		}

	    public Font Font
        {
            get => (Font)GetValue(FontProperty);
	        set => SetValue(FontProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(DoodsEntryBox), null, propertyChanged: OnFontFamilyChanged);

	    private static void OnFontFamilyChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(FontFamily));
		}

	    public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
	        set => SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(DoodsEntryBox), int.MaxValue, propertyChanged: OnMaxLengthChanged);

	    private static void OnMaxLengthChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(MaxLength));
		}

	    public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
	        set => SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create(nameof(XAlign), typeof(TextAlignment), typeof(DoodsEntryBox), TextAlignment.Start, propertyChanged: OnXAlignChanged);

	    private static void OnXAlignChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(XAlign));
		}

	    public TextAlignment XAlign
        {
            get => (TextAlignment)GetValue(XAlignProperty);
	        set => SetValue(XAlignProperty, value);
        }


		public static readonly BindableProperty TextColorProperty =
		    BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(DoodsEntryBox), Color.Black, propertyChanged: OnTextColorChanged);

	    private static void OnTextColorChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(TextColor));
		}

	    public Color TextColor
		{
		    get => (Color)GetValue(TextColorProperty);
		    set => SetValue(TextColorProperty, value);
	    }

	    public static readonly BindableProperty TextProperty =
		    BindableProperty.Create(nameof(Text), typeof(string), typeof(DoodsEntryBox), string.Empty, propertyChanged: OnTextChanged);

	    private static void OnTextChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(Text));
	    }

		public string Text
	    {
		    get => (string)GetValue(TextProperty);
		    set => SetValue(TextProperty, value);
	    }

	    public static readonly BindableProperty LabelProperty =
		    BindableProperty.Create(nameof(Label), typeof(string), typeof(DoodsEntryBox), string.Empty, propertyChanged: OnLabelChanged);

	    private static void OnLabelChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(Label));
		}

	    public string Label
	    {
		    get => (string)GetValue(LabelProperty);
		    set => SetValue(LabelProperty, value);
	    }

	    public static readonly BindableProperty PlaceholderProperty =
		    BindableProperty.Create(nameof(PlaceholderProperty), typeof(string), typeof(DoodsEntryBox), string.Empty, propertyChanged: OnPlaceholderChanged);

	    private static void OnPlaceholderChanged(BindableObject bindable, object oldvalue, object newvalue)
	    {
			((DoodsEntryBox)bindable).SetPropertyChanged(nameof(Placeholder));
		}


	    public string Placeholder
		{
		    get => (string)GetValue(PlaceholderProperty);
		    set => SetValue(PlaceholderProperty, value);
	    }



        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(DoodsEntryBox), false, propertyChanged: OnIsPasswordChanged);

        private static void OnIsPasswordChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((DoodsEntryBox)bindable).SetPropertyChanged(nameof(IsPassword));
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }



        public DoodsEntryBox()
        {
			
	        BindingContext = this;
			InitializeComponent();
        }

	  

		private void VisualElement_OnFocusChangeRequested(object sender, FocusRequestArgs e)
	    {

			
		    if (e.Focus)
			    MyLabel.TextColor = (Color)Application.Current.Resources["Accent"];
		    else
			   MyLabel.TextColor = TextColor;
	    }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Définit la nouvelle valeur.
        /// </summary>
        /// <typeparam name="T">Le type de la propriété à modifier.</typeparam>
        /// <param name="currentValue">La valeur actuel.</param>
        /// <param name="newValue">La nouvelle valeur.</param>
        /// <param name="propertyName">Le nom de la propriété.</param>
        /// <returns><c>Ture</c>, s'il y a eu un changement.</returns>
        protected bool SetProperty<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            return PropertyChanged.SetProperty(this, ref currentValue, newValue, propertyName);
        }

        protected void SetPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

	    private void VisualElement_OnFocused(object sender, FocusEventArgs e)
	    {

		    var color = MyLabel.TextColor;
		    MyLabel.TextColor = (Color)Application.Current.Resources["Accent"];
		}

	    private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
	    {
			MyLabel.TextColor =Color.Black;

		}
    }
}