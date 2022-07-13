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
