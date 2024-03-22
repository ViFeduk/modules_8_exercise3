namespace modules_8_exercise3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string catalog = @"C:\newPapka";
            DirectoryInfo directory = new DirectoryInfo(catalog);
            long a = MemoryDirectory(directory);
            Console.WriteLine("Общий вес директории: " + (a / 1024 ) + " килобайт");
            DeleteAllFileInCatalog(catalog);
            a = MemoryDirectory(directory);
            Console.WriteLine("Общий вес директории: " + (a) + "байт");
            long MemoryDirectory(DirectoryInfo directoryInfo)
            {
                long totalSize = 0;

                if (directoryInfo.Exists)
                {
                    totalSize += MemoryFileInDerictory(directoryInfo);

                    DirectoryInfo[] directories = directoryInfo.GetDirectories();
                    foreach (DirectoryInfo d in directories)
                    {
                        totalSize += MemoryDirectory(d);
                    }

                }
                return totalSize;
            }
            long MemoryFileInDerictory(DirectoryInfo info)
            {
                long size = 0;
                FileInfo[] fileInfo = info.GetFiles();
                foreach (FileInfo file in fileInfo)
                {
                    size += file.Length;
                }
                return size;
            }
        }
        static void DeleteAllFileInCatalog(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                string[] subDirectories = Directory.GetDirectories(path);

                DateTime dateTime = DateTime.Now;
                TimeSpan timeSpan = TimeSpan.FromMinutes(30);


                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    TimeSpan accessTime = dateTime - fileInfo.LastAccessTime;
                    if (accessTime > timeSpan)
                    {
                        File.Delete(file);
                        Console.WriteLine($"Удалён файл {file}");
                    }
                }

                foreach (string subDirectory in subDirectories)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(subDirectory);
                    TimeSpan accessTimeCatalog = dateTime - directoryInfo.LastAccessTime;
                    if (accessTimeCatalog > timeSpan)
                    {
                        directoryInfo.Delete(true);
                        Console.WriteLine($"Удалён каталог {subDirectory}\n");
                    }

                }


            }
            else Console.WriteLine("Папка не существует");
        }

    }
}

