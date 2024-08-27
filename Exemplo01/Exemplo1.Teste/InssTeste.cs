using Exemplo01;

using Microsoft.Extensions.Logging;

using Moq;

namespace Exemplo1.Teste
{
    public class InssTeste
    {
        IEnumerable<INSSFaixa> faixas = new List<INSSFaixa>
              {
                  new INSSFaixa(0,1412, 7.5m ),
                  new INSSFaixa(1412.01m, 2666.68m, 9m ),
                  new INSSFaixa(2666.69m, 4000.03m, 12m ),
                  new INSSFaixa(4000.04m, 7786.02m, 14m )
              };

        ILogger<Inss> logger = new Mock<ILogger<Inss>>().Object;


        [Fact]
        public void Calcular_Desconto_Faixa1()
        {
            var inss = new Inss(logger, faixas);

            //1412.00 - 7.5% = 105.9
            var desconto = inss.CalcularDesconto(1412);

            Assert.Equal(105.9m, desconto);
        }

        [Fact]
        public void Calcular_Desconto_Faixa2()
        {
            const decimal salario = 2100.00m;
            const decimal descontoEsperado = 167.82m;
            var inss = new Inss(logger, faixas);

            //1412.00 * 7.5% = 105.9
            //[2100.00 - 1412] * 0,09 = 688,00 * 0,09 = 61,92
            //105,9 + 61,92 = 167,82
            var desconto = inss.CalcularDesconto(salario);

            Assert.Equal(descontoEsperado, desconto);
        }

        [Fact]
        public void Deve_Retornar_Desconto_Maximo()
        {
            const decimal salario = 8000.00m;
            const decimal descontoEsperado = 908.85m;
            var inss = new Inss(logger, faixas);

            var desconto = inss.CalcularDesconto(salario);

            Assert.Equal(descontoEsperado, desconto);
        }

        [Fact]
        public void Deve_Retornar_Desconto_258_81()
        {
            const decimal salario = 3000.00m;
            const decimal descontoEsperado = 258.82m;
            var inss = new Inss(logger, faixas);

            var desconto = inss.CalcularDesconto(salario);

            Assert.Equal(descontoEsperado, desconto);
        }

        [Fact]
        public void CalcularDesconto_SalarioZero_DeveLancarArgumentException()
        {
            decimal salarioInvalido = 0;

            var inss = new Inss(logger, faixas);

            var exception = Assert.Throws<ArgumentException>(() => inss.CalcularDesconto(salarioInvalido));

            // Verificar se a mensagem de exceção está correta
            Assert.Equal("O salário deve ser maior que zero. (Parameter 'salario')", exception.Message);
            Assert.Equal("salario", exception.ParamName);
        }
    }
}
