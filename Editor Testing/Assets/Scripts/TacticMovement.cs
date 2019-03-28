using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticMovement : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void SetTileUsed()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycast, 1f))
        {
            Tile tile = raycast.transform.GetComponent<Tile>();
            if (tile != null)
            {
                tile.SetCurrentTile();
            }
        }
    }
}
