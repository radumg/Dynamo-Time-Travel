using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Dynamo.ViewModels;
using Dynamo.Wpf.Extensions;

namespace TimeTravel
{
    public class TimeTravelViewExtension : IViewExtension
    {
        public string UniqueId => "EB025BAD-47AD-47A8-AC74-9F6DE2FA7190";
        public string Name => "TimeTravel View Extension";

        private MenuItem sampleMenuItem;
        internal static ViewLoadedParams viewLoadedParams = null;
        internal static DynamoViewModel dynamoViewModel = null;

        public void Dispose()
        {
        }

        public void Startup(ViewStartupParams vsp)
        {
        }

        public void Loaded(ViewLoadedParams vlp)
        {
            // hold a reference to the Dynamo params to be used later
            if (viewLoadedParams == null) viewLoadedParams = vlp;
            if (dynamoViewModel == null) dynamoViewModel = vlp.DynamoWindow.DataContext as DynamoViewModel;

            // add Dynamo Server menu to Dynamo UI
            viewLoadedParams.dynamoMenu.Items.Add(UI.TimeTravelMenu);
        }

        public void Shutdown()
        {
        }

    }
}