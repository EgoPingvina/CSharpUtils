namespace CSharpUtils.Numeric
{
    public static class NumericConvertions
    {
        public static int Limiter(int min, int max, int value)
            => value > min
                ? value < max
                    ? value
                    : max
                : min;

        public static bool IsInRange(int min, int max, int value)
            => value >= min && value <= max;

        public static int RangeTransform(int minCurrent, int maxCurrent, int minNew, int maxNew, int value)
            => (Limiter(minCurrent, maxCurrent, value) - minCurrent) * (maxNew - minNew) / (maxCurrent - minCurrent) + minNew;
    }
}