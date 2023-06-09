﻿using Levi9.ERP.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class DocumentItemSyncRequest
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public float Price { get; set; }
        [Required]
        [RegularExpression("^(RSD|USD|EUR|GBP|RMB|INR|JPY)$", ErrorMessage = "The value of Currency can be: RSD, USD, EUR, GBP, RMB, INR, JPY.")]
        public string Currency { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int Quantity { get; set; }
    }
}
