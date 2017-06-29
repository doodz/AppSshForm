using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Doods.StdFramework.System.ComponentModel;
using Xamarin.Forms;

namespace Doods.StdFramework.Views
{
    public class DoodsRoundedRectangleView : ContentView, INotifyPropertyChanged
    {
        public event EventHandler Clicked;

        #region Properties

        public ContentView View { get; set; }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(DoodsRoundedRectangleView), 16.0F, propertyChanged: OnCornerRadiusChanged);

        private static void OnCornerRadiusChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((DoodsRoundedRectangleView)bindable).SetCornerRadius();
            ((DoodsRoundedRectangleView)bindable).SetPropertyChanged(nameof(CornerRadius));
        }

        private void SetCornerRadius()
        {
            _frame.CornerRadius = CornerRadius;
        }
        public float CornerRadius
        {
            get { return (float) GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(DoodsRoundedRectangleView),
                Color.Transparent, propertyChanged: OnFillColorChanged);

        private static void OnFillColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((DoodsRoundedRectangleView)bindable).SetColor();
            ((DoodsRoundedRectangleView) bindable).SetPropertyChanged(nameof(FillColor));
           
        }

        private void SetColor()
        {
            _root.BackgroundColor = FillColor;
        }
        public Color FillColor
        {
            get { return (Color) GetValue(FillColorProperty); }
            set
            {
                //_root.BackgroundColor = value;
                SetValue(FillColorProperty, value);
            }
        }

        View _innerContent;

        public View InnerContent
        {
            get { return _innerContent; }
            set
            {
                if (value == null && _innerContent != null && _root.Children.Contains(_innerContent))
                    _root.Children.Remove(_innerContent);

                if (_innerContent != value)
                {
                    _innerContent = value;

                    if (_innerContent != null)
                    {
                        _innerContent.HorizontalOptions = LayoutOptions.Center;
                        _innerContent.VerticalOptions = LayoutOptions.Center;
                        _root.Children.Add(_innerContent);
                    }
                }
            }
        }

        #endregion


        private readonly Grid _root = new Grid() {BackgroundColor = Color.Transparent};
        private readonly Frame _frame = new Frame();
        public DoodsRoundedRectangleView()
        {


            _frame.Margin = _frame.Padding = 0;
            _frame.CornerRadius = (float) CornerRadius;
            _frame.Content = _root;


            //_root.SetBinding(Grid.BackgroundColorProperty, new Binding( nameof(FillColor)));
            ////_root.BackgroundColor = FillColor;
            //f.SetBinding(Frame.BackgroundColorProperty, new Binding(nameof(FillColor)));
            //f.SetBinding(Frame.CornerRadiusProperty, new Binding(nameof(CornerRadius)));
            ////f.BackgroundColor = FillColor;
            Content = _frame;

           

            GestureRecognizers.Add(new TapGestureRecognizer((obj) => Clicked?.Invoke(this, new EventArgs())));
        }


        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            _root.HeightRequest = height;
            _root.WidthRequest = width;
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
    }
}