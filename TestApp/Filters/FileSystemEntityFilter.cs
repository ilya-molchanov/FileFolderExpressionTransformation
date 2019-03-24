using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace TestApp
{
    public class FileSystemEntityFilter:FilterBase<FileSystemEntity>
    {
        public IQueryable<FileSystemEntity> FilterContains(IQueryable<FileSystemEntity> all, FileSystemEntityFilterOptions by)
        {
            Result = all;
            FilterContains(fileSystemEntityFilterOptions => fileSystemEntityFilterOptions.Name, by.Name);
            return Result;
        }

        public IQueryable<FileSystemEntity> FilterStartsWith(IQueryable<FileSystemEntity> all, FileSystemEntityFilterOptions by)
        {
            Result = all;
            FilterStartsWith(fileSystemEntityFilterOptions => Path.GetFileName(fileSystemEntityFilterOptions.Name), by.Name);
            return Result;
        }

        public IQueryable<FileSystemEntity> FilterSizeMoreThan(IQueryable<FileSystemEntity> all, FileSystemEntityFilterOptions by)
        {
            Result = all;
            FilterSizeMoreThan(fileSystemEntityFilterOptions => fileSystemEntityFilterOptions.Length, by.Length);
            return Result;
        }

        public IQueryable<FileSystemEntity> FilterSizeLessThan(IQueryable<FileSystemEntity> all, FileSystemEntityFilterOptions by)
        {
            Result = all;
            FilterSizeLessThan(fileSystemEntityFilterOptions => fileSystemEntityFilterOptions.Length, by.Length);
            return Result;
        }

        public IEnumerable<FileSystemEntity> GetCartPromoTotal(IQueryable<FileSystemEntity> all, FileSystemEntityFilterOptions by)
        {
            IQueryable<FileSystemEntity> products = all.Where(FilterNameAndSize(by.Name, by.Length));
            return products;
        }
    }
}