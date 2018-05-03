using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace RDS.ViewModels.Common.Behaviors
{
    public class MyBehavior : Behavior<UIElement>
    {
        public DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T)) source = VisualTreeHelper.GetParent(source);
            return source;
        }
    }
}
