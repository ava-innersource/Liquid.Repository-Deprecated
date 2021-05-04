﻿using Liquid.Core.Telemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Liquid.Repository.EntityFramework.Extensions
{
    /// <summary>
    /// Entity Framework Service Collection Extensions Class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the entity framework database repositories and Context.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="assemblies">The assemblies.</param>
        public static void AddEntityFramework<TContext>(this IServiceCollection services, params Assembly[] assemblies) where TContext : DbContext
        {
            services.AddTransient<DbContext, TContext>();
            services.AddScoped<IEntityFrameworkDataContext, EntityFrameworkDataContext>();
            AddEntityRepositories(services, assemblies);
        }

        /// <summary>
        /// Adds the entity framework database repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="assemblies">The assemblies.</param>
        private static void AddEntityRepositories(IServiceCollection services, Assembly[] assemblies)
        {
            var repositoryTypes = assemblies.SelectMany(a => a.ExportedTypes)
                            .Where(t => t.BaseType != null &&
                                        t.BaseType.Assembly.FullName == Assembly.GetAssembly(typeof(EntityFrameworkDataContext)).FullName &&
                                        t.BaseType.Name.StartsWith("EntityFrameworkRepository"));

            foreach (var repositoryType in repositoryTypes)
            {
                var interfaceType = repositoryType.GetInterfaces().FirstOrDefault(t =>
                    t.GetInterfaces()
                        .Any(i => i.Assembly.FullName == Assembly.GetAssembly(typeof(ILightDataContext)).FullName &&
                                  i.Name.StartsWith("ILightRepository")));

                if (interfaceType != null) services.AddScoped(interfaceType, repositoryType);
            }
        }
    }
}
