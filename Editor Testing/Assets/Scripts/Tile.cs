using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer material;

    public bool currentUsed = false;
    public bool available = false;
    public bool target = false;
    public bool blocked = false;
    public bool walkable = true;

    public List<Tile> adjacentTiles = new List<Tile>();

    // BFS Pending

    private void Start()
    {
        material = GetComponent<Renderer>();
    }

    private void Update()
    {
        SetTileStatus();
    }

    public void SetTileStatus()
    {
        if(currentUsed)
        {
            material.material.color = Color.magenta;
        }
        else if (target)
        {
            material.material.color = Color.green;
        }
        else if (available)
        {
            material.material.color = Color.blue;
        }
        
        else if (blocked)
        {
            material.material.color = Color.red;
        }
        else
        {
            material.material.color = Color.white;
        }
    }

    public void ResetTile()
    {
        currentUsed = false;
        available = false;
        target = false;
        blocked = false;
        walkable = true;
    }

    public void SetAdjacentTiles(float JumpHeight)
    {
        ResetTile();

        IdentifyTiles(Vector3.forward, JumpHeight);
        IdentifyTiles(Vector3.right, JumpHeight);
        IdentifyTiles(Vector3.left, JumpHeight);
        IdentifyTiles(Vector3.back, JumpHeight);
    }

    public void IdentifyTiles(Vector3 distance, float JumpHeight)
    {
        Vector3 halfextents = new Vector3(0.25f, (1f + JumpHeight) / 2f, 0.25f);
        Collider[] adjacentCollider = Physics.OverlapBox(transform.position + distance, halfextents);

        foreach(Collider collider in adjacentCollider)
        {
            Tile tile = collider.GetComponent<Tile>();
            if(tile != null && walkable)
            {
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out RaycastHit hitInfo, 1f))
                {
                    adjacentTiles.Add(tile);
                }
            }
        }
    }

    public void SetCurrentTile()
    {
        currentUsed = true;
    }
}
