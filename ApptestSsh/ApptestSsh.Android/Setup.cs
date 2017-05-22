using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Doods.StdFramework.ApplicationObjects;

namespace ApptestSsh.Droid
{
    public class Setup : AppSetup
    {

        public Setup()
        {
            ContainerBuilder = Bootstrapper.CreateContainer();
        }
    }
}