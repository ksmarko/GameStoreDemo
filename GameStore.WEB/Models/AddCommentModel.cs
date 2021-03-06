﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.WEB.Models
{
    public class AddCommentModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; }
    }
}