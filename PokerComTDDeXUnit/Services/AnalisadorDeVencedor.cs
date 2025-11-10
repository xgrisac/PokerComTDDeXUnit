using PokerComTDDDomain.Entities;

namespace PokerComTDDDomain.Services
{
    public class AnalisadorDeVencedor // CLASSE RESPONSÁVEL POR ANALISAR AS CARTAS
    {
        // MÉTODO RESPONSÁVEL POR DEFINIR O VENCEDOR A PARTIR DA ANALISE
        public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
        {
            // PREMISSA PAR DE CARTAS IGUAIS

            var parDeCartasDoPrimeiroJogador = cartasDoPrimeiroJogador
                .Select(cartaStr => // Pega cada carta da lista
                {
                    var valor = cartaStr.Substring(0, cartaStr.Length - 1); // Pega o valor da carta (tudo menos o último caractere)
                    var naipe = cartaStr.Substring(cartaStr.Length - 1); // Pega somente o último caractere (naipe), para posteriormente validar se é válido
                    var cartaObj = new Carta(valor, naipe); // instancia a classe carta, passando o valor e o naipe
                    return cartaObj.Peso; // Retorna o peso da carta (valor convertido para int)
                })
                .GroupBy(valorDaCarta => valorDaCarta) // 'GroupBy' agrupa as cartas por valor, enquanto grupo.Count() >1, filtra o grupo que tem mais de 1 carta
                .Where(grupo => grupo.Count() > 1);

            var parDeCartasDoSegundoJogador = cartasDoSegundoJogador
                .Select(cartaStr =>
                {
                    var valor = cartaStr.Substring(0, cartaStr.Length - 1);
                    var naipe = cartaStr.Substring(cartaStr.Length - 1);
                    var cartaObj = new Carta(valor, naipe);
                    return cartaObj.Peso;
                })
                .GroupBy(valorDaCarta => valorDaCarta)
                .Where(grupo => grupo.Count() > 1);

            if (parDeCartasDoPrimeiroJogador != null && parDeCartasDoPrimeiroJogador.Any() && // Verifica se ambos os jogadores têm par de cartas, se sim, entra no IF
                parDeCartasDoSegundoJogador != null && parDeCartasDoSegundoJogador.Any())
            {
                var maiorParDeCartasDoPrimeiroJogador = parDeCartasDoPrimeiroJogador
                    .Select(valor => valor.Key).OrderBy(valor => valor).Max(); // Extrai a chave de cada par, ordena e pega o maior valor

                var maiorParDeCartasDoSegundoJogador = parDeCartasDoSegundoJogador
                    .Select(valor => valor.Key).OrderBy(valor => valor).Max();

                if (maiorParDeCartasDoPrimeiroJogador > maiorParDeCartasDoSegundoJogador)
                    return "Primeiro jogador";
                else if (maiorParDeCartasDoSegundoJogador > maiorParDeCartasDoPrimeiroJogador)
                    return "Segundo jogador";
            }

            // Caso apenas 1 dos jogadores tenha par de cartas, entra nesse else if

            else if (parDeCartasDoPrimeiroJogador != null && parDeCartasDoPrimeiroJogador.Any())
                return "Primeiro jogador";

            else if (parDeCartasDoSegundoJogador != null && parDeCartasDoSegundoJogador.Any())
                return "Segundo jogador";

            // PREMISSA MAIOR CARTA

            var maiorCartaDoPrimeiroJogador = cartasDoPrimeiroJogador
                .Select(cartaStr =>
                {
                    var valor = cartaStr.Substring(0, cartaStr.Length - 1);
                    var naipe = cartaStr.Substring(cartaStr.Length - 1);
                    var cartaObj = new Carta(valor, naipe);
                    return cartaObj.Peso;
                })
                .OrderBy(ValorDaCarta => ValorDaCarta)
                .Max();

            var maiorCartaDoSegundoJogador = cartasDoSegundoJogador
                .Select(cartaStr =>
                {
                    var valor = cartaStr.Substring(0, cartaStr.Length - 1);
                    var naipe = cartaStr.Substring(cartaStr.Length - 1);
                    var cartaObj = new Carta(valor, naipe);
                    return cartaObj.Peso;
                })
                .OrderBy(ValorDaCarta => ValorDaCarta)
                .Max();

            return maiorCartaDoPrimeiroJogador > maiorCartaDoSegundoJogador ? "Primeiro jogador" : "Segundo jogador"; // ? = Operador ternário (true/false)
        }

        // MÉTODO RESPONSÁVEL POR CONVERTER CARTAS ESPECIAIS EM VALORES INTEIROS
    }
}
