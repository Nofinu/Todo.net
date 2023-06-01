using System;
using System.Collections.Generic;
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
using ContactList.Classes;

namespace ContactList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Todo> _Todos = new List<Todo>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTodoOpen_OnClick(object sender, RoutedEventArgs e)
        {
            var windows = new AddTodo(this);
            windows.Owner = this;
            windows.ShowDialog();
        }

        public void AddToList(Todo todo)
        { 
            _Todos.Add(todo);
            showContact();
        }

        private void showContact()
        {
            StackTodo.Children.Clear();
            var date = DateTime.Today;
            foreach (var t in _Todos)
            {
                int resultDate = date.CompareTo(t.Date);
                var button = new Button();
                button.Content = "Titre : "+t.Title + "\nDescription : " + t.Description+"\nDate : "+t.Date.ToString("d");
                button.Foreground = Brushes.White ;
                if (resultDate < 0)
                    button.Background = Brushes.Green;
                else if (resultDate == 0)
                    button.Background = Brushes.DarkGoldenrod;
                else
                    button.Background = Brushes.DarkRed;

                button.BorderBrush = Brushes.Transparent;
                button.TabIndex = _Todos.IndexOf(t);
                button.Width = 350;
                button.FontSize = 15;
                button.Margin =new Thickness(10);
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.Click += DeleteTodo;
                StackTodo.Children.Add(button);
            }
        }

        private void DeleteTodo(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = button.TabIndex;
            _Todos.RemoveAt(id);
            showContact();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}