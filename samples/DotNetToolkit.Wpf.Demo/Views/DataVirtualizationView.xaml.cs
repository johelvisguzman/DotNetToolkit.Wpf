namespace DotNetToolkit.Wpf.Demo.Views
{
	using Collections.DataVirtualization;
	using DataProviders;
	using Models;
    using System;
    using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
    using System.Text;
    using System.Windows.Controls;
	using System.Windows.Data;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for DataVirtualizationView.xaml
    /// </summary>
    public partial class DataVirtualizationView
    {
        private CustomerProvider _customerProvider;
        private int _pageSize = 100;
        private int _timePageInMemory = 5000;
		private List<CustomSortDescription> _sortDescriptions;

		public DataVirtualizationView()
        {
            InitializeComponent();

			string defaultSortColumnName = "Name";
			DataGridColumn defaultSortColumn = this.CustomersDataGrid.Columns.Single(dgc => this.GetColumnSortMemberPath(dgc) == defaultSortColumnName);
			_sortDescriptions = new List<CustomSortDescription>
			{
				new CustomSortDescription
				{
					PropertyName = defaultSortColumnName,
					Direction = ListSortDirection.Descending,
					Column = defaultSortColumn
				}
			};

			RefreshData();
        }

		private string GetColumnSortMemberPath(DataGridColumn column)
		{
			string prefixToRemove = "Data.";
			string fullSortColumn = DataGridHelper.GetSortMemberPath(column);
			string sortColumn = fullSortColumn.Substring(prefixToRemove.Length);
			return sortColumn;
		}

		private void Customers_Sorting(object sender, DataGridSortingEventArgs e)
        {
            this.ApplySortColumn(e.Column);
            e.Handled = true;
        }

		private void ApplySortColumn(DataGridColumn column)
		{
			// If column was not sorted, we sort it ascending. If it was already sorted, we flip the sort direction.
			string sortColumn = this.GetColumnSortMemberPath(column);
			CustomSortDescription existingSortDescription = _sortDescriptions.SingleOrDefault(sd => sd.PropertyName == sortColumn);
			if (existingSortDescription == null)
			{
				existingSortDescription = new CustomSortDescription
				{
					PropertyName = sortColumn,
					Direction = ListSortDirection.Ascending,
					Column = column
				};
				_sortDescriptions.Add(existingSortDescription);
			}
			else
			{
				existingSortDescription.Direction = (existingSortDescription.Direction == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
			}

			// If user is not pressing Shift, we remove all SortDescriptions except the current one.
			bool isShiftPressed = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;
			if (!isShiftPressed)
			{
				for (int i = _sortDescriptions.Count - 1; i >= 0; i--)
				{
					CustomSortDescription csd = _sortDescriptions[i];
					if (csd.PropertyName != sortColumn)
					{
						_sortDescriptions.RemoveAt(i);
					}
				}
			}

			this.RefreshData();
		}

		private void UpdateSortingVisualFeedback()
		{
			foreach (CustomSortDescription csd in this._sortDescriptions)
			{
				csd.Column.SortDirection = csd.Direction;
			}
		}

		private string GetCurrentSortString()
		{
			// The result string is created, taking into account all sorted columns in the order they were sorted.
			var result = new StringBuilder();
			string separator = String.Empty;
			foreach (CustomSortDescription sd in _sortDescriptions)
			{
				result.Append(separator);
				result.Append(sd.PropertyName);
				if (sd.Direction == ListSortDirection.Descending)
				{
					result = result.Append(" DESC");
				}
				separator = ", ";
			}

			return result.ToString();
		}

		private void RefreshData()
        {
			var sortString = this.GetCurrentSortString();
			_customerProvider = new CustomerProvider(sortString);
            var customerList = new AsyncVirtualizingCollection<Customer>(_customerProvider, _pageSize, _timePageInMemory);
            this.DataContext = customerList;

			this.UpdateSortingVisualFeedback();
		}
    }

	public class CustomSortDescription
	{
		public string PropertyName { get; set; }
		public ListSortDirection Direction { get; set; }
		public DataGridColumn Column { get; set; }
	}

	public static class DataGridHelper
	{
		public static string GetSortMemberPath(DataGridColumn column)
		{
			string sortPropertyName = column.SortMemberPath;
			if (string.IsNullOrEmpty(sortPropertyName))
			{
				DataGridBoundColumn boundColumn = column as DataGridBoundColumn;
				if (boundColumn != null)
				{
					Binding binding = boundColumn.Binding as Binding;
					if (binding != null)
					{
						if (!string.IsNullOrEmpty(binding.XPath))
						{
							sortPropertyName = binding.XPath;
						}
						else if (binding.Path != null)
						{
							sortPropertyName = binding.Path.Path;
						}
					}
				}
			}

			return sortPropertyName;
		}
	}
}
