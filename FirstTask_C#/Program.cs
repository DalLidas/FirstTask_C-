namespace FirstTask
{
    /// <summary>
    /// Equation of the form mult*x^pow
    /// </summary>
    public struct Monomial
    {
        public double mult { get; set; }
        public int pow { get; set; }

        public Monomial() { this.mult = 0; this.pow = 0; }
        public Monomial(double mult) { this.mult = mult; this.pow = 0; }
        public Monomial(double mult, int pow) { this.mult = mult; this.pow = pow; }

        public void TakeDerivative ()
        {
            //Take derevative from const
            if (pow == 0)
            {
                mult = 0;
                return;
            }

            mult *= pow;
            pow -= 1;
        }

        public void Show()
        {
            Console.WriteLine(mult + "*x^" + pow);
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Monomial m1 = new Monomial(2.5, 0);
            Monomial m2 = m1;
            m1.Show();
            m2.Show();
            m1.TakeDerivative();
            Console.WriteLine();
            m1.Show();
            
           
        }
    }
}
