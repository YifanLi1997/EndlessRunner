using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;
    [SerializeField] Transform player;
    [SerializeField] float lengthOfPerTile = 20;
    [SerializeField] int numberOfInitialTiles = 5;

    private int _tileCount = 0;
    
    void Start()
    {
        // the first one will always be tile 0
        SpawnTile(0);

        for (int i = 0; i < numberOfInitialTiles - 1; i++)
        {
            SpawnTile(UnityEngine.Random.Range(0, tiles.Length));
        }

    }

    void Update()
    {
        // there will always be (numberOfInitialTiles-1) tiles before the player
        if (player.position.z > (_tileCount - numberOfInitialTiles + 1) * lengthOfPerTile)
        {
            SpawnTile(UnityEngine.Random.Range(0, tiles.Length));
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject newTile = Instantiate(tiles[tileIndex], Vector3.forward * _tileCount * lengthOfPerTile, Quaternion.identity);
        _tileCount++;
    }
}
