
using System;
using System.ComponentModel;
using Doods.StdFramework.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DoodsEntry), typeof(Doods.StdFramework.iOS.Renderer.DoodsEntryRenderer))]
namespace Doods.StdFramework.iOS.Renderer
{
    public class DoodsEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var view = (DoodsEntry)Element;

            if (view != null)
            {
                SetBorder(view);
                SetFont(view);
                SetFontFamily(view);
                SetMaxLength(view);
                SetTextAlignment(view);
            }
        }

        void SetFont(DoodsEntry view)
        {
            UIFont uiFont;
            if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
                Control.Font = uiFont;
            else if (view.Font == Font.Default)
                Control.Font = UIFont.SystemFontOfSize(17f);
        }

        void SetFontFamily(DoodsEntry view)
        {
            UIFont uiFont;

            if (!string.IsNullOrWhiteSpace(view.FontFamily) && (uiFont = view.Font.ToUIFont()) != null)
            {
                var ui = UIFont.FromName(view.FontFamily, (nfloat)(view.Font != null ? view.Font.FontSize : 17f));
                Control.Font = uiFont;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (DoodsEntry)Element;

            if (e.PropertyName == DoodsEntry.FontProperty.PropertyName)
                SetFont(view);

            if (e.PropertyName == DoodsEntry.FontFamilyProperty.PropertyName)
                SetFontFamily(view);

            if (e.PropertyName == DoodsEntry.HasBorderProperty.PropertyName)
                SetBorder(view);

            if (e.PropertyName == DoodsEntry.MaxLengthProperty.PropertyName)
                SetMaxLength(view);

            if (e.PropertyName == DoodsEntry.XAlignProperty.PropertyName)
                SetTextAlignment(view);
        }

        void SetMaxLength(DoodsEntry view)
        {
            Control.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                var newLength = textField.Text.Length + replacementString.Length - range.Length;
                return newLength <= view.MaxLength;
            };
        }

        void SetBorder(DoodsEntry view)
        {
            Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        void SetTextAlignment(DoodsEntry view)
        {
            switch (view.XAlign)
            {
                case TextAlignment.Center:
                    Control.TextAlignment = UITextAlignment.Center;
                    break;
                case TextAlignment.End:
                    Control.TextAlignment = UITextAlignment.Right;
                    break;
                case TextAlignment.Start:
                    Control.TextAlignment = UITextAlignment.Left;
                    break;
            }
        }
    }
}