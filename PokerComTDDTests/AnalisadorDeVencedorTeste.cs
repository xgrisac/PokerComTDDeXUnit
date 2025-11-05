namespace PokerComTDDTests

{
    public class AnalisadorDeVencedorTeste // CLASSE RESPONSÁVEL PELOS TESTES PRINCIPAIS

    {
        [Theory] // Parâmetros a serem enviados
        [InlineData("20,4C,3P,6C,7C", "30,5C,2E,9C,7P", "Segundo jogador")] // Maior carta 9C
        [InlineData("30,5C,2E,9C,7P", "20,4C,3P,6C,7C", "Primeiro jogador")] // Maior carta 9C
        [InlineData("30,5C,2E,9C,7P", "20,4C,3P,6C,10E", "Segundo jogador")] // Maior carta 10E
        [InlineData("30,5C,2E,9C,VP", "20,4C,3P,6C,AE", "Segundo jogador")] // Maior carta AE
        public void DeveAnalisarVencedorQuandoTiverMaiorCarta(string cartasDoPrimeiroJogadorString, string cartasDoSegundoJogadorString, string vencedorEsperado)
        {
            // Arrange
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorString.Split(',').ToList(); // Converte as strings em listas
            var cartasDoSegundoJogador = cartasDoSegundoJogadorString.Split(',').ToList();
            var analisador = new AnalisadorDeVencedor();

            // Act
            var vencedor = analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            // Assert
            Assert.Equal(vencedorEsperado, vencedor);
        }

        internal class AnalisadorDeVencedor
        {
            // MÉTODO RESPONSÁVEL POR ANALISAR E DEFINIR VENCEDOR
            public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
            {
                var maiorCartaDoPrimeiroJogador = cartasDoPrimeiroJogador
                    .Select(carta => ConverterParaValorDaCarta(carta)) // 'Select' pega cada ítem da lista. '=>' Lambda indica o que fazer com cada ítem.
                    .OrderBy(ValorDaCarta => ValorDaCarta)
                    .Max();

                var maiorCartaDoSegundoJogador = cartasDoSegundoJogador
                    .Select(carta => ConverterParaValorDaCarta(carta))
                    .OrderBy(ValorDaCarta => ValorDaCarta)
                    .Max();

                return maiorCartaDoPrimeiroJogador > maiorCartaDoSegundoJogador ? "Primeiro jogador" : "Segundo jogador"; // ? = Operador ternário (true/false)
            }

            // MÉTODO RESPONSÁVEL POR CONVERTER CARTAS ESPECIAIS EM VALORES INTEIROS
            private int ConverterParaValorDaCarta(string carta)
            {
                var valorDaCarta = carta.Substring(0, carta.Length - 1); //  'carta.Substring(O, carta.Length -1)' pega todos os ítens da string, exceto o último.

                if (!int.TryParse(valorDaCarta, out var valor)) // 'int.Parse()' tenta transformar o dado em int, se conseguir coloca o dado na variável valor, caso contrário (!), segue para o Switch
                {
                    switch (valorDaCarta)
                    {
                        // ATRIBUIÇÃO DE VALORES INT PARA AS CARTAS
                        case "V":
                            valor = 11;
                            break;
                        case "D":
                            valor = 12;
                            break;
                        case "R":
                            valor = 13;
                            break;
                        case "A":
                            valor = 14;
                            break;
                    }
                }

                return valor; // Retorna que volta para o método Analisar

            }
        }
    }
}