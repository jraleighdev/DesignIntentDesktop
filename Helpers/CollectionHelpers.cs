using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignIntentDesktop.Helpers
{
    public static class CollectionHelpers
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> source)
        {
            foreach (var s in source)
            {
                collection.Add(s);
            }
        }
    }
}
