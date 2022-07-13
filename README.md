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
