using UnityEngine;

namespace Code.Runtime.Common.Extensions
{
  public static class MathExtensions
  {
    public static int NextPowerOfTwo(this float n) => 
      NextPowerOfTwo((int)n);
    
    public static int NextPowerOfTwo(this int n)
    {
      int count = 0;

      // First n in the below  
      // condition is for the 
      // case where n is 0 
      if (n > 0 && (n & (n - 1)) == 0)
        return n;

      while (n != 0)
      {
        n >>= 1;
        count += 1;
      }

      return 1 << count;
    }
    
    public static float Round(this float value, int digits = 1)
    {
      float exponent = Mathf.Pow(10f, digits);
      return Mathf.Round(value * exponent) / exponent;
    }
    
    public static Vector3 Round(this Vector3 value, int digits = 1) => 
      new(value.x.Round(digits), value.y.Round(digits), value.z.Round(digits));
  }
}