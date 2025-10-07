using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Requests;

namespace Todo.Common.Models
{
    public class TaskModel
    {

        private TaskModel()
        {
            //required
            this.Key = string.Empty;

            //required
            this.Name = string.Empty;

            //optional
            this.Description = string.Empty;

            //Jan. 1st, 1900
            this.DueDate = DateTime.MinValue;
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public static TaskModel CreateTask(CreateTaskRequest request)
        {
            if (!request.invalid())
            {
                throw new InvalidOperationException();
            }

            return new TaskModel
            {
                //Guid = Global Unique ID.
                //Generates a unique id that is confirmed to be unique on a global scale.
                Key = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                DueDate = request.DueDate
            };
        }
    }
}
