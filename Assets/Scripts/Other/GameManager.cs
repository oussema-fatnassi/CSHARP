using UnityEngine;

/*
    This class is responsible for managing the game state.
    It caches the initial values of the player stats and resets them when the game is closed.
*/

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStats[] playerStatsArray;

    private void Start()
    {
        foreach (var playerStats in playerStatsArray)
        {
            playerStats.CacheInitialValues(); 
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var playerStats in playerStatsArray)
        {
            playerStats.ResetToInitialValues();
        }
    }
}
