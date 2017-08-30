using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApi.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }
    }
}