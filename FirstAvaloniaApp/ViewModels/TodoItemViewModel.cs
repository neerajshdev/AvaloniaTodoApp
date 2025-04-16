using System;
using FirstAvaloniaApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FirstAvaloniaApp.ViewModels
{
    /// <summary>
    /// ViewModel for TodoItem that provides data binding and property change notifications
    /// </summary>
    public class TodoItemViewModel : ViewModelBase
    {
        private TodoItem _todoItem;
        
        // Constructor that takes a TodoItem model
        public TodoItemViewModel(TodoItem todoItem)
        {
            _todoItem = todoItem;
        }
        
        // Property for accessing the underlying TodoItem model
        public TodoItem TodoItem => _todoItem;
        
        // Title property with change notification
        public string Title
        {
            get => _todoItem.Title;
            set
            {
                if (_todoItem.Title != value)
                {
                    _todoItem.Title = value;
                    OnPropertyChanged();
                }
            }
        }
        
        // Description property with change notification
        public string Description
        {
            get => _todoItem.Description;
            set
            {
                if (_todoItem.Description != value)
                {
                    _todoItem.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        
        // IsCompleted property with change notification
        public bool IsCompleted
        {
            get => _todoItem.IsCompleted;
            set
            {
                if (_todoItem.IsCompleted != value)
                {
                    _todoItem.IsCompleted = value;
                    OnPropertyChanged();
                }
            }
        }
        
        // Created date as formatted string (read-only)
        public string CreatedDate => _todoItem.CreatedAt.ToString("MM/dd/yyyy");

        // Event that will be raised when this item should be deleted
        public event EventHandler? DeleteRequested;

        // Method to request deletion of this item
        public void Delete()
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }
} 