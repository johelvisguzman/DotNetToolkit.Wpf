namespace DotNetToolkit.Wpf.Mvvm
{
    using Common;
    using System;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// A simple locator for retrieving a view model for a specified view.
    /// </summary>
    public static class ViewModelLocator
    {
        private const string DefaultViewSuffix = "View";
        private const string DefaultViewModelSuffix = "ViewModel";

        /// <summary>
        /// An attached property that wires the corresponding view model to a <see cref="FrameworkElement" />.
        /// </summary>
        public static readonly DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            new PropertyMetadata(false, OnAutoWireViewModelChanged));

        /// <summary>
        /// Gets the value of <see cref="AutoWireViewModelProperty" />.
        /// </summary>
        /// <param name="element">The element.</param>
        public static bool GetAutoWireViewModel(FrameworkElement element)
        {
            return (bool)element.GetValue(AutoWireViewModelProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="AutoWireViewModelProperty" />.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetAutoWireViewModel(FrameworkElement element, bool value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        /// <summary>
        /// Locates a view model for the specified view type.
        /// </summary>
        /// <param name="viewType">The type of the view.</param>
        /// <returns>The view model.</returns>
        public static object LocateFor(Type viewType)
        {
            var assembly = viewType.Assembly;
            var viewModelName = viewType.Name.ReplaceEnd(DefaultViewSuffix, DefaultViewModelSuffix);
            var viewModelType = assembly.GetTypes().FirstOrDefault(x => x.Name.Equals(viewModelName));

            return viewModelType != null ? Activator.CreateInstance(viewModelType) : null;
        }

        /// <summary>
        /// Locates a view model for the specified view type.
        /// </summary>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <returns>The view model.</returns>
        public static object LocateFor<TView>()
        {
            return LocateFor(typeof(TView));
        }

        /// <summary>
        /// Called when [automatic wire view model changed].
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            var enable = e.NewValue as bool?;
            if (element != null)
            {
                if (enable.GetValueOrDefault())
                {
                    if (element.DataContext == null)
                        element.DataContext = LocateFor(element.GetType());

                    if (element.IsLoaded)
                    {
                        OnElementLoaded(element, new RoutedEventArgs());
                    }
                    else
                    {
                        element.Loaded += OnElementLoaded;
                    }
                }
                else
                {
                    element.Loaded -= OnElementLoaded;
                }
            }
        }

        /// <summary>
        /// Called when [element loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private static void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.Loaded -= OnElementLoaded;
            
            var viewModelBase = element.DataContext as IViewModel;
            if (viewModelBase != null && !viewModelBase.IsInitialized)
            {
                viewModelBase.Initialize();
            }
        }
    }
}
