using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Lab2.Models;

namespace Lab4.Repositories
{
    class FileRepositoryOfPeople
    {
        private static readonly string BaseFolder = 
            Path.Combine
            (
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "C#Lab4Storage", 
            "People"
            );

        public FileRepositoryOfPeople()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public async Task AddOrUpdateAsync(Person per)
        {
            var stringObj = JsonSerializer.Serialize(per);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, per.Email), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task<Person> GetAsync(string login)
        {
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, login);

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<Person>(stringObj);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = await sw.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }

        public List<Person> GetAll()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = sw.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }

        public async Task Erase(Person p)
        {
            string dest = Path.Combine(BaseFolder, p.Email);
            Task t = Task.Run(() => File.Delete(dest));
            await t;
        }
    }
}