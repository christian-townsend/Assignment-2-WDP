using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AspNetUserRolesEntityFramework.Models
{
    public class MachineLearningCompaniesFeedback
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        public DateTime PostDate { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Heading")]
        public string TopicTitle { get; set; }

        [Required]
        [Display(Name = "Select a Company")]
        public string Company { get; set; }

        [Required]
        [Display(Name = "StarRating")]
        public int StarRating { get; set; }

        [Required]
        [Display(Name = "Feedback")]
        public string MessageContent { get; set; }

        public int Like { get; set; }
        public int Dislike { get; set; }

        public bool canIncreaseLike { get; set; }
        public bool canIncreaseDislike { get; set; }

    }
}
