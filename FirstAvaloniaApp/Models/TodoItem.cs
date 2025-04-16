using System;

namespace FirstAvaloniaApp.Models
{
    /// <summary>
    /// Represents a single todo item in the application
    /// </summary>
    public class TodoItem
    {
        // Unique identifier for the todo item
        public Guid Id { get; set; } = Guid.NewGuid();
        
        // The title or short description of the todo item
        public string Title { get; set; } = string.Empty;
        
        // A more detailed description of the todo item (optional)
        public string Description { get; set; } = string.Empty;
        
        // Indicates whether the todo item has been completed
        public bool IsCompleted { get; set; }
        
        // The date when the todo item was created
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 