using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace ApptestSsh.Droid.Widgets
{
    [BroadcastReceiver(Label = "@string/widget_disk_usage")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_DISK_USE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/widget_disk_usage")]
    public class DiskUsageWidget : AppWidgetProvider
    {

        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            // To prevent any ANR timeouts, we perform the update in a service
            context.StartService(new Intent(context, typeof(UpdateService)));
        }

    }

    [Service]
    public class UpdateService : Service
    {
        public override void OnStart(Intent intent, int startId)
        {
            // Build the widget update for today
            RemoteViews updateViews = BuildUpdate(this);

            // Push update for this widget to the home screen
            ComponentName thisWidget = new ComponentName(this, Java.Lang.Class.FromType(typeof(DiskUsageWidget)).Name);
            AppWidgetManager manager = AppWidgetManager.GetInstance(this);
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

            updateViews.SetTextViewText(Resource.Id.message, "My message");
            

            //// When user clicks on widget, launch to Wiktionary definition page
            //if (!string.IsNullOrEmpty(entry.Link))
            //{
            //    Intent defineIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(entry.Link));

            //    PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, defineIntent, 0);
            //    updateViews.SetOnClickPendingIntent(Resource.Id.widget, pendingIntent);
            //}

            return updateViews;
        }
    }
}