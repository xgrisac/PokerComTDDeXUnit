namespace PokerComTDDDomain.Entities
{
    public class Carta
    {
        // CTOR
        public Carta(string valor, string naipe) // Estes parâmetros são na verdade os valores do Arrange
        {

            if (naipe != "O" && naipe != "C" && naipe != "E" && naipe != "P")
                throw new Exception("Naipe da carta inválido.");

            Valor = valor; // A propriedade Valor, recebe o valor passado no construtor
            Naipe = naipe; // A propriedade Naipe, recebe o naipe passado no construtor
            ConverterParaPeso(valor); // Chama o método para converter o valor da carta em peso

            if (Peso < 2 || Peso > 14)
                throw new Exception("Valor da carta inválido."); // Trata exceção para caso o valor da carta seja inválido
        }

        // PROPRIEDADES
        public string Valor { get; internal set; }
        public int Peso { get; internal set; }
        public string Naipe { get; internal set; }

        public void ConverterParaPeso(string valorDaCarta)
        {
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

            Peso = valor; // Retorna que volta para o método Analisar

        }

    }
}
