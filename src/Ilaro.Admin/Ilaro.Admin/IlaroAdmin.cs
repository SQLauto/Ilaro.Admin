﻿using Ilaro.Admin.Configuration;
using Ilaro.Admin.Configuration.Customizers;
using Ilaro.Admin.Core;
using Ilaro.Admin.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Ilaro.Admin
{
    public class IlaroAdmin : IIlaroAdmin
    {
        private List<Entity> _entitiesTypes = new List<Entity>();
        public ReadOnlyCollection<Entity> Entities
        {
            get
            {
                return _entitiesTypes.AsReadOnly();
            }
        }

        public Entity ChangeEntity
        {
            get
            {
                return _entitiesTypes.FirstOrDefault(x => x.IsChangeEntity);
            }
        }
        public bool IsChangesEnabled
        {
            get { return ChangeEntity != null; }
        }

        public IAuthorizationFilter Authorize { get; private set; }
        public string ConnectionStringName { get; private set; }
        public string RoutesPrefix { get; private set; }

        public void RegisterEntity(Type entityType)
        {
            var customizerHolder = new CustomizersHolder(entityType);
            Admin.AddCustomizer(customizerHolder);
        }

        public EntityCustomizer<TEntity> RegisterEntity<TEntity>() where TEntity : class
        {
            var customizerHolder = new CustomizersHolder(typeof(TEntity));
            Admin.AddCustomizer(customizerHolder);
            return new EntityCustomizer<TEntity>(customizerHolder);
        }

        public void RegisterEntityWithAttributes(Type entityType)
        {
            var customizerHolder = new CustomizersHolder(entityType);
            Admin.AddCustomizer(customizerHolder);
            AttributesConfigurator.Initialise(customizerHolder);
        }

        public EntityCustomizer<TEntity> RegisterEntityWithAttributes<TEntity>() where TEntity : class
        {
            var customizerHolder = new CustomizersHolder(typeof(TEntity));
            Admin.AddCustomizer(customizerHolder);
            AttributesConfigurator.Initialise(customizerHolder);
            var customizer = new EntityCustomizer<TEntity>(customizerHolder);
            return customizer;
        }

        public Entity GetEntity(string entityName)
        {
            return _entitiesTypes.FirstOrDefault(x => x.Name == entityName);
        }

        public Entity GetEntity(Type type)
        {
            return GetEntity(type.Name);
        }

        public Entity GetEntity<TEntity>() where TEntity : class
        {
            return GetEntity(typeof(TEntity));
        }

        public void Initialise(
            string connectionStringName = "",
            string routesPrefix = "IlaroAdmin",
            IAuthorizationFilter authorize = null)
        {
            RoutesPrefix = routesPrefix;
            Authorize = authorize;
            RoutesPrefix = routesPrefix;
            ConnectionStringName = GetConnectionStringName(connectionStringName);

            foreach (var customizer in Admin.Customizers)
            {
                var entity = CreateInstance(customizer.Key);
                customizer.Value.CustomizeEntity(entity);
            }

            SetForeignKeysReferences();
            SetDisplayProperties();
        }

        private Entity CreateInstance(Type entityType)
        {
            var entity = new Entity(entityType);
            _entitiesTypes.Add(entity);
            return entity;
        }

        private static string GetConnectionStringName(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
            {
                if (ConfigurationManager.ConnectionStrings.Count > 1)
                {
                    connectionStringName = ConfigurationManager.ConnectionStrings[1].Name;
                }
                else
                {
                    throw new InvalidOperationException("Need a connection string name - can't determine what it is");
                }
            }

            return connectionStringName;
        }

        private void SetDisplayProperties()
        {
            foreach (var entity in _entitiesTypes.Where(x => x.DisplayProperties.Any() == false))
            {
                foreach (var property in entity.GetDefaultDisplayProperties())
                {
                    property.IsVisible = true;
                }
            }
            foreach (var entity in _entitiesTypes.Where(x => x.SearchProperties.Any() == false))
            {
                foreach (var property in entity.GetDefaultSearchProperties())
                {
                    property.IsSearchable = true;
                }
            }
        }

        private void SetForeignKeysReferences()
        {
            foreach (var entity in _entitiesTypes)
            {
                // Try determine which property is a entity key if is not set
                if (entity.Key.IsNullOrEmpty())
                {
                    var entityKey = entity.Properties.FirstOrDefault(x => x.Name.ToLower() == "id");
                    if (entityKey == null)
                    {
                        entityKey = entity.Properties.FirstOrDefault(x => x.Name.ToLower() == entity.Name.ToLower() + "id");
                    }

                    if (entityKey != null)
                    {
                        entityKey.IsKey = true;
                    }
                }
            }

            foreach (var entity in _entitiesTypes)
            {
                foreach (var property in entity.Properties)
                {
                    if (property.IsForeignKey)
                    {
                        property.ForeignEntity = _entitiesTypes.FirstOrDefault(x => x.Name == property.ForeignEntityName);

                        if (!property.ReferencePropertyName.IsNullOrEmpty())
                        {
                            property.ReferenceProperty = entity.Properties.FirstOrDefault(x => x.Name == property.ReferencePropertyName);
                            if (property.ReferenceProperty != null)
                            {
                                property.ReferenceProperty.IsForeignKey = true;
                                property.ReferenceProperty.ForeignEntity = property.ForeignEntity;
                                property.ReferenceProperty.ReferenceProperty = property;
                            }
                            else if (!property.TypeInfo.IsSystemType)
                            {
                                if (property.ForeignEntity != null)
                                {
                                    property.TypeInfo.Type = property.ForeignEntity.Key.FirstOrDefault().TypeInfo.Type;
                                }
                                else
                                {
                                    // by default foreign property is int
                                    property.TypeInfo.Type = typeof(int);
                                }
                            }
                        }
                    }
                }

                foreach (var property in entity.Properties)
                {
                    property.Template = new PropertyTemplate(
                        property.TypeInfo,
                        property.IsForeignKey);
                }
            }
        }
    }
}