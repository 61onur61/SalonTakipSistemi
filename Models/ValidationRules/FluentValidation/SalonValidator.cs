using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Gazi_Salon_Takip.Models.EntityFramework;

namespace Gazi_Salon_Takip.Models.ValidationRules.FluentValidation
{
    public class SalonValidator:AbstractValidator<Salon>
    {
        public SalonValidator()
        {
            RuleFor(s => s.Salon_Adi).NotEmpty().WithMessage("Salon Adı Boş Olamaz")
                .Length(1, 100).WithMessage("Salon Adı 1 ile 100 Karakter Arasında Olabilir.");

            RuleFor(s => s.Salon_Adresi).NotEmpty().WithMessage("Salon Adresi Boş Olamaz");

            RuleFor(s => s.SalonBilgisi).NotEmpty().WithMessage("Salon Bilgisi Boş Olamaz");

            RuleFor(s => s.Salon_Iletisim).NotEmpty().WithMessage("İletişim Boş Olamaz");
        }
    }
}