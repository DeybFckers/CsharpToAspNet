using FirstWebApi.Contracts;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApi.Models
{
                        //inherit from IBaseModel
    public class User : IBaseModel
    {
        public int Id { get; set; }
        //to have a validation, you should do this
        [Required, EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }

        /*if you have a huge project you should have a different model 
         * for domain, Infrastructure, View Model*/
    }
}
