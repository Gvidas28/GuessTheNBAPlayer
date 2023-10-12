using GuessTheNBAPlayer.Model.Entities.Enums;
using System;

namespace GuessTheNBAPlayer.Model.Services
{
    public static class HelperService
    {
        public static int GetRandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, ++max);
        }

        public static PlayerPosition DeterminePlayerPosition(string position) => position switch
        {
            "G" => PlayerPosition.Guard,
            "F" => PlayerPosition.Forward,
            "C" => PlayerPosition.Center,
            _ => PlayerPosition.Unknown
        };

        public static string DeterminePlayerWeight(int? pounds) => pounds is null ? "Unknown" : $"{string.Format("{0:0.00}", pounds / 2.205)} kg";

        public static string DeterminePlayerHeight(int? feet, int? inches) => feet is null || inches is null ? "Unknown" : $"{feet * 30 + inches * 3} cm";
    }
}