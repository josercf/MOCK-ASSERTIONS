using System.Reflection.Metadata.Ecma335;

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

        public bool ContemValor(decimal valor)
        {
            if (valor > Teto)
                return true;

            return valor >= Piso;
        }

        public decimal ObterValorFaixa(decimal salario)
        {
            //Verificamos se é a primeira faixa, 
            //caso não seja, subtraímos o valor da faixa anterior
            //que sempre será R$0,01 menor que o piso da faixa atual
            var valorFaixaAnterior = Aliquota > 7.5m ? Piso - 0.01m : Piso;

            //caso o salário seja maior 
            // que o teto da faixa,
            // retornamos apenas o intervalo da faixa
            if (salario > Teto)
            {
                return Teto - valorFaixaAnterior;
            }

            return salario - valorFaixaAnterior;
        }
    }


    public class Inss
    {
        private const decimal DESCONTO_TETO = 908.85m;

        public Inss()
        {
            Faixas = new List<INSSFaixa>
              {
                  new INSSFaixa { Piso = 0, Teto = 1412, Aliquota = 7.5m },
                  new INSSFaixa { Piso = 1412.01m, Teto = 2666.68m, Aliquota = 9m },
                  new INSSFaixa { Piso = 2666.69m, Teto = 4000.03m, Aliquota = 12m },
                  new INSSFaixa { Piso = 4000.04m, Teto = 7786.02m, Aliquota = 14m }
              };
        }

        public List<INSSFaixa> Faixas { get; set; }

        //Calcular o desconto por faixa
        public decimal CalcularDesconto(decimal salario)
        {
            if (salario > Faixas.Last().Teto)
            {
                return DESCONTO_TETO;
            }

            var descontos = new List<decimal>();

            foreach (var item in Faixas)
            {
                if (item.ContemValor(salario))
                {
                    var valorEfetivoFaixa = item.ObterValorFaixa(salario);
                    //Somar os descontos de cada faixa
                    var desconto = valorEfetivoFaixa * item.Aliquota / 100;
                    descontos.Add(desconto);
                }
                else
                {
                    break;
                }
            }
            return descontos.Sum();
        }
    }
}
