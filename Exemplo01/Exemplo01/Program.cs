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

    public class INSSFaixa
    {
        public decimal Piso { get; set; }
        public decimal Teto { get; set; }
        public decimal Aliquota { get; set; }
    }

    public interface IINSSImposto
    {
        INSSFaixa BuscarFaixa(decimal salario);
    }

    public class INSSV1 : IINSSImposto
    {
        public INSSV1()
        {
            tabela = new List<INSSFaixa>
            {
                new INSSFaixa { Piso = 0, Teto = 1412, Aliquota = 7.5m },
                new INSSFaixa { Piso = 1412.01m, Teto = 2666.68m, Aliquota = 9m },
                new INSSFaixa { Piso = 2666.69m, Teto = 4000.03m, Aliquota = 12m },
                new INSSFaixa { Piso = 4000.04m, Teto = 7786.02m, Aliquota = 14m }
            };
        }

        private IReadOnlyCollection<INSSFaixa> tabela;

        public INSSFaixa BuscarFaixa(decimal salario)
        {
            foreach (var faixa in tabela)
            {
                if (salario >= faixa.Piso && salario <= faixa.Teto)
                {
                    return faixa;
                }
            }

            return null;
        }
    }




}
