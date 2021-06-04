using System;

namespace SmartSchoolCode.WebAPI.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetAno(this DateTime dateTime)
        {
            var dataAtual = DateTime.UtcNow;
            int idade = dataAtual.Year - dateTime.Year;

            if(dataAtual < dateTime.AddYears(idade))
                idade--;

            return idade;
        }
    }
}