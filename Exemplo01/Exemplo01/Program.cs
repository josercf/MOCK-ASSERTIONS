namespace Exemplo01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }


    //calcular folha de pagamento, descontando IR, INSS

    //calcular desconto INSS
    /*
     * O desconto do INSS é efetuado conforme desconto abaixo
     * 
        SALÁRIO-DE-CONTRIBUIÇÃO (R$)    ALÍQUOTA PROGRESSIVA PARA FINS DE RECOLHIMENTO AO INSS

        Até 1.412,00                7,5%
        De 1.412,01 até 2.666,68    9%
        De 2.666,69 até 4.000,03    12%
        De 4.000,04 até 7.786,02    14%
     */

    //Armazenar as faixas de desconto do INSS
    public class INSSFaixa
    {
        public decimal Piso { get; set; }
        public decimal Teto { get; set; }
        public decimal Aliquota { get; set; }

    }


    public class Inss
    {
        public Inss()
        {
              Faixas = new List<INSSFaixa>
              {
                  new INSSFaixa { Piso = 0, Teto = 1412, Aliquota = 7.5m },
                  new INSSFaixa { Piso = 1412.01m, Teto = 2666.68m, Aliquota = 9 },
                  new INSSFaixa { Piso = 2666.69m, Teto = 4000.03m, Aliquota = 12 },
                  new INSSFaixa { Piso = 4000.04m, Teto = 7786.02m, Aliquota = 14 }
              };
        }

        public List<INSSFaixa> Faixas { get; set; }

        //Calcular o desconto por faixa
        public decimal CalcularDesconto(decimal salario)
        {
            var desconto = 0m;

            foreach (var item in Faixas)
            {
                if (salario >= item.Piso && salario <= item.Teto)
                {
                    //Somar os descontos de cada faixa
                    desconto = salario * item.Aliquota / 100;
                    return desconto;
                }

            }
            return 0;
        }

        
        
    }
}
