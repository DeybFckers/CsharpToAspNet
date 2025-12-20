using FirstWebApi.Contracts;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApi.Models
{
                        //inherit from IBaseModel
    public class Hero : IBaseModel, IValidatableObject
                                //if your validation is not in builtin attributes add the IValidateObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required Hero Name")]
        public String Name { get; set; }
        [Required, Range (18, 100)]// we use builtin attributes, it means the age must 18 above
        public int Age { get; set; }
        //then do this
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age % 2 != 0)
                yield return new ValidationResult("Even Number Only");
        }
    }
}
