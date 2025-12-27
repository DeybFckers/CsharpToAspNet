using HotelBookingApi.Models;

namespace HotelBookingApi.Validator
{
    public class UserValidator
    {
       public bool userValidator(Users users, out string errorMessage)
        {
            if (string.IsNullOrEmpty(users.FirstName))
            {
                errorMessage = "First Name is Required!";
            }
            if (string.IsNullOrEmpty(users.LastName))
            {
                errorMessage = "Last Name is Required!";
            }
            if (string.IsNullOrEmpty(users.Email))
            {
                errorMessage = "Email is Required!";
            }
            if (string.IsNullOrEmpty(users.Password))
            {
                errorMessage = "Passowrd is Required!";
            }

            errorMessage = null;
            return true;
        }
    }
}
