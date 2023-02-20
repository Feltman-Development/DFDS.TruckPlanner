using System.ComponentModel;

namespace DFDS.TP.Core.Utility;

public static class DelegateUtility
{
    /// <summary>
    /// Extension to raise PropertyChangedEvent, to thin out the viewmodel base classes.
    /// </summary>
    public static void Raise(this PropertyChangedEventHandler eventHandler, object source, string propertyName) 
        => eventHandler?.Invoke(source, new PropertyChangedEventArgs(propertyName));

    public static void Raise(this EventHandler eventHandler, object source) 
        => eventHandler?.Invoke(source, EventArgs.Empty);

    public static void Register(this INotifyPropertyChanged model, string propertyName, Action whenChanged) 
        => model.PropertyChanged += (_, args) => { if (args.PropertyName != propertyName) return; whenChanged(); };
}
