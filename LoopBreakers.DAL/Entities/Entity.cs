using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBreakers.DAL.Entities
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
