using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace efcore2_webapi.Infrastructure.EntityFramework
{
    public abstract class EntityMap<TEntity>
            where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }

    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityMap<TEntity> entityMap)
            where TEntity : class
        {            
            entityMap.Map(modelBuilder.Entity<TEntity>());
        }

        public static void AddEntitiesMapsFromAssemblyOf<T>(this ModelBuilder modelBuilder, Type baseType)
            where T : class
        {
            var assembly = typeof(T).GetTypeInfo().Assembly;

            //Get all subclasses of the given baseType
            var types = assembly.GetTypes().Where(t => t.GetTypeInfo().IsSubclassOf(baseType) && !t.GetTypeInfo().IsAbstract);

            foreach (var entityType in types)
            {
                modelBuilder.Entity(entityType);
            }
        }
        
        public static void DiscoverEntityConfigurationFromAssemblyOf<T>(this ModelBuilder modelBuilder)
            where T : class
        {
            var assembly = typeof(T).GetTypeInfo().Assembly;

            //Get all subclasses on EntityTypeConfiguration<>
            var types = assembly.GetTypes()
                .Where(t => t.IsSubclassOfRawGeneric(typeof(EntityMap<>)) && 
                        !t.GetTypeInfo().IsAbstract);

            foreach (var configType in types)
            {
                //Get the entity type in the EntityTypeConfiguration<TEntity>
                var entityType = configType.GetTypeInfo().BaseType.GenericTypeArguments.First();

                //Get the Entity<TEntity>() method on the modelBuilder
                var entMethod = modelBuilder.GetType().GetMethods()
                                    .Where(m => m.Name == nameof(ModelBuilder.Entity) 
                                        && m.IsGenericMethodDefinition
                                        && m.GetParameters().Length == 0).First();

                //Set the TEntity in Entity<TEntity> as the specific entity.
                var entMethodGeneric = entMethod.MakeGenericMethod(new Type[] { entityType });

                //Invoke the Entity<TEntity> method on the modelBuilder to get the exact EntityTypeBuilder<TEntity>
                //This will be the passed as the parameter to the EntityTypeConfiguration<T>.Map method.
                var entTypeBuilder = entMethodGeneric.Invoke(modelBuilder, null);

                //Get the EntityTypeConfiguration<T>.Map method from the specific configType object.
                var mapMethod = configType.GetMethod(nameof(EntityMap<T>.Map));

                //Create instance of the specific EntityTypeConfiguration<TEntity> concrete type
                var initConf = Activator.CreateInstance(configType);
                
                //Invoke the EntityTypeConfiguration<T>.Map method to wire-up the mapping class.
                mapMethod.Invoke(initConf, new[] { entTypeBuilder });
            }
        }

        private static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.GetTypeInfo().IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.GetTypeInfo().BaseType;
            }
            return false;
        }
    }
}
