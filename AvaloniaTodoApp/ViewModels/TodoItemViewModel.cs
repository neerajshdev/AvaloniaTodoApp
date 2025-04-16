using System;
using AvaloniaTodoApp.Models;

namespace AvaloniaTodoApp.ViewModels;

/// <summary>
///     ViewModel for TodoItem that provides data binding and property change notifications
/// </summary>
public class TodoItemViewModel : ViewModelBase
{
    // Constructor that takes a TodoItem model
    public TodoItemViewModel(TodoItem todoItem)
    {
        TodoItem = todoItem;
    }

    // Property for accessing the underlying TodoItem model
    public TodoItem TodoItem { get; }

    // Title property with change notification
    public string Title
    {
        get => TodoItem.Title;
        set
        {
            if (TodoItem.Title != value)
            {
                TodoItem.Title = value;
                OnPropertyChanged();
            }
        }
    }

    // Description property with change notification
    public string Description
    {
        get => TodoItem.Description;
        set
        {
            if (TodoItem.Description != value)
            {
                TodoItem.Description = value;
                OnPropertyChanged();
            }
        }
    }

    // IsCompleted property with change notification
    public bool IsCompleted
    {
        get => TodoItem.IsCompleted;
        set
        {
            if (TodoItem.IsCompleted != value)
            {
                TodoItem.IsCompleted = value;
                OnPropertyChanged();
            }
        }
    }

    // Created date as formatted string (read-only)
    public string CreatedDate => TodoItem.CreatedAt.ToString("MM/dd/yyyy");

    // Event that will be raised when this item should be deleted
    public event EventHandler? DeleteRequested;

    // Method to request deletion of this item
    public void Delete()
    {
        DeleteRequested?.Invoke(this, EventArgs.Empty);
    }
}