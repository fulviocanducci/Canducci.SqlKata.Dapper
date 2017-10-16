using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public bool Active { get; set; } = true;
    }
}
