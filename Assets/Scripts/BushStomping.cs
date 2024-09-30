using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BushStomping : MonoBehaviour
{
    #region Inspector

    [SerializeField] private TileBase stompedBushTile; // Tile to replace the bush when stomped
    [SerializeField] private Tilemap bushTilemap;      // Reference to the Tilemap with bushes

    #endregion

    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the bush"); 
            // Get the position of the collision in world space
            Vector3 playerPosition = other.transform.position;

            // Convert the player's world position to the tilemap's grid position
            Vector3Int tilePosition = bushTilemap.WorldToCell(playerPosition);

            // Get the tile at the player's position
            TileBase currentTile = bushTilemap.GetTile(tilePosition);

            // If the current tile is a bush (you can add further checks here)
            if (currentTile != null) // Check if a tile exists at this position
            {
                // Replace the tile with the stomped version
                bushTilemap.SetTile(tilePosition, stompedBushTile);
            }
        }
    }

    #endregion
}
