//using ProjectName.Application.Common;
//using FluentValidation;

//namespace ProjectName.Application.Features.Users.CreateUser;

//public class CreateUserValidator : AbstractValidator<CreateUserRequest>
//{
//    public CreateUserValidator()
//    {
//        RuleLevelCascadeMode = ClassLevelCascadeMode;

//        RuleFor(x => x.FirstName)
//            .NotEmpty()
//            .MaximumLength(StringSizes.Max);

//        //RuleFor(x => x.Individuality)
//        //    .NotEmpty()
//        //    .MaximumLength(StringSizes.Max);

//        RuleFor(x => x.UserType)
//            .IsInEnum();

//        //RuleFor(x => x.Age)
//        //    .GreaterThan(0);

//        RuleFor(x => x.LastName)
//            .MaximumLength(StringSizes.Max);

//        RuleFor(x => x.Team)
//            .MaximumLength(StringSizes.Max);
//    }
//}