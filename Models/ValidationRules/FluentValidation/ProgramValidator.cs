using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Gazi_Salon_Takip.Models.EntityFramework
{
    public class ProgramValidator:AbstractValidator<Program>
    {
        public ProgramValidator()
        {
            RuleFor(p => p.SalonID).NotEmpty().WithMessage("Salon Boş Olamaz");

            RuleFor(p => p.Program_Adı).NotEmpty().WithMessage("Program Adı Boş Olamaz")
                .Length(1, 150).WithMessage("Porgram Adı 1 ile 150 Karakter Arasında Olabilir");

            RuleFor(p => p.Program_Sahip).NotEmpty().WithMessage("Program Sahibi Boş Olamaz")
                .Length(1,150).WithMessage("Porgram Sahibi 1 ile 150 Karakter Arasında Olabilir");

            RuleFor(p => p.Program_Tarih).NotEmpty().WithMessage("Program Tarihi Boş Olamaz");

            RuleFor(p => p.Program_Baslangic).NotEmpty().WithMessage("Program Başlangıç Saati Boş Olamaz");

            RuleFor(p => p.Program_Bitis).NotEmpty().WithMessage("Program Bitiş Saati Boş Olamaz");

            RuleFor(p => p.Program_Aciklama).NotEmpty().WithMessage("Program Açıklaması Boş Olamaz");




        }
    }
}