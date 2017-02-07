// Pick up all concrete types that inherit from Entity
using System;
using System.Reflection;
using efcore2_webapi.Infrastructure.DomainKernel;
using FluentModelBuilder.Configuration;

namespace efcore2_webapi.Repository.Mappings
{
    public class EntityAutoConfiguration : DefaultEntityAutoConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return base.ShouldMap(type) && type.GetTypeInfo().IsSubclassOf(typeof(Entity));
        }
    }
}