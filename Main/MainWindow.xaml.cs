using System.Windows;
using System.Windows.Controls;
using Main.Entities;

namespace Main;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void UpdateEmployeesList()
    {
        EmployeesList.Children.Clear();
        foreach(Employee employee in employees)
        {
            Grid employeeGrid = new();
            employeeGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});
            employeeGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});
            employeeGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});
            employeeGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});
            employeeGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});
            employeeGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});

            TextBox nameBox = new() { Text = employee.Name };
            nameBox.TextChanged += (sender, e) => employee.Name = nameBox.Text;

            TextBox surnameBox = new() { Text = employee.Surname };
            surnameBox.TextChanged += (sender, e) => employee.Surname = surnameBox.Text;

            TextBox positionBox = new() { Text = employee.Position };
            positionBox.TextChanged += (sender, e) => employee.Position = positionBox.Text;

            Label averageScoreBox = new() { Content = $"Average: {employee.AverageScore}" };

            Button editPerformancesButton = new() { Content = "Edit Performances" };
            editPerformancesButton.Click += (sender, e) => {
                PerformanceEditor editor = new();
                editor.Employee = employee;
                editor.ShowDialog();
                averageScoreBox.Content = $"Average: {employee.AverageScore}";
            };

            Button removeButton = new() { Content = "Remove" };
            removeButton.Click += (sender, e) => {
                employees.Remove(employee);
                UpdateEmployeesList();
            };

            Grid.SetColumn(nameBox, 0);
            Grid.SetColumn(surnameBox, 1);
            Grid.SetColumn(positionBox, 2);
            Grid.SetColumn(averageScoreBox, 3);
            Grid.SetColumn(editPerformancesButton, 4);
            Grid.SetColumn(removeButton, 5);

            employeeGrid.Children.Add(nameBox);
            employeeGrid.Children.Add(surnameBox);
            employeeGrid.Children.Add(positionBox);
            employeeGrid.Children.Add(averageScoreBox);
            employeeGrid.Children.Add(editPerformancesButton);
            employeeGrid.Children.Add(removeButton);

            EmployeesList.Children.Add(employeeGrid);
        }

    }

    private void OnAddNewEmployee(object sender, RoutedEventArgs e)
    {
        Employee employee = new();
        employee.Name = NameBox.Text;
        employee.Surname = SurnameBox.Text;
        employee.Position = PositionBox.Text;
        employees.Add(employee);
        UpdateEmployeesList();
    }

    private List<Employee> employees = new();
}