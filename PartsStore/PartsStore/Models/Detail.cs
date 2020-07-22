using System.ComponentModel.DataAnnotations;

namespace PartsStore.Models
{
    public class Detail
    {
        [Key]
        public int DetailsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
   

}