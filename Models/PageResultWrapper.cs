using System;
using Newtonsoft.Json;

namespace getting_started_with_apollo_csharp.Models
{
    public class PagedResultWrapper<T>
    {
        public PagedResultWrapper()
        {
        }

        public PagedResultWrapper(int pageSize, byte[] pageState, T data)
        {
            PageSize = pageSize;
            PageState = pageState;
            Data = data;
        }

        public int PageSize { get; set; }
        public byte[] PageState { get; set; }
        public T Data { get; set; }
    }

}