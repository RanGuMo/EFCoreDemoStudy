using System;
using System.Collections.Generic;

namespace EFCoreDemo.Api.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string? PostTitle { get; set; }
        public string? PostIcon { get; set; }
        public int PostTypeId { get; set; }
        public string PostType { get; set; } = null!;
        public string? PostContent { get; set; }
        public int Clicks { get; set; }
        public int Replys { get; set; }
        public DateTime? CreateTime { get; set; }
        public int CreateUserId { get; set; }
        public DateTime? EditTime { get; set; }
        public int EditUserId { get; set; }
        public DateTime? LastReplyTime { get; set; }
        public int LastReplyUserId { get; set; }
        public string? Up { get; set; }
        public string? Down { get; set; }
    }
}
