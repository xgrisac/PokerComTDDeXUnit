using PokerComTDDDomain.Services;

namespace PokerComTDDTests.Domain
{
    public class AnalisadorDeVencedorTeste // CLASSE RESPONSÁVEL PELOS TESTES PRINCIPAIS
    {
        private readonly AnalisadorDeVencedor _analisador; // Tranca a variável para que ela não seja alterada fora do construtor / SOMENTE LEITURA
        public AnalisadorDeVencedorTeste() // SETUP - Prepara o ambiente antes da execução dos testes
        {
            _analisador = new AnalisadorDeVencedor();
        }

        [Theory]
        [InlineData("2O,4C,3P,6C,7C", "3O,5C,2E,9C,7P", "Segundo jogador")] // Maior carta 9C
        [InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,7C", "Primeiro jogador")] // Maior carta 9C
        [InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,10E", "Segundo jogador")] // Maior carta 10E
        [InlineData("3O,5C,2E,9C,VP", "2O,4C,3P,6C,AE", "Segundo jogador")] // Maior carta AE
        public void DeveAnalisarVencedorQuandoTiverMaiorCarta(string cartasSortesadasDoPrimeiroJogador, string cartasSortesadasDoSegundoJogador, string vencedorEsperado)
        {
            // Arrange
            var cartasDoPrimeiroJogador = cartasSortesadasDoPrimeiroJogador.Split(',').ToList(); // Converte as strings em listas
            var cartasDoSegundoJogador = cartasSortesadasDoSegundoJogador.Split(',').ToList();

            // Act
            var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            // Assert
            Assert.Equal(vencedorEsperado, vencedor);
        }

        [Theory]
        [InlineData("2O,2C,3P,6C,7C", "3O,5C,2E,9C,7P", "Primeiro jogador")] // Único par de cartas 20, 2c
        [InlineData("3O,5C,2E,9C,7P", "2O,2C,3P,6C,7C", "Segundo jogador")] // Único par de cartas 20, 2C
        [InlineData("DO,DC,2E,9C,7P", "2O,2C,3P,6C,7C", "Primeiro jogador")] // Maior par de cartas DO, DC
        [InlineData("2O,2C,3P,6C,7C", "DO,DC,2E,9C,7P", "Segundo jogador")]  // Maior par de cartas DO, DC    
        public void DeveAnalisarVencedorQuandoTiverUmParDeCartasDoMesmoValor(string cartasSortesadasDoPrimeiroJogador, string cartasSortesadasDoSegundoJogador, string vencedorEsperado)
        {
            // Arrange
            var cartasDoPrimeiroJogador = cartasSortesadasDoPrimeiroJogador.Split(',').ToList();
            var cartasDoSegundoJogador = cartasSortesadasDoSegundoJogador.Split(',').ToList();

            // Act
            var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            // Assert
            Assert.Equal(vencedorEsperado, vencedor);
        }

        [Theory]
        [InlineData("3O,5C,RE,AC,RP", "RO,2C,3P,RC,7C", "Primeiro jogador")] // Pares iguais R0 e RC vs RE e RP, maior carta fora dos pares AC
        [InlineData("RO,2C,3P,RC,7C", "3O,5C,RE,AC,RP", "Segundo jogador")] // Pares iguais R0 e RC vs RE e RP, maior carta fora dos pares AC
        public void DeveAnalisarVencedorQuandoDoisJogadoresEstaoEmpatadosEmParEhVencedorOQueTemAMaiorCarta(string cartasSortesadasDoPrimeiroJogador, string cartasSortesadasDoSegundoJogador, string vencedorEsperado)
        {
            // Arrange
            var cartasDoPrimeiroJogador = cartasSortesadasDoPrimeiroJogador.Split(',').ToList();
            var cartasDoSegundoJogador = cartasSortesadasDoSegundoJogador.Split(',').ToList();

            // Act
            var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            // Assert
            Assert.Equal(vencedorEsperado, vencedor);
        }


    }
}