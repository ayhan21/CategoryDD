using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CategoryDD
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Item> items;
        List<string> blockItems = new List<string>();

        public MainWindow()
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
            addNewCat("Category D");
        }

        public void addNewCat(string name)
        {
            items.Add(new Item { Name = name });
        }

        private void TextBlock_DragEnter(object sender, DragEventArgs e)
        {
            TextBlock itemBlock = (TextBlock)e.Data.GetData(typeof(TextBlock));
            TextBlock targetBlock = (TextBlock)sender;

            if (IsMatch(itemBlock, targetBlock))
            {
                targetBlock.Background = Brushes.LimeGreen;
            }
            else
            {
                targetBlock.Background = Brushes.Salmon;
            }

        }
        private void TextBlock_DragLeave(object sender, DragEventArgs e)
        {
            TextBlock block = (TextBlock)sender;
            block.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF161B1B"));
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            StackPanel stackPanel = (StackPanel)textBlock.Parent;
            DataObject dataObject = new DataObject(textBlock);
            DragDrop.DoDragDrop(textBlock, dataObject, DragDropEffects.Move);
        }

        private void TextBlock_DragOver(object sender, DragEventArgs e)
        {

        }

        private bool IsMatch(TextBlock dragItem, TextBlock target)
        {
            string category = GetCategory(dragItem);

            if (category.Equals(target.Uid) || target.Uid == "Category E")
            {
                return true;
            }

            return false;
        }

        public static UIElement GetByUid(DependencyObject rootElement, string uid)
        {
            foreach (UIElement element in LogicalTreeHelper.GetChildren(rootElement).OfType<UIElement>())
            {
                if (element.Uid == uid)
                    return element;
                UIElement resultChildren = GetByUid(element, uid);
                if (resultChildren != null)
                    return resultChildren;
            }
            return null;
        }

        private void SetText(TextBlock dragItem, TextBlock target)
        {
            TextBlock textDest = (TextBlock)GetByUid(stack, target.Uid);
            string s = textDest.Uid;

            if (textDest.Text == "")
            {
                textDest.Text = dragItem.Text;
            }
            else
            {
                blockItems = textDest.Text.Split(", ").ToList();
                if (!blockItems.Contains(dragItem.Text))
                {
                    blockItems.Add(dragItem.Text);
                }
                textDest.Text = string.Join(", ", blockItems);
            }
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            TextBlock itemBlock = (TextBlock)e.Data.GetData(typeof(TextBlock));
            TextBlock targetBlock = (TextBlock)sender;
            targetBlock.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF161B1B"));

            if (IsMatch(itemBlock, targetBlock))
            {
                Check(itemBlock.Text, GetByUid(stack, targetBlock.Uid).Uid);
                SetText(itemBlock, targetBlock);
            }
        }

        private string GetCategory(TextBlock dragItem)
        {
            string category = "";
            foreach (var i in items)
            {
                foreach (var c in i.Children)
                {
                    if (c.Name.Equals(dragItem.Text))
                    {
                        category = i.Name;
                    }
                }
            }
            return category;
        }

        private void Check(string newItem, string blockId)
        {
            List<string> itemList = new List<string>();

            if (blockId != "Category E")
            {
                itemList = blockE.Text.Split(", ").ToList();
                if (itemList.Contains(newItem))
                {
                    itemList.Remove(newItem);
                    blockE.Text = string.Join(", ", itemList);
                }
                return;
            }

            List<TextBlock> textBlocks = new List<TextBlock> { blockA, blockB, blockC };

            foreach (var block in textBlocks)
            {
                itemList = block.Text.Split(", ").ToList();
                if (itemList.Contains(newItem))
                {
                    itemList.Remove(newItem);
                    block.Text = string.Join(", ", itemList);
                }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            blockA.Text = "";
            blockB.Text = "";
            blockC.Text = "";
            blockD.Text = "";
            blockE.Text = "";
        }
    }

    public class Item
    {
        public Item()
        {
            Children = new ObservableCollection<Item>();
        }
        public string Name { get; set; }
        public ObservableCollection<Item> Children { get; set; }
    }
}