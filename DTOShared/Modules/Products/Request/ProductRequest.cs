﻿using System.ComponentModel.DataAnnotations;

namespace DTOShared.Modules.Products.Request
{
    public class ProductRequest
    {



        [StringLength(500)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]

        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
