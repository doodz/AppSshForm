using System;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using ApptestSsh.Core;
using Android.Util;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdRepository.Interfaces;

[assembly: UsesPermission(Manifest.Permission.Internet)]
[assembly: UsesPermission(Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]

namespace ApptestSsh.Droid
{
    [Application(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/MainTheme")]
    public class MainApplication : Application , Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            RegisterActivityLifecycleCallbacks(this);

            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;
            //AppDomain.CurrentDomain.UnhandledException += (sender, args) => { };
            CoreSetup.SetupContainer(new Setup());
            AppContainer.Container.Resolve<IDatabase>().Initialize();

#if !DEBUG
            MobileCenter.Start(config.MobileCenterKey, typeof(Analytics), typeof(Crashes));
#endif
        }

        private void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
            Log.Debug("MainApplication", "exception-message:" + e.Exception.Message);
            Log.Debug("MainApplication", "exception-stack:" + e.Exception.StackTrace);
            Log.Debug("MainApplication", "exception-source:" + e.Exception.Source);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {

           
        }

        public void OnActivityDestroyed(Activity activity)
        {
            
        }

        public void OnActivityPaused(Activity activity)
        {
           
        }

        public void OnActivityResumed(Activity activity)
        {
            
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            
        }

        public void OnActivityStarted(Activity activity)
        {
            //HockeyApp.Android.Tracking.StartUsage(activity);
        }

        public void OnActivityStopped(Activity activity)
        {
            //HockeyApp.Android.Tracking.StopUsage(activity);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }
    }
}