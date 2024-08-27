namespace Exemplo01
{
    /// <summary>
    /// Representa uma faixa de cálculo para contribuição ao INSS.
    /// Esta classe fornece os meios para determinar se um salário está dentro de uma faixa específica
    /// e calcular o valor de contribuição baseado na alíquota correspondente.
    /// </summary>
    public sealed class INSSFaixa
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="INSSFaixa"/> com os limites especificados e alíquota.
        /// </summary>
        /// <param name="piso">O valor mínimo do salário para esta faixa de contribuição.</param>
        /// <param name="teto">O valor máximo do salário para esta faixa de contribuição.</param>
        /// <param name="aliquota">A alíquota percentual aplicada dentro desta faixa.</param>
        /// <exception cref="ArgumentException">Lança exceção se o teto for menor que o piso ou se a alíquota for negativa.</exception>
        public INSSFaixa(decimal piso, decimal teto, decimal aliquota)
        {
            if (teto < piso)
                throw new ArgumentException("O teto deve ser maior que o piso.");
            if (aliquota < 0)
                throw new ArgumentException("A alíquota deve ser positiva.");

            Piso = piso;
            Teto = teto;
            Aliquota = aliquota;
        }

        /// <summary>
        /// Obtém o piso salarial para a faixa de contribuição.
        /// </summary>
        public decimal Piso { get; }

        /// <summary>
        /// Obtém o teto salarial para a faixa de contribuição.
        /// </summary>
        public decimal Teto { get; }

        /// <summary>
        /// Obtém a alíquota de contribuição para a faixa.
        /// </summary>
        public decimal Aliquota { get; }

        /// <summary>
        /// Determina se um dado valor salarial está contido dentro desta faixa de contribuição.
        /// </summary>
        /// <param name="valor">O valor do salário a ser verificado.</param>
        /// <returns>True se o valor está dentro da faixa, incluindo os limites; False caso contrário.</returns>
        public bool ContemValor(decimal valor)
        {
            if (valor > Teto)
                return true;

            return valor >= Piso;
        }

        /// <summary>
        /// Calcula o valor da contribuição para o INSS baseado no salário fornecido, 
        /// considerando que o salário esteja dentro desta faixa.
        /// </summary>
        /// <param name="salario">O salário do qual a contribuição é calculada.</param>
        /// <returns>O valor da contribuição de acordo com a alíquota desta faixa.</returns>
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
