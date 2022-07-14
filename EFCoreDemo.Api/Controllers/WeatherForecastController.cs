using EFCoreDemo.Api.Factorys;
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
        #region  关闭跟踪（局部）
       using MyBBSContext context = new();
       var user =  context.Users.AsNoTracking().FirstOrDefault(m=>m.UserName == userName); //关闭 EFCore 跟踪，并根据传入的值查询数据库
       user.UserName = newName; //赋予新值
       context.Users.Update(user);//关闭 EFCore 跟踪后，必须使用update，context.SaveChanges(); 才会保存进数据库中
    //   context.Users.Add(new User{
    //       UserName = "Leo",
    //       UserNo="1245"
    //   });
       context.SaveChanges();
       return user;
        #endregion
    }
       [HttpGet("Get2")]
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
    [HttpGet("Get3")]
      public User Get3(string userName,string newName)
    {
        #region  全局单例 关闭跟踪
       var context = DbContextFactory.GetMyBBSContext();
       var user =  context.Users.FirstOrDefault(m=>m.UserName == userName);
       user.UserName = newName; //赋予新值
       context.Users.Update(user);//关闭 EFCore 跟踪后，必须使用update，context.SaveChanges(); 才会保存进数据库中
       context.SaveChanges();
       return user;
        #endregion
    }

      [HttpGet("GetAllOfPage")]
      public IEnumerable<Post> GetAllOfPage(int pageIndex=1,int pageSize=30)
    {
         var context = DbContextFactory.GetMyBBSContext();
          int skipNum = (pageIndex-1)*pageSize; //跳过的数量
          var list = context.Posts.Skip(skipNum).Take(pageSize);//跳过多少条，然后再取页数
          return list.ToList();  

          //不要 向下面这样写  先写ToList（这里就是取出数据库中所有的数据再来分页了）数据量大会非常影响性能
          // var list = context.Posts.ToList().Skip(skipNum).Take(pageSize);//跳过多少条，然后再取页数
         //  return list;
    }


}
