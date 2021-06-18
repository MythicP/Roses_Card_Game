using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject yin;
    public GameObject yang;

    public Tile tile_yin;
    public Tile tile_yang;
    public Card commander_yin;
    public Card commander_yang;

    // Start is called before the first frame update
    void Start()
    {
        Card temp = Instantiate(commander_yin, tile_yin.transform.position, Quaternion.identity);
        Card temp2 = Instantiate(commander_yang, tile_yang.transform.position, Quaternion.identity);

        temp.transform.parent = yin.transform;
        temp2.transform.parent = yang.transform;

        temp.SetOwner(yin);
        temp2.SetOwner(yang);


        temp.SetTilePosition(tile_yin);
        temp2.SetTilePosition(tile_yang);

        tile_yin.SetUnit(temp);
        tile_yang.SetUnit(temp2);

        tile_yin.SetEdge(true);
        tile_yang.SetEdge(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnit()
    {

    }
}
