namespace Exemplo01
{
    public class Inss
    {
        private const decimal DESCONTO_TETO = 908.85m;

        public Inss()
        {
            Faixas = new List<INSSFaixa>
              {
                  new INSSFaixa(0,1412, 7.5m ),
                  new INSSFaixa(1412.01m, 2666.68m, 9m ),
                  new INSSFaixa(2666.69m, 4000.03m, 12m ),
                  new INSSFaixa(4000.04m, 7786.02m, 14m )
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
                    var desconto = Math.Round(valorEfetivoFaixa * item.Aliquota / 100, 2);
                    Console.WriteLine($"Faixa: {item.Aliquota}\tDesconto: {desconto}");
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
