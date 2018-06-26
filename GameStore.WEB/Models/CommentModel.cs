using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WEB.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Game { get; set; }
        public string Parent { get; set; }
        public string Publisher { get; set; }
    }
}