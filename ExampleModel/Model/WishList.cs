﻿using System;
using System.Collections.Generic;

namespace ExampleModel.Model
{
    public class WishList
    {
        public int UserId { get; set; }
        public List<String> wishes { get; set; } = new List<string>();
    }
}