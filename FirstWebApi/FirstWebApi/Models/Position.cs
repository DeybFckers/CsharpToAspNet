using FirstWebApi.Contracts;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApi.Models
{
                            //inherit from IBaseModel
    public class Position : IBaseModel
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required(AllowEmptyStrings = true)]// do this if you want a null value
        public String Description { get; set; }
        
    }
}
