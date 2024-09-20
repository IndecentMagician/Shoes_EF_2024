﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shoes_EF_2024.Web.ViewModels.Sports
{
    public class SportEditVm
    {
        public int SportId { get; set; }
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Sport Name")]
        public string SportName { get; set; } = null!;
    }
}
