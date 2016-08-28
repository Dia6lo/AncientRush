using System;

namespace AncientRush
{
    public class Utility
    {
        public static float SinMotion(float time, float currentTime, float from, float to)
        {
            var v = (float)Math.Sin(currentTime / time * (Math.PI / 2));
            return Lerp(v, from, to);
        }

        public static float Lerp(float amount, float value1, float value2)
        {
            return value1 + (value2 - value1) * amount;
        }
    }
}