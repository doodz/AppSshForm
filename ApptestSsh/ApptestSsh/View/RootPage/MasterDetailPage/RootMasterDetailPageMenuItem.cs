﻿using ApptestSsh.Core.Services;
using Doods.StdFramework.Mvvm;
using System;

namespace ApptestSsh.Core.View.RootPage.MasterDetailPage
{
    public class RootMasterDetailPageMenuItem : IPageMenuItem
    {
        public RootMasterDetailPageMenuItem()
        {
            TargetType = typeof(RootMasterDetailPageDetail);
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string Icon { get; set; }
        public Action<INavigationService> Navigitation { get; set; }
    }
}