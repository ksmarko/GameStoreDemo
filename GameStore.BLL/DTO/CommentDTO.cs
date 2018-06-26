using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public GameDTO Game { get; set; }
        public CommentDTO Parent { get; set; }
        public string Publisher { get; set; }
    }
}
