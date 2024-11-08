using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{

    // Dicionário para definir a hierarquia de valores
    private static Dictionary<string, int> cardHierarchy = new Dictionary<string, int>
    {
        { "2", 2 },
        { "3", 3 },
        { "4", 4 },
        { "5", 5 },
        { "6", 6 },
        { "7", 7 },
        { "8", 8 },
        { "9", 9 },
        { "10", 10 },
        { "Valete", 11 },
        { "Dama", 12 },
        { "Rei", 13 },
        { "As", 14 }
    };


    // Método para comparar duas cartas e retornar o vencedor
    public static int CompareCards(Card card1, Card card2)
    {
        if (!cardHierarchy.ContainsKey(card1.value) || !cardHierarchy.ContainsKey(card2.value))
        {
            Debug.LogError("Valor de carta desconhecido!");
            return 0; // Retorna 0 em caso de erro
        }

        int card1Value = cardHierarchy[card1.value];
        int card2Value = cardHierarchy[card2.value];

        // Compara os valores das cartas
        if (card1Value > card2Value)
        {
            return 1; // card1 é maior
        }
        else if (card1Value < card2Value)
        {
            return -1; // card2 é maior
        }
        else
        {
            return 0; // Empate
        }
    }

    

}
