﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
