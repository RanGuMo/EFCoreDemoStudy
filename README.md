# EFCore 学习
## 1.通过 实体类生成数据库表（CodeFirst）
### 1.添加nuget包
![nuget包](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657718842689.jpg)
### 2.创建实体类
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDemo.Api.Entitys
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}
```
### 3.创建 EFCoreDemoContext.cs
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Api.Entitys
{
    public class EFCoreDemoContext : DbContext
    {
        public DbSet<User> Users { get; set; } //将实体类User映射为 Users数据库表
        public EFCoreDemoContext()
        {
            
        }
        public EFCoreDemoContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        // 数据库连接字符串 ； TestDb 必须是不存在的数据库
           optionsBuilder.UseSqlServer("server=127.0.0.1;database=TestDb;uid=sa;pwd=123456");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
```
![图片](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657719224369.jpg)
### 4. 用终端打开 输入下面三条，即可完成CodeFirst
```bash
dotnet tool install --global dotnet-ef   //全局安装
dotnet ef migrations add init            //数据库迁移日志文件
dotnet ef database update                //创建TestDB 数据库 并为实体类User 创建Users表
```
![终端](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657719688927.jpg)
![数据库](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657719614862.png)
### 5.数据库字段的修改 方式一（不推荐）
![Users](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657723416499.jpg)
更改数据库的字段
```C#
  public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }
        public int Age { get; set; }
    }
```

![修改](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657723726666.jpg)
![修改](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657723819585.jpg)

### 5.数据库字段的修改 方式二（重点推荐）
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Api.Entitys
{
    public class EFCoreDemoContext : DbContext
    {
        //public DbSet<User> Users { get; set; } //将实体类User映射为 Users数据库表（在OnModelCreating中配置的话，可以不用写这个）
        public EFCoreDemoContext()
        {
            
        }
        public EFCoreDemoContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        // 数据库连接字符串 ； TestDb 必须是不存在的数据库
           optionsBuilder.UseSqlServer("server=127.0.0.1;database=TestDb;uid=sa;pwd=123456");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //表名为UserList，架构为EFCoreDemo。（不写这个，默认是表名字是User，架构在sqlserver为dbo）
            modelBuilder.Entity<User>().ToTable("UserList","EFCoreDemo");
            modelBuilder.Entity<User>().Property(m=>m.UserName)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("用户名");//指定不为空，长度为50，注释为用户名

            base.OnModelCreating(modelBuilder);
        }
    }
}
```
![EFCoreDemoContext](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657725008880.jpg)
![EFCoreDemoContext](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657725115450.jpg)
![EFCoreDemoContext](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657725301430.jpg)
## 2.通过 数据库表来生成 实体类（DBFirst）
### 2.1 VSCode 中生成
```bash
dotnet ef dbcontext scaffold "server=127.0.0.1;database=MyBBS;uid=sa;pwd=123456" "Microsoft.EntityFrameworkCore.SqlServer" -o Models // -o 表示输出的路径
```
![VSCode_DBFirst](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657726657057.jpg)
### 2.2 VS 中生成
```bash
scaffold-DbContext "server=127.0.0.1;database=MyBBS;uid=sa;pwd=123456" "Microsoft.EntityFrameworkCore.SqlServer" -o Models222
```
![VS_DBFirst](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657726925075.jpg)
![VS_DBFirst](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657727080337.jpg)

## 3.EFCore 跟踪
### 3.1 关闭跟踪（局部）（AsNoTracking()）
```C#
[HttpGet(Name = "GetWeatherForecast")]
 public User Get(string userName,string newName)
    {
        #region  关闭跟踪
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
```
### 3.2 不关闭跟踪
```C#
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
```
### 3.3 全局单例 关闭跟踪
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDemo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Api.Factorys
{
    public class DbContextFactory
    {
        private static MyBBSContext _dbContext = null;
        private DbContextFactory()
        { 
        }
       public static MyBBSContext GetMyBBSContext(){
           if(_dbContext==null){
               _dbContext = new MyBBSContext();
               _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
           }
           return _dbContext;
       }
    }
}
```
![全局单例 关闭跟踪](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657804804747.jpg)

#### 使用全局单例 
```C#
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
```
![全局单例 关闭跟踪](https://github.com/RanGuMo/EFCoreDemoStudy/blob/master/EFCoreDemo.Api/Images/1657804869873.jpg)

### 3.4 总结
1.DBContext 不能单例
2.默认是开启 跟踪的，最好 关闭跟踪（可提高性能）
3.开启跟踪时。无需update，只需执行SaveChanges()，就可以 将数据 更新到数据库中
4.关闭跟踪后，不仅需要执行update，还需执行SaveChanges()，才可以 将数据 更新到数据库中（这里就是多了一步update，防止出错）


