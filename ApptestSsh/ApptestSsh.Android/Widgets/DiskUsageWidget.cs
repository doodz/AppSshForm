using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace ApptestSsh.Droid.Widgets
{
    [BroadcastReceiver(Label = "@string/widget_disk_usage")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_disk_usage")]
    public class DiskUsageWidget : AppWidgetProvider
    {

        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            // To prevent any ANR timeouts, we perform the update in a service
            //context.StartService(new Intent(context, typeof(UpdateService)));


            ComponentName me = new ComponentName(context, Java.Lang.Class.FromType(typeof(DiskUsageWidget)).Name);
            appWidgetManager.UpdateAppWidget(me, BuildUpdate(context, appWidgetIds));
        }


        private RemoteViews BuildUpdate(Context context, int[] appWidgetIds)
        {
            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_disk_usage);

            updateViews.SetTextViewText(Resource.Id.message, "My message : " + DateTime.Now);



            Intent i = new Intent(context, typeof(DiskUsageWidget));
            i.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
            i.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

            PendingIntent pi = PendingIntent.GetBroadcast(context, 0, i, PendingIntentFlags.UpdateCurrent);


            updateViews.SetOnClickPendingIntent(
                Resource.Id.widgetBackground,
                pi
            );

            //updateViews.SetOnClickPendingIntent(
            //    Resource.Id.message,
            //    pi
            //);

            return updateViews;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);


            if (intent.Action == UpdateService.GREEN_CLICKED)
            {
                
            }
        }
    }

    [Service]
    public class UpdateService : Service 
    {
        public static String GREEN_CLICKED = "GREEN CLICKED";
        public override void OnStart(Intent intent, int startId)
        {
            // Build the widget update for today
            RemoteViews updateViews = BuildUpdate(this);

            // Push update for this widget to the home screen
            var thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(DiskUsageWidget)).Name);
            var manager = AppWidgetManager.GetInstance(this);
            manager.UpdateAppWidget(thisWidget, updateViews);
        }

        public override IBinder OnBind(Intent intent)
        {
            // We don't need to bind to this service
            return null;
        }


       

        // Build a widget update to show the current Wiktionary
        // "Word of the day." Will block until the online API returns.
        public RemoteViews BuildUpdate(Context context)
        {
            //var entry = BlogPost.GetBlogPost();

            // Build an update that holds the updated widget contents
            var updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget_disk_usage);

            updateViews.SetTextViewText(Resource.Id.message, "My message : " + DateTime.Now);

            updateViews.SetOnClickPendingIntent(
                Resource.Id.widgetBackground,
                GetPendingSelfIntent(context, AppWidgetManager.ActionAppwidgetUpdate)
            );

            updateViews.SetOnClickPendingIntent(
                Resource.Id.message,
                GetPendingSelfIntent(context, GREEN_CLICKED)
            );
            //RegisterClicks(context, null, updateViews);

            //// When user clicks on widget, launch update
            //if (!string.IsNullOrEmpty(entry.Link))
            //{
            //    Intent defineIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(entry.Link));

            //    PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, defineIntent, 0);
            //    updateViews.SetOnClickPendingIntent(Resource.Id.widget, pendingIntent);
            //}

            return updateViews;
        }

        protected PendingIntent GetPendingSelfIntent(Context context, String action)
        {
            Intent intent = new Intent(context, typeof(DiskUsageWidget));
            intent.SetAction(action);

            Toast.MakeText(context, "Clicked: " + action, ToastLength.Long).Show();
            return PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        //private void RegisterClicks(Context context, int[] appWidgetIds, RemoteViews widgetView)
        //{
        //    var intent = new Intent(context, typeof(DiskUsageWidget));
        //    intent.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
        //    //intent.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

        //    // Register click event for the Background
        //    var piBackground = PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        //   //PendingIntent configPendingIntent = PendingIntent.GetActivity(context, 0, configIntent, 0);
        //    widgetView.SetOnClickPendingIntent(Resource.Id.widgetBackground, piBackground);
        //}

        //public bool OnTouch(View v, MotionEvent motion)
        //{
        //    switch (motion.Action)
        //    {
        //        case MotionEventActions.Down:
        //            //initial position
        //            _initialX = _layoutParams.X;
        //            _initialY = _layoutParams.Y;

        //            //touch point
        //            _initialTouchX = motion.RawX;
        //            _initialTouchY = motion.RawY;
        //            return true;

        //        case MotionEventActions.Move:
        //            //Calculate the X and Y coordinates of the view.
        //            _layoutParams.X = _initialX + (int)(motion.RawX - _initialTouchX);
        //            _layoutParams.Y = _initialY + (int)(motion.RawY - _initialTouchY);

        //            //Update the layout with new X & Y coordinate
        //            _windowManager.UpdateViewLayout(_floatingView, _layoutParams);
        //            return true;
        //    }
        //}


        //private void toto(Context context, int[] appWidgetIds)
        //{
        //    RemoteViews updateViews = new RemoteViews(context.PackageName, Resource.Layout.widget);

        //    Intent i = new Intent(context, typeof(AppWidget));
        //    i.SetAction(AppWidgetManager.ActionAppwidgetUpdate);
        //    i.PutExtra(AppWidgetManager.ExtraAppwidgetIds, appWidgetIds);

        //    PendingIntent pi = PendingIntent.GetBroadcast(context, 0, i, PendingIntentFlags.UpdateCurrent);

        //    updateViews.SetImageViewResource(Resource.Id.left_die, IMAGES[(int)(Math.Random() * 6)]);
        //    updateViews.SetOnClickPendingIntent(Resource.Id.left_die, pi);

        //    updateViews.SetImageViewResource(Resource.Id.right_die, IMAGES[(int)(Math.Random() * 6)]);
        //    updateViews.SetOnClickPendingIntent(Resource.Id.right_die, pi);

        //    updateViews.SetOnClickPendingIntent(Resource.Id.background, pi);

        //    return (updateViews);
        //}
    }
}