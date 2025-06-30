//using ProjectName.Application.Common;
//using FluentValidation;

//namespace ProjectName.Application.Features.Users.UpdateUser;

//public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
//{
//    public UpdateUserValidator()
//    {

//        RuleFor(x => x.Id)
//            .NotEmpty();
        
//        RuleFor(x => x.FirstName)
//            .NotEmpty()
//            .MaximumLength(StringSizes.Max);

//        //RuleFor(x => x.Individuality)
//        //    .NotEmpty();

//        RuleFor(x => x.UserType)
//            .IsInEnum();

//        //RuleFor(x => x.Age)
//        //    .GreaterThan(0);
        
//        RuleFor(x => x.LastName)
//            .MaximumLength(StringSizes.Max);

//        //RuleFor(x => x.Team)
//        //    .MaximumLength(StringSizes.Max);
//    }
//}