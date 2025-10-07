using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using Todo.Common.Requests;

namespace Todo.Common.Services
{
    //internal = public to DLL, private to everything else.
    //DLL means uhhhhhhhhhhhh
    public interface IInterfacebullshit
    {
        //Tasks and Asyncs are not exclusive to each other.
        //Here, we create a task using the CreateTaskRequest class we just made.
        Task<Result> CreateTaskAsync(CreateTaskRequest request);
    }

    public class TaskService : IInterfacebullshit
    {
        private readonly IFileDataService fileDataService;
        
        public TaskService(IFileDataService fileDataService)
        {
            this.fileDataService = fileDataService;
        }
        public async Task<Result> CreateTaskAsync(CreateTaskRequest request)
        {
            var modelResult = TaskModel.CreateTask(request);

            if (modelResult.IsErr())
            {
                return Result.Err(modelResult.GetErr());
            }

            await this.fileDataService.SaveAsync(modelResult.GetVal());
            return Result.Ok();
        }
    }
}
