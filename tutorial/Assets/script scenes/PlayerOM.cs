using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOM : MonoBehaviour
{
    // Estrutura estática para armazenar os pontos dos jogadores
    public static Dictionary<int, int> StarCounts = new Dictionary<int, int>();

    // Evento estático (Pattern Observer) notificando (playerID, novaQuantidade)
    public static event Action<int, int> OnStarCollected;

    public static void ResetScores()
    {
        StarCounts[1] = 0;
        StarCounts[2] = 0;
    }

    public static void AddStar(int playerID, int amount = 1)
    {
        if (!StarCounts.ContainsKey(playerID))
        {
            StarCounts[playerID] = 0;
        }

        StarCounts[playerID] += amount;

        // Notifica a UI e outros observadores
        OnStarCollected?.Invoke(playerID, StarCounts[playerID]);
    }

    public static int GetStars(int playerID)
    {
        return StarCounts.ContainsKey(playerID) ? StarCounts[playerID] : 0;
    }
}