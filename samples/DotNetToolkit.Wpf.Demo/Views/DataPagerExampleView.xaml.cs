namespace DotNetToolkit.Wpf.Demo.Views
{
    using Data;
    using Models;

    /// <summary>
    /// Interaction logic for DataPagerExampleView.xaml
    /// </summary>
    public partial class DataPagerExampleView
    {
        public DataPagerExampleView()
        {
            InitializeComponent();

            DataContext = new PagedCollectionView(Customer.SampleCustomers)
            {
                PageSize = 6
            };
        }
    }
}
