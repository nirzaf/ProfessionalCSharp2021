﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUICultureDemo
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Activated += OnActivated;
        }

        private void OnSelectionChanged(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is TreeViewNode node && node.Content is CultureData cd)
            {
                ViewModel.SelectedCulture = cd;
            }
        }

        public CulturesViewModel ViewModel { get; } = new CulturesViewModel();

        private void OnActivated(object sender, WindowActivatedEventArgs args)
        {
            void AddSubNodes(TreeViewNode parent)
            {
                if (parent.Content is CultureData cd && cd.SubCultures is not null)
                {
                    foreach (var culture in cd.SubCultures)
                    {
                        TreeViewNode node = new()
                        {
                            Content = culture
                        };
                        parent.Children.Add(node);

                        foreach (var subCulture in culture.SubCultures)
                        {
                            AddSubNodes(node);
                        }
                    }
                }
            }

            var rootNodes = ViewModel.RootCultures.Select(cd => new TreeViewNode
            {
                Content = cd
            });

            foreach (var node in rootNodes)
            {
                treeView1.RootNodes.Add(node);
                AddSubNodes(node);
            }
        }
    }
}
