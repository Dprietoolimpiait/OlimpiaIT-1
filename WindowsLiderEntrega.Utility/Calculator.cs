namespace WindowsLiderEntrega.Utility
{
    public class Calculator
    {
        /// <summary>
        /// Calcula la comisión
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long CalculateCommission(long n)
        {

            int count = 0;
            long a = 0;
            while (count < n)
            {
                a = 2;

                long b = 2;
                int prime = 1;
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }
    }
}
