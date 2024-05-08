using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Main.Entities;

namespace Main;

public partial class PerformanceEditor : Window
{
    public PerformanceEditor()
    {
        InitializeComponent();
    }

    private void UpdateObjectsList()
    {
        StringsList.Children.Clear();
        foreach (Performance perf in Employee.Performances)
        {
            Grid itemPanel = new Grid();
            itemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            itemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
            itemPanel.Margin = new Thickness(2, 0, 2, 3);

            TextBox itemTextBlock = new TextBox() { Text = perf.Score.ToString() };
            itemTextBlock.PreviewTextInput += (sender, args) => args.Handled = IsTextAllowed(args.Text);
            itemTextBlock.Margin = new Thickness(0, 0, 3, 0);
            itemTextBlock.TextChanged += (sender, args) =>
            {
                if (int.TryParse(itemTextBlock.Text, out int score))
                {
                    perf.Score = score;
                }
            };
            Grid.SetColumn(itemTextBlock, 0);

            Button deleteButton = new Button() { Content = "Delete" };
            deleteButton.Click += (sender, args) =>
            {
                Employee.Performances.Remove(perf);
                UpdateObjectsList();
            };
            Grid.SetColumn(deleteButton, 1);

            itemPanel.Children.Add(itemTextBlock);
            itemPanel.Children.Add(deleteButton);
            StringsList.Children.Add(itemPanel);
        }
    }

    private static readonly Regex _regex = new Regex("^[0-9]+$"); //regex that matches disallowed text
    private static bool IsTextAllowed(string text)
    {
        return !_regex.IsMatch(text);
    }

    private void OnAddNewItem(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(ItemBox.Text, out int score))
        {
            Employee.Performances.Add(new Performance() { Score = score });
            UpdateObjectsList();
        }
    }

    public Employee Employee
    {
        get => employee;
        set
        {
            employee = value;
            UpdateObjectsList();
        }
    }

    private Employee employee;

    private void IsAllowedText(object sender, TextCompositionEventArgs e)
    {
        e.Handled = IsTextAllowed(e.Text);
    }
}