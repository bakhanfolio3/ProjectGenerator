﻿using System.ComponentModel.DataAnnotations;

namespace ProjectName.Application.DTOs.Identity;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}