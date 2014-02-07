using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.JoshSmith.Controls;
using System.Windows.Markup;
using System.Xml;
using WPF.JoshSmith.Panels;

namespace Test.DragCanvas
{
	/// <summary>
	/// Interaction logic for DragCanvasWindow.xaml
	/// </summary>
	public partial class DragCanvasWindow : Window
	{
        #region Data

        /// <summary>
        /// Stores a reference to the UIElement which the Canvas's context menu currently targets.
        /// </summary>
        private UIElement elementForContextMenu;

        #endregion // Data

        #region Constructor

        public DragCanvasWindow()
        {
            InitializeComponent();

            this.PreviewMouseRightButtonDown += Window1_PreviewMouseRightButtonDown;
            this.btnOnlyShowOffsetIndicators.Checked += btnOnlyShowOffsetIndicators_Checked;
            this.btnOnlyShowOffsetIndicators.Unchecked += btnOnlyShowOffsetIndicators_Unchecked;
            
            // Add the blocks which display their positions within the Canvas.
            foreach( string key in new string[] { 
													"buttonTopLeft", 
													"buttonTopRight", 
													"buttonBottomRight", 
													"buttonBottomLeft", 
													"buttonAll", 
													"buttonNone" 
												} )
            {
                Button button = this.FindResource( key ) as Button;
				this.dragCanvas.Children.Add( button );
            }

			this.ResetZOrder();			
        }

        #endregion // Constructor

        #region btnOnlyShowOffsetIndicators_Checked

        void btnOnlyShowOffsetIndicators_Checked( object sender, RoutedEventArgs e )
        {
			foreach( UIElement child in this.dragCanvas.Children )
            {
                child.Visibility =
                    child is Button && (child as Button).Content == null ?
                    Visibility.Visible :
                    Visibility.Collapsed;
            }

			this.ResetZOrder();
        }

        #endregion // btnOnlyShowOffsetIndicators_Checked

        #region btnOnlyShowOffsetIndicators_Unchecked

        void btnOnlyShowOffsetIndicators_Unchecked( object sender, RoutedEventArgs e )
        {
			foreach( UIElement child in this.dragCanvas.Children )
                child.Visibility = Visibility.Visible;

			this.ResetZOrder();
        }

        #endregion // btnOnlyShowOffsetIndicators_Unchecked        

        #region OnButtonClick

        private void OnButtonClick( object sender, RoutedEventArgs e )
        {
            MessageBox.Show( "Thank you for clicking today, your clicks are important to us." );
        }

        #endregion // OnButtonClick

		#region OnContextMenuOpened

		void OnContextMenuOpened( object sender, RoutedEventArgs e )
		{
			if( this.elementForContextMenu != null )
				this.menuItemCanBeDragged.IsChecked = WPF.JoshSmith.Panels.DragCanvas.GetCanBeDragged( this.elementForContextMenu );
		}

		#endregion // OnContextMenuOpened

		#region OnMenuItemClick

		/// <summary>
        /// Handles the Click event of both menu items in the context menu.
        /// </summary>
        void OnMenuItemClick( object sender, RoutedEventArgs e )
        {
            if( this.elementForContextMenu == null )
                return;

			if( e.Source == this.menuItemBringToFront ||
				e.Source == this.menuItemSendToBack )
			{
				bool bringToFront = e.Source == this.menuItemBringToFront;

				if( bringToFront )
					this.dragCanvas.BringToFront( this.elementForContextMenu );
				else
					this.dragCanvas.SendToBack( this.elementForContextMenu );
			}
			else
			{				
				bool canBeDragged = WPF.JoshSmith.Panels.DragCanvas.GetCanBeDragged( this.elementForContextMenu );
				WPF.JoshSmith.Panels.DragCanvas.SetCanBeDragged( this.elementForContextMenu, !canBeDragged );
				(e.Source as MenuItem).IsChecked = !canBeDragged;
			}
        }

        #endregion // OnMenuItemClick

		#region ResetZOrder

		private void ResetZOrder()
		{
			// Set the z-index of every visible child in the Canvas.
			int index = 0;
			for( int i = 0; i < this.dragCanvas.Children.Count; ++i )
				if( this.dragCanvas.Children[i].Visibility == Visibility.Visible )
					Canvas.SetZIndex( this.dragCanvas.Children[i], index++ );
		}

		#endregion // ResetZOrder

		#region Window1_PreviewMouseRightButtonDown

		void Window1_PreviewMouseRightButtonDown( object sender, MouseButtonEventArgs e )
        {
            // If the user right-clicks while dragging an element, assume that they want 
            // to manipulate the z-index of the element being dragged (even if it is  
            // behind another element at the time).
			if( this.dragCanvas.ElementBeingDragged != null )
				this.elementForContextMenu = this.dragCanvas.ElementBeingDragged;
            else
                this.elementForContextMenu =
					this.dragCanvas.FindCanvasChild( e.Source as DependencyObject );
        }

        #endregion // Window1_PreviewMouseRightButtonDown
	}
}