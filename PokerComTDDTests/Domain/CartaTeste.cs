using System.Data;
using PokerComTDDDomain.Entities;


    namespace PokerComTDDTests.Domain
    {
        public class CartaTeste // Classe responsável por testar a criação e validação de carta e naipe
        {
            [Fact]
            public void DeveCriarUmaCarta()
            {
                // Arrange
                const string valorEsperado = "A";
                const string naipeEsperado = "O";

                // Act
                var carta = new Carta(valorEsperado, naipeEsperado); // Valores passados para o construtor

                // Assert
                Assert.Equal(valorEsperado, carta.Valor);
                Assert.Equal(naipeEsperado, carta.Naipe);
            }

            [Theory]
            [InlineData("V", 11)]
            [InlineData("D", 12)]
            [InlineData("R", 13)]
            [InlineData("A", 14)]
            public void DeveCriarUmaCartaComPeso(string valorDaCarta, int pesoEsperado)
            {
                // Act
                var carta = new Carta(valorDaCarta, naipe: "E");

                // Assert
                Assert.Equal(pesoEsperado, carta.Peso);
            }

            [Theory]
            [InlineData("Z")]
            [InlineData("1")]
            [InlineData("15")]
            [InlineData("-1")]
            public void DeveValidarValorDaCarta(string valorDaCartaInvalido)
            { // Arrange & Act & Assert - Tudo em um só passo
              // Testa cenário de falha, caso carta criada o teste falha, caso nao criada lança a exception e o teste passa

                var mensagemDeErro =
                    Assert.Throws<Exception>(() => new Carta(valorDaCartaInvalido, naipe: "O")).Message;
                Assert.Equal("Valor da carta inválido.", mensagemDeErro);

            } // var mensagemDeErro =  recebe a mensagem de erro que é lançada pela exception, junto com o .Message
              // Assert.Throws<Exception> espera um erro, e lança uma exception
              // ()=> new Carta(... = é a ação que esperamos que estamos testando, ou seja, criar uma carta com valor inválido
              // Assert.Equal compara a mensagem esperada com a mensagem real

            [Theory]
            [InlineData("A")]
            [InlineData("Z")]
            public void DeveValidarNaipeDaCarta(string naipeDaCartaInvalido)
            {

                var mensagemDeErro =
                    Assert.Throws<Exception>(() => new Carta(valor: "2", naipe: naipeDaCartaInvalido)).Message;
                Assert.Equal("Naipe da carta inválido.", mensagemDeErro);
            }
        }
    }
