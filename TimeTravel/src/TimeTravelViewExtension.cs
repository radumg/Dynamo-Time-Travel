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
            var inputNodes = dynamoViewModel.CurrentSpaceViewModel.Nodes
                .Where(x => x.IsSetAsInput)
                .Select(x=> new NodePreset(x.NodeModel))
                .ToList();

            var snapshot = new Snapshot(inputNodes);
            var saved = snapshot.ToJsonFile();
            MessageBox.Show($"Snapshot saved to: {snapshot.GenerateSnapshotFilename()}");
        }

        public static void LoadSnapshot()
        {
            var filepath = Utils.AskUserForFile();
            if(String.IsNullOrWhiteSpace(filepath))
            {
                MessageBox.Show("Could not load file.");
                throw new ArgumentNullException(nameof(filepath));
            }

            // attempt to deserialise the snapshot from JSON file
            Snapshot snapshot=null;
            try
            {
                snapshot = Snapshot.FromJsonFile(filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not read the snapshot file, reported error : {ex.Message}");
            }

            foreach (var nodePreset in snapshot.Nodes)
            {
                var matchingNode = dynamoViewModel.CurrentSpaceViewModel.Nodes
                    .Select(x=>x.NodeModel)
                    .Where(x => x.GUID.ToString() == nodePreset.Id)
                    .FirstOrDefault();

                if (matchingNode == null) continue;

                matchingNode.SetSize(500, 500);
            }
        }

    }
}