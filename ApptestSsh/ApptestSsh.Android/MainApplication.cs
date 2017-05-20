using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ApptestSsh.Core;

using Android.Util;
using Autofac;
using Doods.StdRepository.Interfaces;

[assembly: UsesPermission(Manifest.Permission.Internet)]
[assembly: UsesPermission(Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]
namespace ApptestSsh.Droid
{
    [Application(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/MainTheme")]
    public class MainApplication : Application
    {
        public override void OnCreate()
        {
            base.OnCreate();
            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;
            App.SetupContainer(Bootstrapper.CreateContainer());
            App.Container.Resolve<IDatabase>().Initialize();

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

    }
}