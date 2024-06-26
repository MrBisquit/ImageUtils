﻿using ImageUtils.Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageUtils.Windows
{
    /// <summary>
    /// Interaction logic for Layers.xaml
    /// </summary>
    public partial class Layers : Window
    {
        public Layers()
        {
            InitializeComponent();
        }

        bool UnCoupled = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Utils.DisableWindowButtons.Disable(this);

            Width = 200;

            Left = Globals.mainWindow.Left + Globals.mainWindow.Width + 5;
            Top = Globals.mainWindow.Top + 15;
            Height = Globals.mainWindow.Height - 30;

            ResizeMode = ResizeMode.NoResize;
            ShowInTaskbar = false;

            Globals.mainWindow.LocationChanged += (object? sender, EventArgs e) =>
            {
                if (UnCoupled) return;

                Left = Globals.mainWindow.Left + Globals.mainWindow.Width + 5;
                Top = Globals.mainWindow.Top + 15;
                Height = Globals.mainWindow.Height - 30;
                Width = 200;
            };

            Globals.mainWindow.SizeChanged += (object? sender, SizeChangedEventArgs e) =>
            {
                if (UnCoupled) return;

                Left = Globals.mainWindow.Left + Globals.mainWindow.Width + 5;
                Top = Globals.mainWindow.Top + 15;
                Height = Globals.mainWindow.Height - 30;
                Width = 200;
            };

            Globals.mainWindow.Closing += (object? sender, CancelEventArgs e) =>
            {
                Close();
            };

            Globals.mainWindow.GotFocus += (object sender, RoutedEventArgs e) =>
            {
                //Focus();
            };

            Globals.mainWindow.StateChanged += (object? sender, EventArgs e) =>
            {
                if(Globals.mainWindow.WindowState == WindowState.Minimized)
                {
                    //Topmost = false;
                    //WindowState = WindowState.Minimized;
                    Hide();
                } else
                {
                    //WindowState = WindowState.Normal;
                    Show();

                    if (Globals.mainWindow.WindowState == WindowState.Maximized)
                    {
                        UnCoupled = true;
                        ResizeMode = ResizeMode.CanResize;
                        Topmost = true;
                    }
                    else
                    {
                        UnCoupled = false;
                        ResizeMode = ResizeMode.NoResize;
                        Topmost = false;
                    }
                }
            };

            //WindowStyle = WindowStyle.None;

            UpdateLayers();

            Events.LayerUpdate += (object? sender, EventArgs args) =>
            {
                UpdateLayers();
            };

            LayersOC.CollectionChanged += LayersOC_CollectionChanged;
        }

        bool MidChange = false;
        private void LayersOC_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (MidChange) return;

            LayerManager.Layers.Clear();

            foreach (var item in LayersOC)
            {
                LayerManager.Layers.Add(item);
            }

            //UpdateLayers();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (UnCoupled) return;

            Left = Globals.mainWindow.Left + Globals.mainWindow.Width + 5;
            Top = Globals.mainWindow.Top + 15;
            Height = Globals.mainWindow.Height - 30;
        }

        ObservableCollection<Layer> LayersOC = new ObservableCollection<Layer>();

        private void UpdateLayers()
        {
            MidChange = true;

            Project.LayerManager.Layers.ForEach(layer =>
            {
                //layer.Name = $"Layer {layer.LayerLevel}";
                layer.Preview = Globals.mainWindow.ToBitmapImage(layer.Content);
            });

            LayersOC.Clear();

            Project.LayerManager.Layers.ForEach(layer =>
            {
                LayersOC.Add(layer);
            });

            LayerListBox.ItemsSource = LayersOC;
            LayerListBox.Items.Refresh();

            MidChange = false;
        }

        // Bing AI

        private void LayerListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null)
                return;

            // Check if the mouse is over a CheckBox or its child elements
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null && dep != listBox)
            {
                if (dep is CheckBox || dep is ContentPresenter)
                {
                    // If the mouse is over a CheckBox or its child elements, prevent the drag operation
                    return;
                }
                dep = VisualTreeHelper.GetParent(dep);
            }

            // If the mouse is not over a CheckBox, initiate the drag operation
            object data = GetDataFromListBox(listBox, e.GetPosition(listBox));
            if (data != null)
            {
                DragDrop.DoDragDrop(listBox, data, DragDropEffects.Move);
            }
        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            System.Windows.UIElement element = source.InputHitTest(point) as System.Windows.UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                        element = System.Windows.Media.VisualTreeHelper.GetParent(element) as System.Windows.UIElement;
                    if (element == source)
                        return null;
                }

                if (data != DependencyProperty.UnsetValue)
                    return data;
            }
            return null;
        }

        private void LayerListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Project.Layer)) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                ListBox listBox = sender as ListBox;
                if (listBox != null && listBox.SelectedItem != null)
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
        }

        private void LayerListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Project.Layer)))
            {
                ListBox listBox = sender as ListBox;
                if (listBox != null)
                {
                    Project.Layer data = e.Data.GetData(typeof(Project.Layer)) as Project.Layer;
                    Project.Layer target = listBox.SelectedItem as Project.Layer;

                    // Ensure that the dragged item and the target item are not null
                    if (data != null && target != null)
                    {
                        // Remove the dragged item from the collection
                        LayersOC.Remove(data);

                        // Find the index of the target item in the collection
                        int index = LayersOC.IndexOf(target);

                        // Ensure that the index is valid
                        if (index >= 0 && index < LayersOC.Count)
                        {
                            // Insert the dragged item at the calculated index
                            LayersOC.Insert(index, data);
                        }
                        else
                        {
                            // If the index is out of range, add the dragged item to the end of the collection
                            LayersOC.Add(data);
                        }
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null && checkBox.DataContext is Project.Layer layerItem)
            {
                layerItem.Visible = true;
            }

            Events.RaiseLayerUpdate(EventArgs.Empty);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null && checkBox.DataContext is Project.Layer layerItem)
            {
                layerItem.Visible = false;
            }

            Events.RaiseLayerUpdate(EventArgs.Empty);
        }

        private void CheckBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void LayerListBox_MouseMove(object sender, MouseEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null)
                return;

            // Get the dragged item under the mouse
            object data = GetDataFromListBox(listBox, e.GetPosition(listBox));

            // Check if the mouse click is directly on the CheckBox or its child elements
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null && dep != listBox)
            {
                if (dep is CheckBox || dep is ContentPresenter)
                {
                    // If the mouse click is on the CheckBox or its child elements, return without initiating drag
                    return;
                }
                dep = VisualTreeHelper.GetParent(dep);
            }

            // Start the drag operation
            if (data != null)
            {
                DragDrop.DoDragDrop(listBox, data, DragDropEffects.Move);
            }
        }

        private void Dupe_Click(object sender, RoutedEventArgs e)
        {
            LayersOC.Insert(LayerListBox.SelectedIndex + 1, LayersOC[LayerListBox.SelectedIndex]);
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if(LayersOC.Count == 1) return;

            LayersOC.RemoveAt(LayerListBox.SelectedIndex);
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if(LayerListBox.SelectedIndex == -1)
            {
                LayerListBox.SelectedIndex = 0;
            }

            if (LayerListBox.SelectedIndex == 0) MoveUp.IsEnabled = false;
            else MoveUp.IsEnabled = true;

            if (LayerListBox.SelectedIndex == LayersOC.Count - 1) MoveDown.IsEnabled = false;
            else MoveDown.IsEnabled = true;

            if (LayersOC.Count == 1) Del.IsEnabled = false;
            else Del.IsEnabled = true;
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            Layer _movingLayer = LayersOC[LayerListBox.SelectedIndex];
            
            LayersOC.Insert(LayerListBox.SelectedIndex - 1, _movingLayer);
            LayersOC.RemoveAt(LayerListBox.SelectedIndex);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            Layer _movingLayer = LayersOC[LayerListBox.SelectedIndex];

            LayersOC.Insert(LayerListBox.SelectedIndex + 2, _movingLayer);
            LayersOC.RemoveAt(LayerListBox.SelectedIndex);
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            Windows.Rename rename = new Windows.Rename();
            rename.Start(LayersOC[LayerListBox.SelectedIndex].Name, "Layer Name");
            rename.ShowDialog();

            LayersOC[LayerListBox.SelectedIndex].Name = rename.ChangedText;

            UpdateLayers();
        }

        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in LayersOC)
            {
                item.Selected = true;
            }
        }

        private void SelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in LayersOC)
            {
                item.Selected = false;
            }
        }
    }
}
