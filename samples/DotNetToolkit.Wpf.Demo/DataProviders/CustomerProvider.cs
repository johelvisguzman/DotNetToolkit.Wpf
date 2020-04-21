namespace DotNetToolkit.Wpf.Demo.DataProviders
{
    using ComponentModel.DataVirtualization;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading;

    public class CustomerProvider : IItemsProvider<Customer>
    {
        private readonly IQueryable<Customer> _customers;
        private readonly string _sortString;

        public CustomerProvider(string sortString)
        {
            _customers = Customer.SampleCustomers.AsQueryable();
            _sortString = sortString;
        }

        public int FetchCount()
        {
            Thread.Sleep(100);

            return _customers.Count();
        }

        public IList<Customer> FetchRange(int startIndex, int pageCount, out int overallCount)
        {
            Thread.Sleep(100);

            var query = _customers;

            overallCount = query.Count();

            var sorts = _sortString.Split(',');

            if (sorts.Count() > 0)
            {
                var orderableQuery = query.OrderBy(sorts[0]);

                for (int i = 1; i < sorts.Length; i++)
                {
                    orderableQuery = orderableQuery.ThenBy(sorts[1]);
                }

                query = orderableQuery;
            }


            return query.Skip((startIndex - 1) * pageCount).Take(pageCount).ToList();
        }
    }
}
