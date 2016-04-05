namespace Labinator2016.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Lib.Headers;
    using Labinator2016.Lib.Models;
    class FakeDatabase : ILabinatorDb
    {
        public IQueryable<T> Query<T>() where T : class
        {
            return this.Sets[typeof(T)] as IQueryable<T>;
        }

        public void Dispose() { }

        public void AddSet<T>(IQueryable<T> objects)
        {
            this.Sets.Add(typeof(T), objects);
        }

        public void Add<T>(T entity) where T : class
        {
            if (typeof(T) == typeof(Log))
            {
                LogAdded.Add(entity);
            }
            else
            {
                this.Added.Add(entity);
                IQueryable<T> temp = this.Sets[typeof(T)] as IQueryable<T>;
                List<T> existing = temp.ToList();
                string IDName = entity.GetType().Name + "Id";
                int MaxId = 0;
                foreach(T item in existing)
                {
                    MaxId = Math.Max(MaxId, int.Parse(item.GetType().GetProperty(IDName).GetValue(item).ToString()));
                }
                entity.GetType().GetProperty(IDName).SetValue(entity,MaxId+1);
                existing.Add(entity);
                Sets[typeof(T)] = existing.AsQueryable();
            }
        }

        void ILabinatorDb.Delete<T1>(System.Linq.Expressions.Expression<Func<T1, bool>> Target)
        {

        }

        public void Update<T>(T entity) where T : class
        {
            if (typeof(T) == typeof(Log))
            {
                LogUpdated.Add(entity);
            }
            else
            {
                this.Updated.Add(entity);
            }
        }

        public void Remove<T>(T entity) where T : class
        {
            if (typeof(T) == typeof(Log))
            {
                LogRemoved.Add(entity);
            }
            else
            {
                this.Removed.Add(entity);
            }
        }

        public void SaveChanges()
        {
            this.saved++;
        }

        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();

        public List<object> Added = new List<object>();

        public List<object> LogAdded = new List<object>();

        public List<object> Updated = new List<object>();

        public List<object> LogUpdated = new List<object>();

        public List<object> Removed = new List<object>();

        public List<object> LogRemoved = new List<object>();

        public int saved = 0;
    }
}