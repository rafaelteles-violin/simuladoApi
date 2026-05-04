using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulado.Domain
{
    public class HorarioBrasilia
    {
        public static DateTime Get()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ZonaDeTempo.ObterZonaDeTempo());
        }

        public static DateTime Set(DateTime data)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(data.ToUniversalTime(), ZonaDeTempo.ObterZonaDeTempo());
        }
    }

    public static class ZonaDeTempo
    {
        public static TimeZoneInfo ObterZonaDeTempo()
        {
            TimeZoneInfo cetZone;

            try
            {
                cetZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            }
            catch
            {
                cetZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
            }

            return cetZone;
        }
    }
}
