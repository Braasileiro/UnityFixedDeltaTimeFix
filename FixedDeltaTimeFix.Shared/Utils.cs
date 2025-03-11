namespace FixedDeltaTimeFix.Shared
{
    public static class Utils
    {
        public static float GetLowestRefreshRateMultipleAbove50(int refreshRate)
        {
            for (var rate = refreshRate / 2; rate > 50; rate--)
            {
                if (refreshRate % rate == 0)
                {
                    return rate;
                }
            }

            return refreshRate;
        }
    }
}
