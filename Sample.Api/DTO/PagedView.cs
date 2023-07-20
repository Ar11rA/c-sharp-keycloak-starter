using System;
namespace Sample.Api.DTO
{
    public class PagedView<T> where T : class
    {
        public List<T> objectList { get; set; }
    }
}

