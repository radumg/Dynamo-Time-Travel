using Dynamo.Graph.Nodes;
using System;
using System.Windows;

namespace TimeTravel
{
    public static class Events
    {
        public static void RegisterEventHandlers()
        {
            TimeTravelViewExtension.viewLoadedParams.CurrentWorkspaceChanged += OnCurrentWorkspaceChanged;
        }

        public static void UnregisterEventHandlers()
        {
            TimeTravelViewExtension.viewLoadedParams.CurrentWorkspaceChanged -= OnCurrentWorkspaceChanged;
        }

        private static void OnCurrentWorkspaceChanged(Dynamo.Graph.Workspaces.IWorkspaceModel obj)
        {
            // do something here
        }

        public static async void OnTakeSnapshot(object sender, RoutedEventArgs e)
        {
            TimeTravelViewExtension.TakeSnapshot();
        }

        public static async void OnLoadSnapshot(object sender, RoutedEventArgs e)
        {
            TimeTravelViewExtension.LoadSnapshot();
        }
    }
}
