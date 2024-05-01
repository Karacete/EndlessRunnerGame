using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagmentScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilesPrefabs;
    private float zSpawn;
    private float tileLength;
    private Transform playerTransform;
    private float numberofTiles;
    private List<GameObject> activeTiles = new List<GameObject>();
    void Start()
    {
        zSpawn = 0;
        tileLength = 99.7f;
        playerTransform = GameObject.FindWithTag("Player").transform;
        numberofTiles = 3;
        for (int i = 0; i < numberofTiles; i++)
        {
            TileSpawn(Random.Range(0, tilesPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 100 > zSpawn - (numberofTiles * tileLength))
        {
            TileSpawn(Random.Range(0, tilesPrefabs.Length));
            DeleteTile();
        }
    }
    private void TileSpawn(int tileIndex) 
    {
        GameObject go = Instantiate(tilesPrefabs[tileIndex], transform.up * -zSpawn, transform.rotation);
        Debug.Log("uretilen" + go.name);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    private void DeleteTile()
    {
        Debug.Log("silinen" + activeTiles[0].name);
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
