using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;


//Extensions allow us to attach functions to models without directly adding them to the models themselves.
namespace Todo.Common.Extensions
{
    public static class TaskModelExtensions
    {
        public static string ToFileName(this TaskModel model) => $"{model.Key}.json";

        public static string ToFileName(string Key) => $"{Key}.json";
    }
}
