using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace InstagramDataReader.Interfaces
{
    public abstract class BaseInstagramDataElement : IIdentificable, ILoadeable, INode
    {
        public virtual Guid Id { get; } = Guid.NewGuid();

        public abstract string Name { get; }

        public virtual IEnumerable<INode> Childs => GetChilds();

        internal abstract void Load(JObject jObject);

        void ILoadeable.Load(JObject jObject)
        {
            Load(jObject);
        }

        protected virtual IEnumerable<INode> GetChilds()
        {
            return Enumerable.Empty<INode>();
        }
    }

    public abstract class InstagramDataElement<TEntity> : BaseInstagramDataElement
    {
        protected abstract TEntity CreateEntity(JToken jToken);

        protected virtual IEnumerable<TEntity> ReadEntities(JObject jObject, string node)
        {
            return ReadEntities(jObject, node, CreateEntity);
        }

        protected virtual IEnumerable<TEntity> ReadEntities(JObject jObject, string node, Func<JToken, TEntity> createEntity)
        {
            return ReadEntities<TEntity>(jObject, node, createEntity);
        }

        protected virtual IEnumerable<TCustomEntity> ReadEntities<TCustomEntity>(JObject jObject, string node, Func<JToken, TCustomEntity> createEntity)
        {
            return jObject[node].Select(createEntity);
        }

        protected virtual TCollection CreateCollection<TCollection>(JObject jObject, TCollection collection, string node)
            where TCollection : BaseInstagramCollectionElements<TEntity>
        {
            collection.Load(jObject);
            var entities = ReadEntities(jObject, node);
            collection.AddRange(entities);
            return collection;
        }

        protected virtual TCollection CreateCollection<TCollection, TCustomEntity>(JObject jObject, TCollection collection, string node, Func<JToken, TCustomEntity> createEntity)
           where TCollection : BaseInstagramCollectionElements<TCustomEntity>
        {
            collection.Load(jObject);
            var entities = ReadEntities(jObject, node, createEntity);
            collection.AddRange(entities);
            return collection;
        }
    }

    public abstract class BaseInstagramCollectionElements<TEntity> : BaseInstagramDataElement, IList<TEntity>, IInstagramCollectionElements
    {
        protected abstract IList<TEntity> Collection { get; }

        public int Count => Collection.Count;

        public bool IsReadOnly => Collection.IsReadOnly;

        public TEntity this[int index]
        {
            get => Collection[index];
            set => Collection[index] = value;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TEntity item)
        {
            Collection.Add(item);
        }

        public void Clear()
        {
            Collection.Clear();
        }

        public bool Contains(TEntity item)
        {
            return Collection.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            Collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(TEntity item)
        {
            return Collection.Remove(item);
        }

        public int IndexOf(TEntity item)
        {
            return Collection.IndexOf(item);
        }

        public void Insert(int index, TEntity item)
        {
            Collection.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Collection.RemoveAt(index);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Collection.Add(entity);
        }

        internal override void Load(JObject jObject) { }

        internal abstract void Load(JArray array);

        void IInstagramCollectionElements.Load(JArray array)
        {
            Load(array);
        }
    }
}
