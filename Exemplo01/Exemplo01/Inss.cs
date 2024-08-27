namespace Exemplo01
{
    public class Inss
    {
        private const decimal DESCONTO_TETO = 908.85m;
        private readonly IReadOnlyCollection<INSSFaixa> faixas;
        private readonly decimal teto_ultima_faixa;


        public Inss()
        {
            faixas = new List<INSSFaixa>
              {
                  new INSSFaixa(0,1412, 7.5m ),
                  new INSSFaixa(1412.01m, 2666.68m, 9m ),
                  new INSSFaixa(2666.69m, 4000.03m, 12m ),
                  new INSSFaixa(4000.04m, 7786.02m, 14m )
              }.AsReadOnly();

            teto_ultima_faixa = faixas.Last().Teto;
        }


        //Calcular o desconto por faixa
        public decimal CalcularDesconto(decimal salario)
        {
            if (salario > teto_ultima_faixa)
            {
                return DESCONTO_TETO;
            }

            var descontos = new List<decimal>();

            foreach (var item in faixas)
            {
                if (!item.ContemValor(salario))
                    break;

                var valorEfetivoFaixa = item.ObterValorFaixa(salario);
                var desconto = Math.Round(valorEfetivoFaixa * item.Aliquota / 100, 2);
                Console.WriteLine($"Faixa: {item.Aliquota}\tDesconto: {desconto}");
                descontos.Add(desconto);

            }
            return descontos.Sum();//Somar os descontos de cada faixa
        }
    }
}
