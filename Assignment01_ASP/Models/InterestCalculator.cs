using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment01_ASP.Models {
    public class InterestCalculator {

        public InterestCalculator() { }
        public InterestCalculator(double principal, double rate, int compound, double years) { }
    
        public double Principal { get; set; }
        public double Rate { get; set; }
        public int Compound { get; set; }
        public double Years { get; set; }

        public double CompoundInterestCalc() {
            double valueOfBase = 1+ ((Rate / 100) / Compound);
            double exponent = (Compound * Years);
            double result = (Principal * Math.Pow(valueOfBase, exponent));
            Console.WriteLine(result);
            return Math.Round(result, 2, MidpointRounding.AwayFromZero);
        }
    }
}
