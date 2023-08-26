using JoimChat.Models;
using JoimChat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JoimChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ChatDbContext>(options =>
                options.UseNpgsql("Host=172.17.0.1;Port=5432;Database=JoimChatDB;Username=postgres;Password=joimchat"));

            
            builder.Services.TryAddTransient<IUsersService, UsersService>();
            builder.Services.TryAddTransient<IMessagesService, MessagesService>();
            builder.Services.AddControllers();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();
            app.MapHub<ChatHub>("/chatHub");

            app.UseAuthorization();

            app.Run();
        }
    }
}