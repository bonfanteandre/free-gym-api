using System.Collections;
using System.Collections.Generic;

namespace FreeGym.Core.Results
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public long Total { get; set; }

        public PagedResult(IEnumerable<T> data, long total)
        {
            Data = data;
            Total = total;
        }
    }
}
