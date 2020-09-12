using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoranaRegistration.Models
{
    public class Pagination
    {
        public int Items { get; }

        public int PageSize { get; }

        public int Positives { get; }
        public Pagination(int itemSum, int pageSize, int positive)
        {
            Items = itemSum;
            PageSize = pageSize;
            Positives = positive; 
        }
       
        public int Pages { get {
                int pages = (int)Items / PageSize;
                return (Items % PageSize) > 0 ? pages + 1 : pages; 
            } }
    }
}
