using System.ComponentModel;
using Android.Text;
using Android.Views;
using Doods.StdFramework.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DoodsEntry), typeof(Doods.StdFramework.Android.Renderer.DoodsEntryRenderer))]
namespace Doods.StdFramework.Android.Renderer
{
public class DoodsEntryRenderer : EntryRenderer
{

protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
{
base.OnElementChanged(e);

var view = (DoodsEntry)Element;

SetFont(view);
SetTextAlignment(view);
SetMaxLength(view);
}

protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
{
base.OnElementPropertyChanged(sender, e);

var view = (DoodsEntry)Element;

if (e.PropertyName == DoodsEntry.FontProperty.PropertyName)
SetFont(view);

if (e.PropertyName == DoodsEntry.XAlignProperty.PropertyName)
SetTextAlignment(view);
}

void SetTextAlignment(DoodsEntry view)
{
switch (view.XAlign)
{
case Xamarin.Forms.TextAlignment.Center:
Control.Gravity = GravityFlags.CenterHorizontal;
break;
case Xamarin.Forms.TextAlignment.End:
Control.Gravity = GravityFlags.End;
break;
case Xamarin.Forms.TextAlignment.Start:
Control.Gravity = GravityFlags.Start;
break;
}
}

void SetFont(DoodsEntry view)
{
if (view.Font != Font.Default)
{
Control.TextSize = view.Font.ToScaledPixel();
}
}

void SetMaxLength(DoodsEntry view)
{
Control.SetFilters(new IInputFilter[] {
new InputFilterLengthFilter(view.MaxLength)
});
}
}
}