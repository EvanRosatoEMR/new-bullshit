using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using System.Text.Json;
using Todo.Common.Extensions;

namespace Todo.Common.Services
{
    public interface IDataService<T, TKey>
    {
        Task SaveAsync(T obj);
        Task<T> GetAsync(TKey id);
    }

    public interface IFileDataService : IDataService<TaskModel?, string>
    {

    }

    //question mark to tell the code "yeah this COULD be null but that's sometimes just gonna happen so don't get on my ass about it."
    public class FileDataService : IDataService<TaskModel?, string>
    {

        private readonly string path;
        
        //TODO: configure ILogger
        public FileDataService(string path)
        {
            this.path = path;
        }

        public async Task<TaskModel?> GetAsync(string id)
        {
            try
            {
                //file we're looking for
                string fileName = TaskModelExtensions.ToFileName(id);

                //Concatenates the id of the file we're looking for with the path we're looking for.
                //So that way if the path has more than 1 file in it the whole thing doesn't fail.
                string combinePath = Path.Combine(this.path, fileName);

                //The big difference between pro code and newbie code: How much error checking you do!
                //TEST EARLY! Don't do work you're not *sure* that you can do.
                if (!File.Exists(combinePath))
                {
                    Console.WriteLine($"File Does Not Exist In {combinePath}!");
                    return null;
                }

                //when you us IO stuff you use "unmanaged classes"
                using StreamReader sr = new StreamReader(this.path);

                //Give us the text from whatever file we're... testing, I guess? I, I don't know what any of this does.
                string text = await sr.ReadToEndAsync();


                //if the string is not found (null) or not printable (just whiteness)
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine($"Empty File In {combinePath}!");
                    return null;
                }

                //turns text into class
                return JsonSerializer.Deserialize<TaskModel>(text);
            }
            catch (IOException ex)
            {
                //don't leave these empty!
                Console.WriteLine($"Finding File Failed For {id}! FUCK!!!");

                //"throw" basically throws the erorr handling to the caller.(?)
                //generally, don't just use "throw" by itself like this.
                throw;
            }
            catch (JsonException)
            {
                Console.WriteLine($"Deserializing File Failed!");
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine($"I don't think... this worked...");
                throw;
            }
        }

        public async Task SaveAsync(TaskModel? obj)
        {
            //just because we don't want the program to nag us about a possible null value,
            //doesn't mean we actually want that null value.
            if (obj is null)
            {
                return;
            }

            //TODO: Test if we're overwriting anything.
            try
            {
                //we type "varname.methodname()" here because it's an extension method.
                string fileName = obj.ToFileName();
                string combinedPath = Path.Combine(this.path, fileName);


                using StreamWriter sw = new StreamWriter(this.path);
                string text = JsonSerializer.Serialize(obj);
                await sw.WriteAsync(text);
            }
            catch (IOException)
            {
                Console.WriteLine($"Failed Writing File For Task {obj.Key}! SHIT!!!");
                throw;
            }
            catch (JsonException)
            {
                Console.WriteLine($"Serializing File Failed!");
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine($"I don't think... this worked...");
                throw;
            }
        }
    }
}
