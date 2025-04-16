using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using AvaloniaTodoApp.ViewModels;

namespace AvaloniaTodoApp.Views;

public partial class MainWindow : Window
{
    private Button? _statusToggleButton;
    private TextBlock? _taskStatusText;

    public MainWindow()
    {
        InitializeComponent();

        // Subscribe to delete events from TodoItemViews
        AddHandler(TodoItemView.DeleteRequestedEvent, HandleTodoDeleteRequested);

        // Find UI elements when loaded
        Loaded += (s, e) =>
        {
            _taskStatusText = this.FindControl<TextBlock>("TaskStatusText");
            _statusToggleButton = this.FindControl<Button>("StatusToggleButton");

            // Set up property change detection
            if (DataContext is MainWindowViewModel viewModel)
            {
                // Set initial empty state
                UpdateTaskStatusDisplay(null);

                // Monitor selection changes
                viewModel.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(MainWindowViewModel.SelectedTodo))
                        UpdateTaskStatusDisplay(viewModel.SelectedTodo);
                };
            }
        };
    }

    private void UpdateTaskStatusDisplay(TodoItemViewModel? todoItem)
    {
        if (_taskStatusText == null || _statusToggleButton == null) return;

        if (todoItem == null)
        {
            // No todo selected
            _taskStatusText.Text = "No task selected";
            _statusToggleButton.IsVisible = false;
            return;
        }

        // Update status display
        var isCompleted = todoItem.IsCompleted;
        _taskStatusText.Text = isCompleted ? "Completed" : "Pending";

        // Update button
        _statusToggleButton.IsVisible = true;
        _statusToggleButton.Content = isCompleted ? "Mark Pending" : "Mark Completed";
        _statusToggleButton.Background = new SolidColorBrush(
            isCompleted ? Color.Parse("#52de97") : Color.Parse("#f2a365")
        );

        // Update when IsCompleted changes
        todoItem.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(TodoItemViewModel.IsCompleted))
            {
                var completed = todoItem.IsCompleted;
                _taskStatusText.Text = completed ? "Completed" : "Pending";
                _statusToggleButton.Content = completed ? "Mark Pending" : "Mark Completed";
                _statusToggleButton.Background = new SolidColorBrush(
                    completed ? Color.Parse("#52de97") : Color.Parse("#f2a365")
                );
            }
        };
    }

    private void HandleTodoDeleteRequested(object? sender, RoutedEventArgs e)
    {
        // The sender is the TodoItemView
        if (sender is TodoItemView todoItemView &&
            todoItemView.DataContext is TodoItemViewModel todoItem &&
            DataContext is MainWindowViewModel mainViewModel)
            // Call the remove command
            mainViewModel.RemoveTodoCommand.Execute(todoItem);

        // Mark the event as handled
        e.Handled = true;
    }
}