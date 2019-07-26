using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTravel
{
    public static class Utils
    {
        public static string AskUserForFile()
        {
            System.Windows.Forms.OpenFileDialog chooseFileDialog = new OpenFileDialog
            {
                Filter = "Json files (.json)|*.json",
                FilterIndex = 1,
                Multiselect = false
            };

            if (chooseFileDialog.ShowDialog() != DialogResult.OK) return null;
            return chooseFileDialog.FileName;
        }
    }
}
