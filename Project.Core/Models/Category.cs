using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Project.Core.Models
{
    public class Category
    {
        public Category()
        {
            Products = new Collection<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        //Relational properties
        public ICollection<Product> Products { get; set; }
    }
}
