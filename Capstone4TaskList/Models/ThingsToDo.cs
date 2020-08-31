using System;
using System.Collections.Generic;

namespace Capstone4TaskList.Models
{
    public partial class ThingsToDo
    {
        public int Id { get; set; }
        public string ThingDescription { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Completion { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
