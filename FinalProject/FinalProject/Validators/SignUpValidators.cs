using FinalProject.Models;
using FinalProject.ViewModel;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace FinalProject.Validators
{
    public class SignUpValidators : AbstractValidator<SignUpViewModel>
    {
        public SignUpValidators()
        {
            RuleFor(a => a.UsernameSurname).NotEmpty().WithMessage("İsim Soyisim Alanı Boş Bırakılamaz").NotNull().WithMessage("İsim Soyisim Alanı Boş Bırakılamaz");
            RuleFor(a => a.Eposta).EmailAddress().WithMessage("Lütfen Geçerli Bir Email Adresi Giriniz").Must(UniqueEmail).WithMessage("Bu EMail zaten Kullanılıyor");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Şifre Alanı Boş Bırakılamaz").NotNull().WithMessage("Şifre Alanı Boş Bırakılamaz").MinimumLength(8)
            .Matches("[A-Z]").WithMessage("'{PropertyName}' Bir Veya Daha Fazla Büyük Harf İçermelidir.")
            .Matches("[a-z]").WithMessage("'{PropertyName}' Bir Veya Daha Fazla Küçük Harf İçermelidir.")
            .Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{ PropertyName}' must contain one or more special characters.")
            .Matches("^[^£# “”]*$").WithMessage("'{PropertyName}' must not contain the following characters £ # “” or spaces.");

            RuleFor(a => a.PasswordAgain).Equal(a => a.Password).WithMessage("Şifreler Eşleşmiyor");


        }
        private bool UniqueEmail(string name)
        {
            FinalProjectDBContext _db = new FinalProjectDBContext();
            var email = _db.Logins.Where(x => x.EPosta.ToLower() == name.ToLower()).SingleOrDefault();

            if (email == null) return true;
            return false;
        }
    }
}
