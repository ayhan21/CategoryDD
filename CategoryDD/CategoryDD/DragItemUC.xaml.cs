using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CategoryDD
{
    /// <summary>
    /// Interaction logic for DragItemUC.xaml
    /// </summary>
    public partial class DragItemUC : UserControl
    {
        ObservableCollection<Item> items;
        public ObservableCollection<Item> Items { get { return items; } set { items = value; } }

        public DragItemUC()
        {
            InitializeComponent();

            items = new ObservableCollection<Item>
            {
                new Item
                {
                    Name = "Category A",
                    Children = new ObservableCollection<Item>
                    {
                        new Item
                        {
                            Name = "Item A1"
                        },
                        new Item
                        {
                            Name = "Item A2"
                        },
                        new Item
                        {
                            Name = "Item A3"
                        },
                        new Item
                        {
                            Name = "Item A4"
                        }
                    }
                },
                 new Item
                {
                    Name = "Category B",
                    Children = new ObservableCollection<Item>
                    {
                        new Item
                        {
                            Name = "Item B1"
                        },
                        new Item
                        {
                            Name = "Item B2"
                        },
                        new Item
                        {
                            Name = "Item B3"
                        },
                        new Item
                        {
                            Name = "Item B4"
                        }
                    }
                 },
                    new Item
                    {
                    Name = "Category C",
                    Children = new ObservableCollection<Item>
                    {
                        new Item
                        {
                            Name = "Item C1"
                        },
                        new Item
                        {
                            Name = "Item C2"
                        },
                        new Item
                        {
                            Name = "Item C3"
                        },
                        new Item
                        {
                            Name = "Item C4"
                        }
                    }
                }
            };

            treeView.ItemsSource = items;

            Item item = new Item { Name = "Category D" };
            AddNewCat(item);
            AddNewItem(item, "Item D1");
        }

        public void AddNewCat(Item item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }

        public void AddNewItem(Item category, string itemName)
        {
            if (items.Contains(category))
            {
                category.Children.Add(new Item { Name = itemName });
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            textBlock.Background = Brushes.Purple;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            textBlock.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF161B1B"));
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TextBlock textBlock = (TextBlock)sender;
                DataObject dataObject = new DataObject(textBlock);

                DragDrop.DoDragDrop(textBlock, dataObject, DragDropEffects.Move | DragDropEffects.Copy);
            }
        }
    }
}
