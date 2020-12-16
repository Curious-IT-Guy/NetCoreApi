﻿using System;
using System.Collections.Generic;

namespace NetCoreApi.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Todo> Todo { get; set; }
    }
}
