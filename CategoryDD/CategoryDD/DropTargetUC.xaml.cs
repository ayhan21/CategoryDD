using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for DropTargetUC.xaml
    /// </summary>
    public partial class DropTargetUC : UserControl
    {
        ObservableCollection<Item> items;
        List<string> blockItems = new List<string>();

        public ObservableCollection<Item> Items { get { return items; } set { items = value; } }


        public DropTargetUC()
        {
            InitializeComponent();
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

            List<TextBlock> textBlocks = new List<TextBlock> { blockA, blockB, blockC, blockD };

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            blockA.Text = "";
            blockB.Text = "";
            blockC.Text = "";
            blockD.Text = "";
            blockE.Text = "";
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

        private bool IsMatch(TextBlock dragItem, TextBlock target)
        {
            string category = GetCategory(dragItem);

            if (category.Equals(target.Uid) || target.Uid == "Category E")
            {
                return true;
            }

            return false;
        }
    }

}
