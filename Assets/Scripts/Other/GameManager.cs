using UnityEngine;

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
