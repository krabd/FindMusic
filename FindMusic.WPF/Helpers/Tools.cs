using System;
using System.Windows;
using System.Windows.Threading;

namespace FindMusic.WPF.Helpers
{
    public static class Tools
    {
        public static void DispatchedInvoke(Action action, bool useUiThread = true, Dispatcher dispatcher = null)
        {
            if (action == null) 
                throw new ArgumentNullException(nameof(action));

            if (dispatcher == null && Application.Current?.Dispatcher == null) return;

            dispatcher = dispatcher ?? Application.Current.Dispatcher;

            if (!useUiThread || dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                try
                {
                    if (!dispatcher.HasShutdownStarted)
                    {
                        dispatcher.Invoke(action);
                    }
                }
                catch (OperationCanceledException)
                {
                    // порождается при закрытии приложения
                }
            }
        }
    }
}
