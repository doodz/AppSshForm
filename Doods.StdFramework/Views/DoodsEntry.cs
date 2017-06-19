using Xamarin.Forms;

namespace Doods.StdFramework.Views
{
    public class DoodsEntry : Entry
    {
		
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(DoodsEntry), true);

        public bool HasBorder
        {
            get { return (bool) GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public static readonly BindableProperty FontProperty =
            BindableProperty.Create(nameof(Font), typeof(Font), typeof(DoodsEntry), new Font());

        public Font Font
        {
            get { return (Font) GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(DoodsEntry), null);

        public string FontFamily
        {
            get { return (string) GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(DoodsEntry), int.MaxValue);

        public int MaxLength
        {
            get { return (int) GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create(nameof(XAlign), typeof(TextAlignment), typeof(DoodsEntry), TextAlignment.Start);

        public TextAlignment XAlign
        {
            get { return (TextAlignment) GetValue(XAlignProperty); }
            set { SetValue(XAlignProperty, value); }
        }

		//public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(DoodsEntry),
		//	Color.Blue);

	 //   public Color BackgroundColor
		//{
		//    get => (Color)GetValue(BackgroundColorProperty);
		//    set => SetValue(BackgroundColorProperty, value);
	 //   }

		

		//public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(DoodsEntry),
		//    Color.Blue);

	    
	 //   public Color PlaceholderColor
		//{
		//    get => (Color)GetValue(PlaceholderColorProperty);
		//    set => SetValue(PlaceholderColorProperty, value);
	 //   }

	 //   public static readonly BindableProperty StyleProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Style), typeof(DoodsEntry),
		//    null);


	 //   public Style Style
		//{
		//    get => (Style)GetValue(StyleProperty);
		//    set => SetValue(TextColorProperty, value);
	 //   }
		
	}
}