using System;
using System.Collections.Generic;
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
        List<string> items = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
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

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.Data.GetData(typeof(TextBlock));
            TextBlock block = (TextBlock)sender;
            TreeViewItem treeViewItem = (TreeViewItem)textBlock.Parent;

            if (IsMatch(treeViewItem.Uid, block.Uid))
            {
                Check(textBlock.Text, block.Uid);
                if (block.Text == "")
                {
                    block.Text = textBlock.Text;
                }
                else
                {
                   items = block.Text.Split(", ").ToList();
                    if (!items.Contains(textBlock.Text))
                    {
                        items.Add(textBlock.Text);
                    }

                    block.Text = string.Join(", ", items);

                }
               
            }

        }

        private void Check(string item, string blockId)
        {
            List<string> itemList = new List<string>();

            if (blockId != "5")
            {
                itemList = blockE.Text.Split(", ").ToList();
                if (itemList.Contains(item))
                {
                    itemList.Remove(item);
                    blockE.Text = string.Join(", ", itemList);
                }
                return;
            }

            List<TextBlock> textBlocks = new List<TextBlock> { blockA, blockB, blockC };

            foreach(var block in textBlocks)
            {
                itemList = block.Text.Split(", ").ToList();
                if (itemList.Contains(item))
                {
                    itemList.Remove(item);
                    block.Text = string.Join(", ", itemList);
                }
            }
           
        }

        private bool IsMatch(string dragItem, string dragTarget)
        {
            if (dragTarget == "5")
            {
                targetE.Background = Brushes.LimeGreen;
                return true;
            }
            if(dragItem == dragTarget)
            {
                return true;
            }
            return false;
        }

        private void TextBlock_DragEnter(object sender, DragEventArgs e)
        {
            TextBlock textBlock = (TextBlock)e.Data.GetData(typeof(TextBlock));
            TextBlock block = (TextBlock)sender;
            TreeViewItem treeViewItem = (TreeViewItem)textBlock.Parent;
            SetBoxColor(IsMatch(treeViewItem.Uid, block.Uid), block.Uid);
        }        

        private void SetBoxColor(bool b, string id)
        {
            Brush brush = Brushes.LimeGreen;
            if (!b)
            {
                brush = Brushes.Salmon;
            }
            switch (id)
            {
                case "1":
                    targetA.Background = brush;
                    break;
                case "2":
                    targetB.Background = brush;
                    break;
                case "3":
                    targetC.Background = brush;
                    break;
                case "4":
                    targetD.Background = brush;
                    break;
                case "5":
                    targetE.Background = brush;
                    break;
            }
        }

        private void TextBlock_DragLeave(object sender, DragEventArgs e)
        {
            AllBlack();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            TextBlock textBlock = (TextBlock)sender;
            textBlock.Background = Brushes.Purple;

            DataObject dataObject = new DataObject(textBlock);
            DragDrop.DoDragDrop(textBlock, dataObject, DragDropEffects.Move);
            AllBlack();
        }

        private void AllBlack()
        {
            Brush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF161B1B"));
            targetA.Background = brush;
            targetB.Background = brush;
            targetC.Background = brush;
            targetD.Background = brush;
            targetE.Background = brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            blockA.Text = "";
            blockB.Text = "";
            blockC.Text = "";
            blockE.Text = "";
        }
    }
}