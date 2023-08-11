﻿using Core.Enums;

namespace PseuSM.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public ImageTypes Type { get; set; } = ImageTypes.Regular;
    }
}
