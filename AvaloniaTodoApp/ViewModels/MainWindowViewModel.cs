using System;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaTodoApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTodoApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // Property to store the new todo text being entered
    [ObservableProperty] private string _newTodoTitle = string.Empty;

    // Selected todo item for editing or viewing details
    [ObservableProperty] private TodoItemViewModel? _selectedTodo;

    // Collection to store and manage todo items
    [ObservableProperty] private ObservableCollection<TodoItemViewModel> _todoItems = new();

    // Constructor to initialize with some sample data
    public MainWindowViewModel()
    {
        // Adding some sample todo items
        AddSampleTodos();

        // Update task counts when collection changes
        TodoItems.CollectionChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(CompletedCount));
            OnPropertyChanged(nameof(PendingCount));
        };
    }

    // Title for the application window
    public string Title => "Avalonia Todo App";

    // Current date for display in the header
    public string CurrentDate => DateTime.Now.ToString("dddd, MMMM d, yyyy");

    // Computed property for completed tasks count
    public int CompletedCount => TodoItems.Count(t => t.IsCompleted);

    // Computed property for pending tasks count
    public int PendingCount => TodoItems.Count(t => !t.IsCompleted);

    // Command to add a new todo item
    [RelayCommand]
    private void AddTodo()
    {
        // Don't add empty todos
        if (string.IsNullOrWhiteSpace(NewTodoTitle))
            return;

        // Create a new TodoItem
        var newTodo = new TodoItem
        {
            Title = NewTodoTitle,
            CreatedAt = DateTime.Now
        };

        // Create the view model
        var todoViewModel = new TodoItemViewModel(newTodo);

        // Subscribe to delete events
        todoViewModel.DeleteRequested += (s, e) => RemoveTodo(todoViewModel);

        // Add it to the collection
        TodoItems.Add(todoViewModel);

        // Clear the input field for the next todo
        NewTodoTitle = string.Empty;
    }

    // Command to remove a todo item
    [RelayCommand]
    private void RemoveTodo(TodoItemViewModel todo)
    {
        if (SelectedTodo == todo)
            SelectedTodo = null;

        TodoItems.Remove(todo);
    }

    // Command to toggle the completion status of a todo
    [RelayCommand]
    private void ToggleTodoCompletion(TodoItemViewModel todo)
    {
        todo.IsCompleted = !todo.IsCompleted;
        OnPropertyChanged(nameof(CompletedCount));
        OnPropertyChanged(nameof(PendingCount));
    }

    // Command to clear all completed tasks
    [RelayCommand]
    private void ClearCompleted()
    {
        // Get completed items (create a new list to avoid collection modification issues)
        var completedItems = TodoItems.Where(t => t.IsCompleted).ToList();

        // If nothing to clear, return
        if (!completedItems.Any())
            return;

        // If selected item is among completed, clear selection
        if (SelectedTodo != null && SelectedTodo.IsCompleted)
            SelectedTodo = null;

        // Remove all completed items
        foreach (var item in completedItems) TodoItems.Remove(item);
    }

    // Helper method to add sample data
    private void AddSampleTodos()
    {
        var items = new[]
        {
            new TodoItemViewModel(new TodoItem
                { Title = "Create UI mockups", Description = "Design the interface for the new dashboard feature" }),
            new TodoItemViewModel(new TodoItem
            {
                Title = "Finish project proposal", Description = "Complete the budget section and timeline estimates",
                IsCompleted = true
            }),
            new TodoItemViewModel(new TodoItem
            {
                Title = "Review pull requests", Description = "Check code changes and provide feedback to the team"
            }),
            new TodoItemViewModel(new TodoItem
            {
                Title = "Attend weekly meeting", Description = "Prepare progress report for team status meeting",
                CreatedAt = DateTime.Now.AddDays(-1)
            }),
            new TodoItemViewModel(new TodoItem
            {
                Title = "Research new technologies",
                Description = "Evaluate potential tech stack for the upcoming project"
            })
        };

        foreach (var item in items)
        {
            item.DeleteRequested += (s, e) => RemoveTodo(item);
            TodoItems.Add(item);
        }
    }
}