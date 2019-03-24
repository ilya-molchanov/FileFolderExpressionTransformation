using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CodeFixture;

namespace TestApp
{
    public class FilterBase<TEntity>
    {
        protected IQueryable<TEntity> Result { get; set; }

        protected void FilterContains(Expression<Func<TEntity, string>> by, string text)
        {
            if (text != null)
            {
                Expression<Func<TEntity, bool>> filter = entity => by.Call()(entity).Contains(text);
                Result = Result.Where(filter.SubstituteMarker());
            }
        }

        protected void FilterStartsWith(Expression<Func<TEntity, string>> by, string text)
        {
            if (text != null)
            {
                Expression<Func<TEntity, bool>> filter = entity => by.Call()(entity).StartsWith(text);
                Result = Result.Where(filter.SubstituteMarker());
            }
        }

        protected void FilterSizeMoreThan(Expression<Func<TEntity, long>> by, long size)
        {
            Expression<Func<TEntity, bool>> filter = entity => by.Call()(entity) > size;
            Result = Result.Where(filter.SubstituteMarker());
        }

        protected void FilterSizeLessThan(Expression<Func<TEntity, long>> by, long size)
        {
            Expression<Func<TEntity, bool>> filter = entity => by.Call()(entity) < size;
            Result = Result.Where(filter.SubstituteMarker());
        }

        public static Expression<Func<FileSystemEntity, bool>> FilterNameAndSize(string name, long length)
        {
            return x => Path.GetFileName(x.Name).Contains(name) && x.Length > length;
        }
    }
}