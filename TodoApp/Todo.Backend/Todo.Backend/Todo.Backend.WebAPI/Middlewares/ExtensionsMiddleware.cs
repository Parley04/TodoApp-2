﻿using Todo.Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Todo.Backend.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                AppUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Cafer Emre",
                    LastName = "Solmaz",
                    EmailConfirmed = true,
                    IsActive=true,
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}
