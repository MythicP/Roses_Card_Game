using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    private List<GameObject> tiles;
    private IEnumerator<GameObject> list;

    private GameObject[,] tiles_arr;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            tiles.Add(transform.GetChild(i).gameObject);
        }

        list = tiles.GetEnumerator();

        //////////////////////////////////////////////////
        tiles_arr = new GameObject[8, 10];

        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                tiles_arr[i, j] = transform.GetChild((i) + (j * 7) + j).gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void reset_list()
    {
        list.Reset();
    }

    public (int, int) FindTile(GameObject til)
    {

        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                if (tiles_arr[i, j] == til)
                {
                    //til.GetComponent<GridSelector>().SetColour();
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    public List<GridSelector> FindNearbyTiles(GameObject til, int distance)
    {
        List<GridSelector> list = new List<GridSelector>();

        (int, int) val = FindTile(til);

        int val_i = val.Item1;
        int val_j = val.Item2;

        int value = 1 + (distance * 2);

        int left_value = val_i - distance;

        int loop_No = 1;

        //tiles_arr[val_i - distance, val_j].GetComponent<GridSelector>().SetColour();

        til.GetComponent<GridSelector>().SetColour(6, 1);

        if (val_i != -1)
        {

            //first row
            for (int i = left_value; i < left_value + value; i++)
            {
                if(0 <= i && i < 8)
                {
                    if(tiles_arr[i, val_j].GetComponent<GridSelector>().GetCharacter() == null)
                    {
                        tiles_arr[i, val_j].GetComponent<GridSelector>().SetColour(1, 1); //Mathf.Abs(val_i - i)
                        list.Add(tiles_arr[i, val_j].GetComponent<GridSelector>());
                    }    
                }
            }

            value -= 1;

            //rows after first
            while (distance > 0)
            {
                if (val_j % 2 == 1 && loop_No % 2 == 1) // odd
                {
                    left_value += 1;
                }

                if (val_j % 2 == 0 && loop_No % 2 == 0) // even
                {
                    left_value += 1;
                }

                //Above
                if (val_j + loop_No < 10)
                {
                    for (int i = left_value; i < left_value + value; i++)
                    {
                        if (0 <= i && i < 8)
                        {
                            if (tiles_arr[i, val_j + loop_No].GetComponent<GridSelector>().GetCharacter() == null)
                            { 
                                tiles_arr[i, val_j + loop_No].GetComponent<GridSelector>().SetColour(1, 1); //loopNo
                                list.Add(tiles_arr[i, val_j + loop_No].GetComponent<GridSelector>());
                            }
                        }                            
                    }
                }

                //Below
                if (val_j - loop_No >= 0)
                {
                    for (int i = left_value; i < left_value + value; i++)
                    {
                        if (0 <= i && i < 8)
                        {
                            if (tiles_arr[i, val_j - loop_No].GetComponent<GridSelector>().GetCharacter() == null)
                            { 
                                tiles_arr[i, val_j - loop_No].GetComponent<GridSelector>().SetColour(1, 1);//loopNo
                                list.Add(tiles_arr[i, val_j - loop_No].GetComponent<GridSelector>());
                            }
                        }                            
                    }
                }

                loop_No += 1;
                distance -= 1;
                value -= 1;
            }

            return list;
        }
        return null;
    }
}
