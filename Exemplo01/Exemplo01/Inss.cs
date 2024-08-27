using Microsoft.Extensions.Logging;

namespace Exemplo01
{
    public class Inss
    {
        private const decimal DESCONTO_TETO = 908.85m;
        private readonly IReadOnlyCollection<INSSFaixa> faixas;
        private readonly decimal teto_ultima_faixa;
        private readonly ILogger<Inss> logger;

        public Inss(ILogger<Inss> logger, IEnumerable<INSSFaixa> faixaList)
        {
            this.logger = logger;
            faixas = faixaList.ToList().AsReadOnly();
            teto_ultima_faixa = faixas.Last().Teto;
        }

        //Calcular o desconto por faixa
        public decimal CalcularDesconto(decimal salario)
        {
            if (salario > teto_ultima_faixa)
            {
                logger.LogInformation("Salário maior que o teto da última faixa. Desconto: {DESCONTO_TETO}", DESCONTO_TETO);
                return DESCONTO_TETO;
            }

            return CalcularDescontoFaixas(salario);
        }

        private decimal CalcularDescontoFaixas(decimal salario)
        {
            var descontos = new List<decimal>();

            foreach (var item in faixas)
            {
                if (!item.ContemValor(salario))
                    break;

                decimal desconto = CalcularValorFaixa(salario, item);
                descontos.Add(desconto);

            }
            return descontos.Sum();
        }

        private decimal CalcularValorFaixa(decimal salario, INSSFaixa item)
        {
            var valorEfetivoFaixa = item.ObterValorFaixa(salario);
            var desconto = Math.Round(valorEfetivoFaixa * item.Aliquota / 100, 2);
            logger.LogInformation("Faixa: {item.Aliquota}\tDesconto: {desconto}", item.Aliquota, desconto);
            return desconto;
        }
    }
}
