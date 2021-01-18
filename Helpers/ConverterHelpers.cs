using System;

namespace DesignIntentDesktop.Helpers
{
    public static class ConvererHelpers
    {
        public static double ConvertDouble(string value)
        {
            return double.TryParse(value, out var unit) ? unit : 0.00;
        }

        public static int ConvertInt(string value)
        {
            return int.TryParse(value, out var unit) ? unit : 0;
        }

        public static double Round(double value, double factor)
        {
            return Math.Round(value * factor) / factor;
        }

    }
}