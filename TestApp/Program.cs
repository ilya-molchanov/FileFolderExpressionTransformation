using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CodeFixture;

namespace TestApp
{
    class Program
    {
        static Dictionary<string, long> _dictionary = new Dictionary<string, long>();

        static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        static void GetInnerEntities(string folderName)
        {
            //DirectoryInfo directoryInfo = new DirectoryInfo(@"F:\Projects\mentoring\New folder\LinqKitSource");
            //Console.WriteLine("Your directory : {0}", directoryInfo.FullName);
            //Console.WriteLine();
            //Console.WriteLine(myDir.FullName);
            //ShowDirectories(myDir, Probel);
            string[] entries = Directory.GetFileSystemEntries(folderName, "*", SearchOption.AllDirectories);
            foreach (string str in entries)
            {
                FileAttributes attr = File.GetAttributes(str);

                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    _dictionary.Add(str, DirSize(new DirectoryInfo(str)));
                }
                else
                {
                    _dictionary.Add(str, new FileInfo(str).Length);
                }
            }
        }

        static void Main(string[] args)
        {
            GetInnerEntities(@"F:\Projects\mentoring\New folder\LinqKitSource");
            
            var fileSystemEntities = _dictionary.Select(x => new FileSystemEntity(x.Key, x.Value)).AsQueryable();
            var filter = new FileSystemEntityFilter();

            var fileSystemEntitiesContainsName = filter.FilterContains(fileSystemEntities, new FileSystemEntityFilterOptions { Name = "Expression" }).ToArray();
            foreach (var entry in fileSystemEntitiesContainsName)
            {
                Console.WriteLine(entry.Name + " " + entry.Length);
            }
            Console.WriteLine("--------------");

            var fileSystemEntitiesStartsNameWith = filter.FilterStartsWith(fileSystemEntities, new FileSystemEntityFilterOptions { Name = "Li" }).ToArray();
            foreach (var entry in fileSystemEntitiesStartsNameWith)
            {
                Console.WriteLine(entry.Name + " " + entry.Length);
            }
            Console.WriteLine("--------------");

            var fileSystemEntitiesSizeMoreThan = filter.FilterSizeMoreThan(fileSystemEntities, new FileSystemEntityFilterOptions { Length = 1000 }).ToArray();
            foreach (var entry in fileSystemEntitiesSizeMoreThan)
            {
                Console.WriteLine(entry.Name + " " + entry.Length);
            }
            Console.WriteLine("--------------");

            var fileSystemEntitiesSizeLessThan = filter.FilterSizeLessThan(fileSystemEntities, new FileSystemEntityFilterOptions { Length = 1000 }).ToArray();
            foreach (var entry in fileSystemEntitiesSizeLessThan)
            {
                Console.WriteLine(entry.Name + " " + entry.Length);
            }
            Console.WriteLine("--------------");

            var fileSystemEntitiesSizeMoreThanAndName = filter.GetCartPromoTotal(fileSystemEntities, new FileSystemEntityFilterOptions { Length = 1000, Name = "DemoData" }).ToArray();
            foreach (var entry in fileSystemEntitiesSizeMoreThanAndName)
            {
                Console.WriteLine(entry.Name + " " + entry.Length);
            }

            Console.WriteLine("--------------");

            Console.ReadLine();
        }
    }
}
