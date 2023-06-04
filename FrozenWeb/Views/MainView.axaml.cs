using Avalonia.Controls;
using Avalonia.Input;
using FrozenWeb.ViewModels;

namespace FrozenWeb.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void InputBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        if (DataContext is MainViewModel vm) vm.OnMessageSend.Execute(null);
    }
}