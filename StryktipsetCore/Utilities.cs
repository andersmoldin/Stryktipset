using System.Collections.Generic;

namespace StryktipsetCore
{
    public class Utilities
    {
        public static Dictionary<int, int> Faktorisera(int input)
        {
            int b;
            Dictionary<int, int> faktorer = new Dictionary<int, int>();

            for (b = 2; input > 1; b++)
                if (input % b == 0)
                {
                    int x = 0;
                    while (input % b == 0)
                    {
                        input /= b;
                        x++;
                    }
                    faktorer.Add(b, x);
                }

            return faktorer;
        }
    }
}
