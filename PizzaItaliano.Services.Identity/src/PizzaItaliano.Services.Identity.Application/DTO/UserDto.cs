﻿using PizzaItaliano.Services.Identity.Core.Entities;
using System;
using System.Collections.Generic;

namespace PizzaItaliano.Services.Identity.Application.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<string> Permissions { get; set; }

        public UserDto()
        {
        }

        public UserDto(User user)
        {
            Id = user.Id;
            Email = user.Email.Value;
            Role = user.Role;
            CreatedAt = user.CreatedAt;
            Permissions = user.Permissions;
        }
    }
}
