﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Domain.ViewModels.User
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Enter your email")]
        [MinLength(7, ErrorMessage = "Email must be greater than 7")]
        [MaxLength(50, ErrorMessage = "Email must be less than 50")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your name")]
        [MinLength(1, ErrorMessage = "Password must be greater than 1")]
        [MaxLength(30, ErrorMessage = "Password must be less than 30")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be greater than 6")]
        [MaxLength(30, ErrorMessage = "Password must be less than 30")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat your password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be greater than 6")]
        [MaxLength(30, ErrorMessage = "Password must be less than 30")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }
    }
}
