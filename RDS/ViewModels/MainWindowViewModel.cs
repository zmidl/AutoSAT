using RDS.Apps;
using RDS.Models;
using RDS.ViewModels.Common;
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RDS.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public RelayCommand Closed { get; private set; }

        public MainWindowViewModel()
        {
            this.Closed = new RelayCommand(this.ExecuteClosed);

            General.ReadConfiguration();
        }

        private void ExecuteClosed()
        {
            //var processName = Properties.Resources.RdsReaderFileName.Split(Properties.Resources.Separator6.ToCharArray()[0])[0];
            //try
            //{
            //    if (General.IsFluorReaderWorking) General.StopProcess(processName);
            //}
            //catch
            //{
            //    System.Windows.MessageBox.Show(string.Format
            //    (
            //        Properties.Resources.StringFormat2,
            //        General.FindStringResource(Properties.Resources.Message6),
            //        processName)
            //    );
            //}

          
        }
    }
}
