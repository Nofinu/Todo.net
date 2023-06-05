using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
        private Boolean todoCompleted = false;
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
                var resultDate = date.CompareTo(t.Date);
                var myGrid = new Grid();
                myGrid.Margin = new Thickness(15);
                myGrid.HorizontalAlignment = HorizontalAlignment.Left;
                myGrid.Width = 500;
           

                var colDef1 = new ColumnDefinition();
                var colDef2 = new ColumnDefinition();
                var colDef3 = new ColumnDefinition();
                myGrid.ColumnDefinitions.Add(colDef1);
                myGrid.ColumnDefinitions.Add(colDef2);
                myGrid.ColumnDefinitions.Add(colDef3);

                var rowDef1 = new RowDefinition();
                var rowDef2 = new RowDefinition();
                myGrid.RowDefinitions.Add(rowDef1);
                myGrid.RowDefinitions.Add(rowDef2);


                var textTitle = new TextBlock();
                textTitle.Text = t.Title;
                textTitle.Foreground = Brushes.White;
                textTitle.FontSize = 18;
                textTitle.Margin = new Thickness(0,0,15,0);
                Grid.SetColumn(textTitle,0);
                Grid.SetRow(textTitle,0);
                
                var textDescription = new TextBlock();
                textDescription.Text = t.Description;
                textDescription.Foreground = Brushes.White;
                textDescription.FontSize = 15;
                textDescription.Margin = new Thickness(0,0,15,0);
                Grid.SetColumnSpan(textDescription,2);
                Grid.SetRow(textDescription,1);

                var textDate = new TextBlock();
                textDate.Text = t.Date.ToString("d");
                textDate.FontSize = 15;
                textDate.Margin = new Thickness(0,0,15,0);
                switch (resultDate)
                {
                    case 1:
                        textDate.Foreground = Brushes.DarkRed;
                        break;
                    case 0:
                        textDate.Foreground = Brushes.DarkGoldenrod;
                        break;
                    case -1:
                        textDate.Foreground = Brushes.Green;
                        break;
                }
                Grid.SetColumn(textDate,1);
                Grid.SetRow(textDate,0);

                var buttonDelete = new Button();
                buttonDelete.Content = "Delete";
                buttonDelete.TabIndex = _Todos.IndexOf(t);
                buttonDelete.Click += DeleteTodo;
                buttonDelete.Background = Brushes.DarkRed;
                buttonDelete.Foreground = Brushes.White;
                buttonDelete.BorderBrush = Brushes.Transparent;
                Grid.SetRow(buttonDelete,0);
                Grid.SetColumn(buttonDelete,2);
                
                var buttonStatus = new Button();
                buttonStatus.Content = "check";
                buttonStatus.TabIndex = _Todos.IndexOf(t);
                buttonStatus.Foreground = Brushes.White;
                buttonStatus.BorderBrush = Brushes.Transparent;
                buttonStatus.Click += changeStatus;
                buttonStatus.Margin = new Thickness(0, 10, 0, 0);
                Grid.SetRow(buttonStatus,1);
                Grid.SetColumn(buttonStatus,2);
                
                if (todoCompleted && !t.Status)
                {
                    buttonStatus.Background = Brushes.DarkGoldenrod;
                    myGrid.Children.Add(textTitle);
                    myGrid.Children.Add(textDescription);
                    myGrid.Children.Add(textDate);
                    myGrid.Children.Add(buttonDelete);
                    myGrid.Children.Add(buttonStatus);
                    StackTodo.Children.Add(myGrid);
                }else if (!todoCompleted)
                {
                    if (t.Status)
                    {
                        myGrid.Background = Brushes.DarkGreen;
                        buttonStatus.Background = Brushes.Green;
                        myGrid.Opacity = .50;
                    }
                    else
                        buttonStatus.Background = Brushes.DarkGoldenrod;
                    
                    myGrid.Children.Add(textTitle);
                    myGrid.Children.Add(textDescription);
                    myGrid.Children.Add(textDate);
                    myGrid.Children.Add(buttonDelete);
                    myGrid.Children.Add(buttonStatus);
                    StackTodo.Children.Add(myGrid);
                    
                }
                
            }
        }

        private void DeleteTodo(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = button.TabIndex;
            _Todos.RemoveAt(id);
            showContact();
        }

        private void changeStatus(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var id = button.TabIndex;
            var status = !_Todos[id].Status;
            _Todos[id].Status = status;
            showContact();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RadioChecked_OnChecked(object sender, RoutedEventArgs e)
        {
            todoCompleted = !todoCompleted;
            if (todoCompleted)
            {
                buttonChecked.Content = "afficher toute les todos";
            }
            else
            {
                buttonChecked.Content = "afficher les todos a terminer";
            }
            showContact();
        }
    }
}