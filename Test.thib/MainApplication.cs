using System;
using Android;
using Android.App;
using Android.Runtime;
using Android.Util;
using ApptestSsh.Core;
using Autofac;
using Doods.StdFramework.ApplicationObjects;
using Doods.StdRepository.Interfaces;

[assembly: UsesPermission(Manifest.Permission.Internet)]
[assembly: UsesPermission(Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]
namespace Test.thib
{
    [Application(Label = "toto", Icon = "@drawable/icon", Theme = "@style/MainTheme")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
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

    }
}