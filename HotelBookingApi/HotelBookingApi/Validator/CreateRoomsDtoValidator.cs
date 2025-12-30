using FluentValidation;
using HotelBookingApi.Models;

namespace HotelBookingApi.Validator
{
    internal sealed class CreateRoomsDtoValidator : AbstractValidator<CreateRoomsDto>
    {
        public CreateRoomsDtoValidator()
        {
            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("Room Number is Requierd");

            RuleFor(x => x.RoomType)
                .NotEmpty().WithMessage("Room Type is Required");

            RuleFor(x => x.PricePerNight)
                .NotEmpty().WithMessage("Price is Required");

            RuleFor(x => x.Capacity).NotEmpty()
                .WithMessage("Capacity is Required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description");

        }
    }
}
