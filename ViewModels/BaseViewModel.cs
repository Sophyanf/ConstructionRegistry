using ConstructionRegistry.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Application = System.Windows.Application;

namespace ConstructionRegistry.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // UI-размеры (оставляем, если реально нужны в XAML)
        public int ScreenHeight { get; } = (int)SystemParameters.MaximizedPrimaryScreenHeight;
        public int ScreenWidth { get; } = (int)SystemParameters.PrimaryScreenWidth;
        public int MainStackPanel { get; } = (int)SystemParameters.MaximizedPrimaryScreenHeight - 160;

        #region Commands (базовые)
        public ActionCommand CloseAppCommand { get; }
        public ActionCommand CloseWindowCommand { get; }
        public ActionCommand WindowMinimizeCommand { get; }
        public ActionCommand WindowMaximizeCommand { get; }

        public BaseViewModel()
        {
            CloseAppCommand = new ActionCommand(_ => Application.Current.Shutdown());
            CloseWindowCommand = new ActionCommand(_ => CloseCurrentWindow());
            WindowMinimizeCommand = new ActionCommand(_ => MinimizeWindow());
            WindowMaximizeCommand = new ActionCommand(_ => MaximizeWindow());
        }

        private void CloseCurrentWindow()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            activeWindow?.Close();
        }

        private void MinimizeWindow()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (activeWindow != null)
                activeWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow()
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (activeWindow == null) return;

            activeWindow.WindowState = activeWindow.WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected void UpdateValue<T>(ref T field, T value, [CallerMemberName] string? property = null)
        {
            field = value;
            OnPropertyChanged(property);
        }
        #endregion
    }
}
