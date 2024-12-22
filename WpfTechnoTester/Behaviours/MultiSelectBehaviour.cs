using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace WpfTechnoTester.Behaviours
{
    public static class MultiSelectBehavior
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItems",
                typeof(IList),
                typeof(MultiSelectBehavior),
                new PropertyMetadata(null, OnSelectedItemsChanged));

        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListView listView)
            {
                listView.SelectionChanged -= OnSelectionChanged;
                listView.SelectionChanged += OnSelectionChanged;
            }
        }

        private static void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                var selectedItems = GetSelectedItems(listView);
                if (selectedItems != null)
                {
                    selectedItems.Clear();
                    foreach (var item in listView.SelectedItems)
                    {
                        selectedItems.Add(item);
                    }
                }
            }
        }
    }
}