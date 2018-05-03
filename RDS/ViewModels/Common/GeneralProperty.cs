using RDS.Models.RuntimeData.Config;
using RDS.Models.RuntimeData.WorkPanel;
using RDS.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RDS.ViewModels.Common
{
    public static partial class General
    {
        private static MainWindow mainWindow;

        private static SetupView setupView;

        private static ResourceDictionary CurrentLanguageResource { get; set; }

        public static readonly string JsonFilePath = string.Format(Properties.Resources.StringFormat2, Environment.CurrentDirectory, Properties.Resources.JsonConfigFilePath);

        public static AutoSAT RuntimeData { get; set; }

        public static Configuration Configuration { get; set; } = new Configuration();

        public static List<string> SelectedItemReagentNames { get; set; }

        public static bool IsUnLock { get; set; } = true;

        public static SolidColorBrush GrayColor { get { return (General.FindResource(Properties.Resources.GrayColor) as SolidColorBrush); } }

        public static SolidColorBrush GreenColor { get { return (General.FindResource(Properties.Resources.GreenColor) as SolidColorBrush); } }

        public static SolidColorBrush BlueColor { get { return (General.FindResource(Properties.Resources.BlueColor) as SolidColorBrush); } }

        public static SolidColorBrush BlueColor2 { get { return (General.FindResource(Properties.Resources.BlueColor2) as SolidColorBrush); } }

        public static SolidColorBrush WathetColor { get { return (General.FindResource(Properties.Resources.WathetColor) as SolidColorBrush); } }

        public static SolidColorBrush WathetColor2 { get { return (General.FindResource(Properties.Resources.WathetColor2) as SolidColorBrush); } }

        public static SolidColorBrush WathetColor3 { get { return (General.FindResource(Properties.Resources.WathetColor3) as SolidColorBrush); } }

        public static SolidColorBrush ChartColor1 { get { return (General.FindResource(Properties.Resources.ChartColor1) as SolidColorBrush); } }

        public static SolidColorBrush ChartColor2 { get { return (General.FindResource(Properties.Resources.ChartColor2) as SolidColorBrush); } }

        public static SolidColorBrush ChartColor3 { get { return (General.FindResource(Properties.Resources.ChartColor3) as SolidColorBrush); } }

        public static SolidColorBrush Transparent => new SolidColorBrush(Colors.Transparent);

        public static PathGeometry Out => General.FindResource("Out") as PathGeometry;

        public static PathGeometry In => General.FindResource("In") as PathGeometry;

        public static PathGeometry Play => General.FindResource(Properties.Resources.Play) as PathGeometry;

        public static PathGeometry Pause { get { return (General.FindResource(Properties.Resources.Pause) as PathGeometry); } }

        public static PathGeometry Alert { get { return (General.FindResource(Properties.Resources.Alert) as PathGeometry); } }

        public static PathGeometry Wait { get { return (General.FindResource(Properties.Resources.Wait) as PathGeometry); } }

        public static PathGeometry Message { get { return (General.FindResource(Properties.Resources.Message) as PathGeometry); } }

        public static PathGeometry Lock { get { return (General.FindResource(Properties.Resources.Lock) as PathGeometry); } }

        public static PathGeometry UnLock { get { return (General.FindResource(Properties.Resources.UnLock) as PathGeometry); } }


    }
}