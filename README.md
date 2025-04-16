# Avalonia Todo App

A modern, cross-platform Todo application built with [Avalonia UI](https://avaloniaui.net/) and .NET 8.

## Features

- Create, edit, and delete todo items
- Mark todo items as complete
- Persistent storage of todo items
- Clean, modern user interface
- Cross-platform compatibility (Windows, macOS, Linux)

## Screenshots

![image](https://github.com/user-attachments/assets/7700c8d9-d8a1-4ec8-907f-9f7a96c473ef)

## Technology Stack

- **Framework**: .NET 8
- **UI Library**: Avalonia UI 11.2.7
- **Architecture**: MVVM (Model-View-ViewModel) using CommunityToolkit.Mvvm
- **Language**: C#

## Project Structure

The application follows a standard MVVM architecture:

- **Models**: Data structures representing the core business logic
  - `TodoItem.cs`: Represents a single todo item

- **ViewModels**: Intermediary between the Model and View layers
  - `ViewModelBase.cs`: Base class for all ViewModels
  - `MainWindowViewModel.cs`: ViewModel for the main window
  - `TodoItemViewModel.cs`: ViewModel for individual todo items

- **Views**: UI components
  - `MainWindow.axaml`: Main application window
  - `TodoItemView.axaml`: View for displaying and editing todo items

## Getting Started

### Prerequisites

- .NET 8 SDK or later
- IDE (Visual Studio, Rider, or VS Code with C# extensions)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/AvaloniaTodoApp.git
   ```

2. Navigate to the project directory:
   ```
   cd AvaloniaTodoApp
   ```

3. Build the application:
   ```
   dotnet build
   ```

4. Run the application:
   ```
   dotnet run --project AvaloniaTodoApp
   ```

## Development

### Adding a New Feature

1. Create or modify the appropriate Model classes
2. Implement the ViewModel logic
3. Design the View in AXAML
4. Connect the View to the ViewModel using data binding

### Building for Different Platforms

```
# Windows
dotnet publish -c Release -r win-x64 --self-contained

# macOS
dotnet publish -c Release -r osx-x64 --self-contained

# Linux
dotnet publish -c Release -r linux-x64 --self-contained
```

## License

[MIT License](LICENSE)

## Acknowledgments

- [Avalonia UI](https://avaloniaui.net/) - Cross-platform .NET UI framework
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) - Modern MVVM library
