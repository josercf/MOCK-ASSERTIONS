using Exemplo01;

namespace Exemplo1.Teste
{
    public class InssTeste
    {
        [Fact]
        public void Calcular_Desconto_Faixa1()
        {
            var inss = new Inss();

            //1412.00 - 7.5% = 105.9
            var desconto = inss.CalcularDesconto(1412);

            Assert.Equal(105.9m, desconto);
        }

        [Fact]
        public void Calcular_Desconto_Faixa2()
        {
            const decimal salario = 2100.00m;
            const decimal descontoEsperado = 167.82m;
            var inss = new Inss();

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
            var inss = new Inss();

            var desconto = inss.CalcularDesconto(salario);

            Assert.Equal(descontoEsperado, desconto);
        }
    }
}
