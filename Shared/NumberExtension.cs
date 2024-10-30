namespace Desafio.Shared
{
    public static class NumberExtension
    {
        public static decimal ToDecimal(this int value)
        {
            return Convert.ToDecimal(value);
        }

        public static int ToInt(this decimal value)
        {
            return Convert.ToInt32(value);
        }
    }
}
