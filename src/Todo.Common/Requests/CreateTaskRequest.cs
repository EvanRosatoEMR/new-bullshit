using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Requests
{
    //This is the data needed to make the task, not the task itself.
    //No Behavior, Just Data!
    public class CreateTaskRequest
    {

        public CreateTaskRequest(string name, string description, DateTime duedate) 
        {
            this.Name = name;
            this.Description = description;
            this.DueDate = duedate;
        }
        public string Name { get; }
        public string Description { get; }
        public DateTime DueDate { get; }
    }
}
