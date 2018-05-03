using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace RDS.ViewModels.Common.Behaviors
{
    public class ReagentDrop : Behavior<UIElement>, ICommandSource
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ReagentDrop), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(ReagentDrop), new PropertyMetadata(null));

        public IInputElement CommandTarget => this.AssociatedObject;

        public object DroppedData
        {
            get { return (object)GetValue(DroppedDataProperty); }
            set { SetValue(DroppedDataProperty, value); }
        }
        public static readonly DependencyProperty DroppedDataProperty =
            DependencyProperty.Register("DroppedData", typeof(object), typeof(ReagentDrop), new PropertyMetadata(null));



        public ReagentDrop() { }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Drop += AssociatedObject_Drop;
            this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            //this.Command?.Execute(false);
        }

        //private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        //{
        //   
        //}

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Drop -= AssociatedObject_Drop;
            this.AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
            this.AssociatedObject.DragLeave -= AssociatedObject_DragLeave;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            //this.Command?.Execute(true);
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            this.DroppedData = e.Data.GetData(e.Data.GetFormats()[0]);
            this.Command?.Execute(this.CommandParameter);

        }
    }
}
