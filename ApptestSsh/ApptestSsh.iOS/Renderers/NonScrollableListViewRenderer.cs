﻿using System;
using ApptestSsh.iOS.Renderers;
using Doods.StdFramework.Views;
using Xamarin.Forms;

using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NonScrollableListView), typeof(NonScrollableListViewRenderer))]
[assembly: ExportRenderer(typeof(AlwaysScrollView), typeof(AlwaysScrollViewRenderer))]
namespace ApptestSsh.iOS.Renderers
{
    public class NonScrollableListViewRenderer : ListViewRenderer
    {
        public static void Initialize()
        {
            var test = DateTime.UtcNow;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.ScrollEnabled = false;

        }
    }

    public class AlwaysScrollViewRenderer : ScrollViewRenderer
    {
        public static void Initialize()
        {
            var test = DateTime.UtcNow;
        }
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            this.AlwaysBounceVertical = true;
        }
    }
}

