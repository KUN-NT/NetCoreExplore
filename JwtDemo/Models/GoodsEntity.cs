using System;

namespace JwtDemo.Models
{
    public class GoodsEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsDelete { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public int Price { get; set; }
    }
}
