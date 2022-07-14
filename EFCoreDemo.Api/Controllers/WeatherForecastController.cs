using EFCoreDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public User Get(string userName,string newName)
    {
        #region  关闭跟踪
       using MyBBSContext context = new();
       var user =  context.Users.AsNoTracking().FirstOrDefault(m=>m.UserName == userName); //关闭 EFCore 跟踪，并根据传入的值查询数据库
       user.UserName = newName; //赋予新值
       context.Users.Update(user);
    //   context.Users.Add(new User{
    //       UserName = "Leo",
    //       UserNo="1245"
    //   });
       context.SaveChanges();
       return user;
        #endregion
    }
       public User Get2(string userName,string newName)
    {
        #region  不关闭跟踪
       using MyBBSContext context = new();
       var user =  context.Users.FirstOrDefault(m=>m.UserName == userName);
       user.UserName = newName; //赋予新值
       //context.Users.Update(user);//不关闭跟踪，无需update 也可以将保存进数据库
       context.SaveChanges();
       return user;
        #endregion
       

    }


}
