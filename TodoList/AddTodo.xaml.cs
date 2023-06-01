using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ContactList.Classes;

namespace ContactList;

public partial class AddTodo : Window
{
    private MainWindow _mainWindow;
    public AddTodo(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        InitializeComponent();
    }
    

    private void AddContact_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var title = TitleTextBox.Text;
            var description = DescriptionTextBox.Text;
            var year = Convert.ToInt32(YearTextBox.Text);
            var month = Convert.ToInt32(MonthTextBox.Text);
            var day = Convert.ToInt32(DayTextBox.Text);

            var date = new DateTime(year, month, day);
            var todo = new Todo(title, description, date);

            _mainWindow.AddToList(todo);
            Close();
        }
        catch (FormatException)
        {
            MessageBox.Show("Erreure dans la date saisie");
        }
        catch (ArgumentOutOfRangeException)
        {
            MessageBox.Show("date incorrecte");
        }
       
    }
    
}