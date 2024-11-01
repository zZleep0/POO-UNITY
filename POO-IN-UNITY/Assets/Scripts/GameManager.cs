using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // Importante para usar a UI

public class GameManager : MonoBehaviour
{
    public Deck deck;  // Refer�ncia ao baralho
    public string player1Name = "Jogador 1";  // Nome do primeiro jogador
    public string player2Name = "Jogador 2";  // Nome do segundo jogador
    private Player player1;  // Inst�ncia do primeiro jogador
    private Player player2;  // Inst�ncia do segundo jogador

    public GameObject player1Object;  // Refer�ncia ao Jogador 1
    public GameObject player2Object;  // Refer�ncia ao Jogador 2

    public GameObject cardPrefab;  // Prefab para as cartas
    public GameObject cardTextPrefab;  // Prefab for the text

    List<Card> cartasDoJogador1;
    List<Card> cartasDoJogador2;

    // M�todo chamado ao iniciar o jogo
    void Start()
    {

        // Verifique se o deck foi atribu�do
        if (deck == null)
        {
            Debug.LogError("Deck is not assigned in the inspector!");
            return; // Saia do m�todo se o deck n�o estiver atribu�do
        }


        // Cria inst�ncias dos jogadores
        player1 = new Player(player1Name);
        player2 = new Player(player2Name);
        StartGame();  // Chama o m�todo para iniciar o jogo
    }

    // M�todo que inicializa o jogo
    void StartGame()
    {
        //TestLoadJson();
       // Carrega cartas do arquivo JSON
        deck.LoadCards(Resources.Load<TextAsset>("deck"));// Verifique se o nome do arquivo est� correto
        Debug.Log(deck.cards.Count); // Verifica quantas cartas foram carregadas

        // Certifique - se de que cartas foram carregadas
        if (deck.cards.Count == 0)
        {
            Debug.LogError("No cards available in the deck. Check the JSON file.");
            return; // Saia do m�todo se n�o houver cartas
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
            // Instanciar a carta na posi��o do jogador
            GameObject cardObj = Instantiate(cardPrefab, playerPosition + new Vector3(0.5f + i * 1.2f, 0,  1f), Quaternion.identity);
            cardObj.name = cards[i].DisplayCardInfo();  // Nome da carta

            // (Opcional) Adicionar texto ou material para representar a carta
            cardObj.GetComponent<Renderer>().material.color = Color.red; // Exemplo de cor

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
            Debug.Log(jsonFile.text);  // Mostra o conte�do do JSON
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
        GetOponentCard(cartasDoJogador1);
    }

    void GetOponentCard(List<Card> cartas)
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            
            Debug.Log(cartas.Count);
        }
        
    }
}
