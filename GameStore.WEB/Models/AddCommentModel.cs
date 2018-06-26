using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.WEB.Models
{
    public class AddCommentModel
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required]
        public string Publisher { get; set; }
    }
}