//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HowToWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class requests
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int upvote { get; set; }
        public bool isDone { get; set; }
        public int usersId { get; set; }
    
        public virtual users users { get; set; }
    }
}
