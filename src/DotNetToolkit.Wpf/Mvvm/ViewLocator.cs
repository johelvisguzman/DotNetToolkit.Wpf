namespace DotNetToolkit.Wpf.Mvvm
{
    using Extensions;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    /// <summary>
    /// A simple locator for retrieving a view for a specified view model.
    /// </summary>
    public static class ViewLocator
    {
        private const string DefaultViewSuffix = "View";
        private const string DefaultViewModelSuffix = "ViewModel";
        private const string DefaultContentPropertyName = "Content";

        /// <summary>
        /// An attached property for attaching a model to the UI.
        /// </summary>
        public static readonly DependencyProperty LocateForProperty = DependencyProperty.RegisterAttached(
            "LocateFor",
            typeof(object),
            typeof(ViewLocator),
            new PropertyMetadata(null, OnLocateForChanged));

        /// <summary>
        /// Gets the value of <see cref="LocateForProperty" />.
        /// </summary>
        /// <param name="element">The element.</param>
        public static object GetLocateFor(DependencyObject element)
        {
            return (object)element.GetValue(LocateForProperty);
        }

        /// <summary>
        /// Sets the value of <see cref="LocateForProperty" />.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetLocateFor(DependencyObject element, object value)
        {
            element.SetValue(LocateForProperty, value);
        }

        /// <summary>
        /// Locates a view for the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The view.</returns>
        public static FrameworkElement LocateFor(object viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            var viewModelType = viewModel.GetType();
            var assembly = viewModelType.Assembly;
            var viewName = viewModelType.Name.ReplaceEnd(DefaultViewModelSuffix, DefaultViewSuffix);
            var viewType = assembly.GetTypes().FirstOrDefault(x => x.Name.Equals(viewName));

            FrameworkElement element;

            if (viewType == null)
            {
                element = new TextBlock { Text = $"Cannot locate view for '{viewModelType.FullName}'." };
            }
            else
            {
                element = (FrameworkElement)Activator.CreateInstance(viewType);
                element.DataContext = viewModel;
                element.Loaded += OnElementLoaded;
            }

            return element;
        }

        /// <summary>
        /// Locates a view for the specified view model.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        /// <returns>The view.</returns>
        public static FrameworkElement LocateFor(Type viewModelType)
        {
            return LocateFor(Activator.CreateInstance(viewModelType));
        }

        /// <summary>
        /// Locates a view for the specified view model.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <returns>The view.</returns>
        public static FrameworkElement LocateFor<TViewModel>()
        {
            return LocateFor(typeof(TViewModel));
        }

        /// <summary>
        /// The event handler to handle the loaded event of a framework element.
        /// </summary>
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

        /// <summary>
        /// Called when [locate for changed].
        /// </summary>
        /// <param name="d">The dependency property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnLocateForChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
                return;

            if (e.NewValue != null)
            {
                var view = LocateFor(e.NewValue);

                if (!SetContentProperty(d, view))
                {
                    view = LocateFor(e.NewValue.GetType());

                    SetContentProperty(d, view);
                }
            }
            else
            {
                SetContentProperty(d, e.NewValue);
            }
        }


        /// <summary>
        /// Sets the content property.
        /// </summary>
        /// <param name="d">The dependency property.</param>
        /// <param name="view">The view.</param>
        // https://github.com/Caliburn-Micro/Caliburn.Micro/blob/master/src/Caliburn.Micro.Platform/View.cs
        private static bool SetContentProperty(DependencyObject d, object view)
        {
            var element = view as FrameworkElement;
            if (element != null && element.Parent != null)
            {
                SetContentPropertyCore(element.Parent, null);
            }

            return SetContentPropertyCore(d, view);
        }

        /// <summary>
        /// Sets the content property.
        /// </summary>
        /// <param name="d">The dependency property.</param>
        /// <param name="view">The view.</param>
        // https://github.com/Caliburn-Micro/Caliburn.Micro/blob/master/src/Caliburn.Micro.Platform/View.cs
        private static bool SetContentPropertyCore(DependencyObject d, object view)
        {
            try
            {
                var type = d.GetType();
                var contentPropertyName = GetContentPropertyName(type);

                type.GetRuntimeProperty(contentPropertyName)
                    .SetValue(d, view, null);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the name of the content property.
        /// </summary>
        /// <param name="type">The type.</param>
        // https://github.com/Caliburn-Micro/Caliburn.Micro/blob/master/src/Caliburn.Micro.Platform/View.cs
        private static string GetContentPropertyName(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var contentProperty = typeInfo.GetCustomAttribute<ContentPropertyAttribute>();

            return contentProperty?.Name ?? DefaultContentPropertyName;
        }
    }
}
