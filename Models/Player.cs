using System;

namespace Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
    }
}
