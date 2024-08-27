using System.Reflection.Metadata.Ecma335;

namespace Exemplo01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var faixas = new List<INSSFaixa>
              {
                  new INSSFaixa(0,1412, 7.5m ),
                  new INSSFaixa(1412.01m, 2666.68m, 9m ),
                  new INSSFaixa(2666.69m, 4000.03m, 12m ),
                  new INSSFaixa(4000.04m, 7786.02m, 14m )
              };
        }
    }
}
