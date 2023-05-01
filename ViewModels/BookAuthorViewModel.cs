using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    // we use this if we wanna show data from more than one model 
    public class BookAuthorViewModel
    {
        // to use ModelState i should put validation attributes here first 
        public int BookId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string Description { get; set; }
        public int AutherId { get; set; }
        public List<Auther> Authers { get; set; }

        [Required]
        public IFormFile Img { get; set; }

        public string ImageUrl { get; set; }
    }
}
