using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RDSCL
{
    /// <summary>
    /// RD_TipRackLight.xaml 的交互逻辑
    /// </summary>
    public partial class RD_TipRackLight : UserControl
    {
        private readonly SolidColorBrush RackLoadedColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF75C8FF"));
        private readonly SolidColorBrush RackUnloadColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEAF5FF"));

        private RD_TipHost host;

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register
        (
             nameof(Value),
             typeof(object),
             typeof(RD_TipRackLight),
             new PropertyMetadata(null, new PropertyChangedCallback(ValueChangedCallback))
        );

        private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tipRack = (RD_TipRackLight)d;

            var newValue = (Tuple<bool?, bool[]>)e.NewValue;

            if(newValue!=null)
            {
                if (newValue.Item1 == true)
                {
                    tipRack.Rectangle_Frame.Fill = tipRack.RackLoadedColor;
                    tipRack.host.Visibility = Visibility.Visible;
                }
                else if (newValue.Item1 == false)
                {
                    tipRack.Rectangle_Frame.Fill = tipRack.RackUnloadColor;
                    tipRack.host.Visibility = Visibility.Hidden;
                }
                tipRack.host.UpdateDrawing(newValue.Item2);
            }
        }

        public RD_TipRackLight()
        {
            InitializeComponent();
            this.host = new RD_TipHost();
            this.Canvas.Children.Add(this.host);
        }
    }

    public class RD_TipHost : FrameworkElement
    {
        private VisualCollection children;
        private DrawingVisual[] drawingVisuals = new DrawingVisual[96];
        public RD_TipHost()
        {
            this.Width = 290;
            this.Height = 200;
            children = new VisualCollection(this);
            for (int i = 0; i < this.drawingVisuals.Length; i++)
            {
                this.drawingVisuals[i] = new DrawingVisual();
                this.children.Add(this.drawingVisuals[i]);
            }
        }

        private void DrawVisualEllipse(int index, Brush brush, Point center, Size size, Pen pen = null)
        {
            using (DrawingContext drawingContext = drawingVisuals[index].RenderOpen())
            {
                drawingContext.DrawEllipse(brush, pen, center, size.Width, size.Height);
            }
        }

        private void DrawVisualEllipse(int index, bool isLoaded)
        {
            var loadedBrush = (Brush)new BrushConverter().ConvertFromString("#FF35AAF7");//#FF97C300
            var unLoadBrush = Brushes.White;
            var brush = isLoaded ? loadedBrush : unLoadBrush;
            var size = isLoaded ? new Size(9.5, 9.5) : new Size(7.5,7.5);
            this.DrawVisualEllipse(index, brush, new Point(((index / 8) * 22) + 23, ((index % 8) * 22) + 23), size);
        }

        public void UpdateDrawing(int index, bool isLoaded)
        {
            this.DrawVisualEllipse(index, isLoaded);
        }

        public void UpdateDrawing(bool[] allStates)
        {
            for (int i = 0; i < allStates.Length; i++) this.DrawVisualEllipse(i, allStates[i]);
        }

        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return children[index];
        }
    }
}
