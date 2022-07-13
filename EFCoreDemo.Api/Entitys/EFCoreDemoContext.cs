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