﻿using System;

namespace Domodedovo.PhoneBook.Data.Models.Entities
{
    public class Picture : EntityBase
    {
        public PictureType Type { get; set; }
        public Uri Url { get; set; }
        public byte[] Image { get; set; }
        public string Path { get; set; }
    }
}