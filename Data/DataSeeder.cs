using System.Collections.Generic;
using efcore2_webapi.AppServices.Contracts;
using efcore2_webapi.Repository;
using efcore2_webapi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace efcore2_webapi.Data
{
    public static class DataSeeder
    {
        public static IApplicationBuilder SeedPersonData(this IApplicationBuilder appBuilder, string jsonData)
        {
            using (var serviceScope = appBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.HasUserSeedData())
                {
                    var userAppService = serviceScope.ServiceProvider.GetService<IUserAppService>();
                    var users = JsonConvert.DeserializeObject<List<UserDto>>(jsonData);

                    foreach (var user in users)
                    {
                        userAppService.Save(user);
                    }
                }
            }

            return appBuilder;
        }

        public static IApplicationBuilder SeedTodoData(this IApplicationBuilder appBuilder, string todoJson)
        {
            using (var serviceScope = appBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.HasTodoSeedData())
                {
                    var todoAppService = serviceScope.ServiceProvider.GetService<ITodoAppService>();
                    var todos = JsonConvert.DeserializeObject<List<TodoItemDto>>(todoJson);

                    foreach (var todo in todos)
                    {
                        todoAppService.Save(todo);
                    }
                }
            }

            return appBuilder;
        }

        private static bool HasUserSeedData(this IServiceScope serviceScope)
        {
            var userRepo = serviceScope.ServiceProvider.GetRequiredService<IUserRepository>();

            return userRepo.TotalUserCount() > 0;
        }

        private static bool HasTodoSeedData(this IServiceScope serviceScope)
        {
            var userRepo = serviceScope.ServiceProvider.GetRequiredService<ITodoRepository>();

            return userRepo.TotalUserCount() > 0;
        }
    }
}