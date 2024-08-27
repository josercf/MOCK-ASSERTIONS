using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exemplo01;

namespace Exemplo1.Teste
{
    public class INSSFaixaTeste
    {
        public static IEnumerable<object[]> DadosForaFaixa => new List<object[]>
        {
            new object[] { 900, false },
            new object[] { 2100, true }  ,
            new object[] { 2700, true }
        };

        [Fact]
        public void INSS_Faixa_Deve_Conter_Valor()
        {
            var faixa = new INSSFaixa(1000, 2000, 14m);

            Assert.True(faixa.ContemValor(1500));
        }

        [Theory]
        [MemberData(nameof(DadosForaFaixa))]
        public void INSS_Faixa_Nao_Deve_Conter_Valor(int valor, bool expected)
        {
            var faixa = new INSSFaixa(1412.01m, 2666.68m, 14m);

            var result = faixa.ContemValor(valor);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void INSS_Faixa_Deve_Obter_1412()
        {
            var faixa = new INSSFaixa(0, 1412.01m, 7.5m);

            var result = faixa.ObterValorFaixa(1412m);

            const decimal expected = 1412m;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void INSS_Faixa_Deve_Obter_688()
        {
            var faixa = new INSSFaixa(1412.01m, 2666.68m, 9m);

            var result = faixa.ObterValorFaixa(2100m);

            const decimal expected = 688m;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void INSS_Faixa_Deve_Obter_1254_68()
        {
            var faixa = new INSSFaixa(1412.01m, 2666.68m, 9m);
           
            var result = faixa.ObterValorFaixa(2_700m);

            const decimal expected = 1_254.68m;
            Assert.Equal(expected, result);
        }
    }
}
