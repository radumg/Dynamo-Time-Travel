using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TimeTravel
{
    public static class UI
    {
        public static MenuItem TimeTravelMenu;
        private static MenuItem TakeSnapshotMenuItem;
        private static MenuItem LoadSnapshotMenuItem;

        static UI()
        {
            TimeTravelMenu = new MenuItem { Header = "Time Travel" };
            TakeSnapshotMenuItem = new MenuItem { Header = "Take snapshot" };
            LoadSnapshotMenuItem = new MenuItem { Header = "Travel to snapshot" };

            // register event handlers
            TakeSnapshotMenuItem.Click += Events.OnTakeSnapshot;
            LoadSnapshotMenuItem.Click += Events.OnLoadSnapshot;

            TimeTravelMenu.Items.Add(TakeSnapshotMenuItem);
            TimeTravelMenu.Items.Add(LoadSnapshotMenuItem);
        }
    }
}
