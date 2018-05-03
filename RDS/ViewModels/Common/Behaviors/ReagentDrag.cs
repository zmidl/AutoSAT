using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RDS.ViewModels.Common.Behaviors
{
    public class ReagentDrag : MyBehavior
    {
        public object DraggedData
        {
            get { return (object)GetValue(DragedDataProperty); }
            set { SetValue(DragedDataProperty, value); }
        }
        public static readonly DependencyProperty DragedDataProperty = DependencyProperty.Register(nameof(DraggedData), typeof(object), typeof(ReagentDrag), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (this.VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) is TreeViewItem treeViewItem)
                {
                    DragDrop.DoDragDrop(treeViewItem, treeViewItem.DataContext, DragDropEffects.Copy);
                }
            }
        }
    }
}
