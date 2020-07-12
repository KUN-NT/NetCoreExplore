using System;

namespace JwtDemo.Models
{
    public class GoodsDto
    {
        public string GoodsName { get; set; }
        public string Content { get; set; }
        public bool IsDelete { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public int Price { get; set; }
    }
}
