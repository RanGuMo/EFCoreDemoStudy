using System;
using System.Collections.Generic;

namespace EFCoreDemo.Api.Models222
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserNo { get; set; } = null!;
        public string? UserName { get; set; }
        public int UserLevel { get; set; }
        public string? Password { get; set; }
        public bool IsDelete { get; set; }
        public Guid? Token { get; set; }
        public Guid? AutoLoginTag { get; set; }
        public DateTime? AutoLoginLimitTime { get; set; }
    }
}
