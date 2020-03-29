using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Gazi_Salon_Takip.Models.EntityFramework;

namespace Gazi_Salon_Takip.Models.ValidationRules.FluentValidation
{
    public class UyeValidator:AbstractValidator<Uye>
    {
        public UyeValidator()
        {
            RuleFor(u => u.KullaniciAd).NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz!")
               .Length(2, 150).WithMessage("Kullanıcı Adı En Fazla 150 En Az 2 Karakter Olmalıdır.");

            RuleFor(u => u.KullaniciAdSoyad).NotEmpty().WithMessage("Kullanıcı Ad ve Soyad Boş Bırakılamaz!")
                .Length(2, 150).WithMessage("Kullanıcı Adı En Fazla 150 En Az 2 Karakter Olmalıdır.");

            RuleFor(u => u.KullaniciEmail).NotEmpty().WithMessage("Email Adresi Boş Bırakılamaz!")
                .EmailAddress().WithMessage("Email Adress Formatında Giriniz!");

            RuleFor(u => u.KullaniciSifre).NotEmpty().WithMessage("Şife Boş Bırakılamaz!")
                .Length(4, 10).WithMessage("Şifre En Fazla 10 En Az 4 Karakter Olmalıdır.");
        }
    }
}