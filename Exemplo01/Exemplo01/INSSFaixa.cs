namespace Exemplo01
{
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
        public INSSFaixa(decimal piso, decimal teto, decimal aliquota)
        {
            Piso = piso;
            Teto = teto;
            Aliquota = aliquota;
        }

        public decimal Piso { get; }
        public decimal Teto { get; }
        public decimal Aliquota { get; }

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
}
