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

        public static void TakeSnapshot()
        {
            var inputNodes = dynamoViewModel.CurrentSpaceViewModel.Nodes.Where(x => x.IsSetAsInput);

            var snapshot = new Snapshot(inputNodes.ToList());
            var saved = snapshot.ToJsonFile();
            MessageBox.Show(snapshot.GenerateSnapshotFilename());
        }

        public static void LoadSnapshot()
        {
            var filepath = Utils.AskUserForFile();
            if(String.IsNullOrWhiteSpace(filepath))
            {
                MessageBox.Show("Could not load file.");
                throw new ArgumentNullException(nameof(filepath));
            }

            var snapshot = Snapshot.FromJsonFile(filepath);
            foreach (var node in snapshot.Nodes)
            {
                var matchingNode = dynamoViewModel.CurrentSpaceViewModel.Nodes.Where(x => x.Id == node.Id).FirstOrDefault();
                if (matchingNode == null) continue;

                matchingNode.X = node.X;
                matchingNode.Y = node.Y;
            }
        }

    }
}