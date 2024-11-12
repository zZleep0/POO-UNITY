using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // Importante para usar a UI

public class GameManager : MonoBehaviour
{

    public Deck deck;  // Referência ao baralho
    public string player1Name = "Jogador 1";  // Nome do primeiro jogador
    public string player2Name = "Jogador 2";  // Nome do segundo jogador
    private Player player1;  // Instância do primeiro jogador
    private Player player2;  // Instância do segundo jogador

    public GameObject player1Object;  // Referência ao Jogador 1
    public GameObject player2Object;  // Referência ao Jogador 2

    public GameObject cardPrefab;  // Prefab para as cartas
    public GameObject cardTextPrefab;  // Prefab for the text

    List<Card> cartasDoJogador1;
    List<Card> cartasDoJogador2;

    

    // Método chamado ao iniciar o jogo
    void Start()
    {

        // Verifique se o deck foi atribuído
        if (deck == null)
        {
            Debug.LogError("Deck is not assigned in the inspector!");
            return; // Saia do método se o deck não estiver atribuído
        }


        // Cria instâncias dos jogadores
        player1 = new Player(player1Name);
        player2 = new Player(player2Name);
        StartGame();  // Chama o método para iniciar o jogo
    }

    // Método que inicializa o jogo
    void StartGame()
    {
        //TestLoadJson();
       // Carrega cartas do arquivo JSON
        deck.LoadCards(Resources.Load<TextAsset>("deck"));// Verifique se o nome do arquivo está correto
        Debug.Log(deck.cards.Count); // Verifica quantas cartas foram carregadas

        // Certifique - se de que cartas foram carregadas
        if (deck.cards.Count == 0)
        {
            Debug.LogError("No cards available in the deck. Check the JSON file.");
            return; // Saia do método se não houver cartas
        }

        // Distribui 3 cartas para o jogador 1
        List<Card> cardsPlayer1 = deck.Distribute(3);
        player1.GetCards(cardsPlayer1);
        Debug.Log($"{player1.name}");
        Debug.Log($"{player1.name} recebeu: {string.Join(", ", cardsPlayer1.ConvertAll(c => c.DisplayCardInfo()))}");
        cartasDoJogador1 = cardsPlayer1;

        // Distribui 3 cartas para o jogador 2
        List<Card> cardsPlayer2 = deck.Distribute(3);
        player2.GetCards(cardsPlayer2);
        Debug.Log($"{player2.name} recebeu: {string.Join(", ", cardsPlayer2.ConvertAll(c => c.DisplayCardInfo()))}");
        cartasDoJogador2 = cardsPlayer2;

     
        // Exibir as cartas na cena
        ShowCards(cardsPlayer1, player1Object.transform.position);
        ShowCards(cardsPlayer2, player2Object.transform.position);
    }

    void ShowCards(List<Card> cards, Vector3 playerPosition)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            // Instanciar a carta na posição do jogador
            GameObject cardObj = Instantiate(cardPrefab, playerPosition + new Vector3(0.5f + i * 1.2f, 0,  1f), Quaternion.identity);
            cardObj.name = cards[i].DisplayCardInfo();  // Nome da carta

            Renderer material = cardObj.GetComponent<Renderer>();

            //Trocar cor com base no naipe
            if (cardObj.name.Contains("Paus"))
            {
                material.material.color = Color.blue;
            }
            else if (cardObj.name.Contains("Ouros"))
            {
                material.material.color = Color.yellow;
            }
            else if (cardObj.name.Contains("Espadas"))
            {
                material.material.color = Color.green;
            }
            else if (cardObj.name.Contains("Copas"))
            {
                material.material.color = Color.red;
            }
            else
            {
                Debug.Log("Algo deu errado na cor");
            }
            // (Opcional) Adicionar texto ou material para representar a carta
            //cardObj.GetComponent<Renderer>().material.color = Color.red; // Exemplo de cor
            

            // Instantiate the text for the card
            GameObject textObj = Instantiate(cardTextPrefab, cardObj.transform);
            //textObj.GetComponent<Text>().text = $"{cards[i].suit} {cards[i].value}"; // Atualiza com o card's suit e value
            //textObj.transform.localPosition = new Vector3(0, 0.1f, 0); // Adjust position above card
        }
    }

    void TestLoadJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("deck");
        if (jsonFile != null)
        {
            Debug.Log(jsonFile.text);  // Mostra o conteúdo do JSON
            CardsData data = JsonUtility.FromJson<CardsData>(jsonFile.text);
            if (data.cards != null)
            {
                Debug.Log($"Cards loaded: {data.cards.Count}");
                foreach (var card in data.cards)
                {
                    Debug.Log($"{card.value} de {card.suit}");
                }
            }
            else
            {
                Debug.LogError("Cards are null after deserialization.");
            }
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetOponentCard(cartasDoJogador2, player1, cartasDoJogador1, player2);
        }
        

        
        
    }

    void GetOponentCard(List<Card> playerCard1, Player player1, List<Card> playerCard2, Player player2)
    {
        //CARTAS PLAYER 1

        // Pega a primeira carta do oponente (ou uma carta aleatória)
        Card takenCard1 = playerCard1[Random.Range(0, playerCard1.Count)]; // Aqui você pode implementar lógica para escolher uma carta específica

        // Remove a carta do oponente
        playerCard1.Remove(takenCard1);

        // Adiciona a carta ao jogador atual
        //currentPlayer.GetCards(new List<Card> { takenCard });

        Debug.Log($"{player1.name} pegou a carta: {takenCard1.DisplayCardInfo()} do oponente.");


        //CARTAS PLAYER 2

        // Pega a primeira carta do oponente (ou uma carta aleatória)
        Card takenCard2 = playerCard2[Random.Range(0, playerCard2.Count)]; // Aqui você pode implementar lógica para escolher uma carta específica

        // Remove a carta do oponente
        playerCard2.Remove(takenCard2);

        // Adiciona a carta ao jogador atual
        //currentPlayer.GetCards(new List<Card> { takenCard });

        Debug.Log($"{player2.name} pegou a carta: {takenCard2.DisplayCardInfo()} do oponente.");


        //Verificar se nao tem cartas
        if (playerCard1.Count == 0 || playerCard2.Count == 0)
        {
            Debug.Log("O oponente não possui cartas para pegar.");
        }

        CheckCardWinner(takenCard1, takenCard2);

    }

    void CheckCardWinner(Card player1Card, Card player2Card)
    {
        int result = GameRules.CompareCards(player1Card, player2Card);

        if (result == 1)
        {
            player1Object.GetComponent<Renderer>().material.color = Color.green;
            player2Object.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log($"{player1.name} venceu com a carta: {player1Card.DisplayCardInfo()}");
            
        }
        else if (result == -1)
        {
            player2Object.GetComponent<Renderer>().material.color = Color.green;
            player1Object.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log($"{player2.name} venceu com a carta: {player2Card.DisplayCardInfo()}");
            
        }
        else
        {
            Debug.Log("Empate!");
        }
    }

}
