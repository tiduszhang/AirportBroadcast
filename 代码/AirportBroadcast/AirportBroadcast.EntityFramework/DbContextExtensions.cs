using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using Abp.Domain.Entities;
using System.Linq;

namespace AirportBroadcast
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// 获取实体的表名
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="context">Context</param>
        /// <returns>Table name</returns>
        public static string GetTableName<T, PK>(this DbContext context) where T : IEntity<PK>
        {
            //var tableName = typeof(T).Name;
            //return tableName;

            //this code works only with Entity Framework.
            //If you want to support other database, then use the code above (commented)

            var adapter = ((IObjectContextAdapter)context).ObjectContext;
            var storageModel = (StoreItemCollection)adapter.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
            var containers = storageModel.GetItems<EntityContainer>();
            var entitySetBase = containers.SelectMany(c => c.BaseEntitySets.Where(bes => bes.Name == typeof(T).Name)).First();

            // Here are variables that will hold table and schema name
            string tableName = entitySetBase.MetadataProperties.First(p => p.Name == "Table").Value.ToString();
            //string schemaName = productEntitySetBase.MetadataProperties.First(p => p.Name == "Schema").Value.ToString();
            return tableName;
        }


        /// <summary>
        /// 获取实体的表名
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="context">Context</param>
        /// <returns>Table name</returns>
        public static string GetTableName<T>(this DbContext context) where T : IEntity
        {
            return context.GetTableName<T, int>();
        }
    }
}