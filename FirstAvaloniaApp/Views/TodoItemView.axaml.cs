using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FirstAvaloniaApp.ViewModels;
using System;

namespace FirstAvaloniaApp.Views
{
    public partial class TodoItemView : UserControl
    {
        // Event that will be raised when delete is requested
        public static readonly RoutedEvent<RoutedEventArgs> DeleteRequestedEvent =
            RoutedEvent.Register<TodoItemView, RoutedEventArgs>(
                nameof(DeleteRequested),
                RoutingStrategies.Bubble);

        // CLR wrapper for the event
        public event EventHandler<RoutedEventArgs> DeleteRequested
        {
            add => AddHandler(DeleteRequestedEvent, value);
            remove => RemoveHandler(DeleteRequestedEvent, value);
        }
        
        private Button? _deleteButton;
        private TextBlock? _titleText;

        public TodoItemView()
        {
            InitializeComponent();
            
            _deleteButton = this.FindControl<Button>("DeleteButton");
            _titleText = this.FindControl<TextBlock>("TitleText");

            if (_deleteButton != null)
            {
                _deleteButton.Click += DeleteButton_Click;
            }

            this.DataContextChanged += TodoItemView_DataContextChanged;
            this.PointerPressed += TodoItemView_PointerPressed;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void TodoItemView_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (DataContext is TodoItemViewModel viewModel)
            {
                viewModel.IsCompleted = !viewModel.IsCompleted;
                UpdateTitleStyling(viewModel.IsCompleted);
            }
        }

        private void TodoItemView_DataContextChanged(object? sender, EventArgs e)
        {
            if (DataContext is TodoItemViewModel viewModel)
            {
                UpdateTitleStyling(viewModel.IsCompleted);
            }
        }

        private void UpdateTitleStyling(bool isCompleted)
        {
            if (_titleText == null) return;
            
            if (isCompleted)
            {
                _titleText.TextDecorations = TextDecorations.Strikethrough;
                _titleText.Opacity = 0.5;
            }
            else
            {
                _titleText.TextDecorations = null;
                _titleText.Opacity = 1.0;
            }
        }
        
        private void DeleteButton_Click(object? sender, RoutedEventArgs e)
        {
            if (DataContext is TodoItemViewModel viewModel)
            {
                viewModel.Delete();
            }
        }
    }
} 