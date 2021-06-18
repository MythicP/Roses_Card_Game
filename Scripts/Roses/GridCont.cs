using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCont : MonoBehaviour
{
    public GameObject terrain;
    
    private Tile[,] tiles_arr;

    public (int, int) gridSize = (9, 9);


    void Start()
    {
        tiles_arr = new Tile[gridSize.Item1, gridSize.Item2];

        for (int j = 0; j < gridSize.Item1; j++)
        {
            for (int i = 0; i < gridSize.Item2; i++)
            {
                tiles_arr[i, j] = transform.GetChild((i) + (j * (gridSize.Item2 - 1)) + j).gameObject.GetComponent<Tile>();
            }
        }

        for (int j = 0; j < gridSize.Item1; j++)
        {
            for (int i = 0; i < gridSize.Item2; i++)
            {
                tiles_arr[i, j].SetTerrain(terrain.transform.GetChild((i) + (j * (gridSize.Item2 - 1)) + j).gameObject.GetComponent<TerrainTile>());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public (int, int) FindTile(Tile til)
    {

        for (int j = 0; j < gridSize.Item1; j++)
        {
            for (int i = 0; i < gridSize.Item2; i++)
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

    public Tile GetNextTile(Tile tile, (int,int) mov)
    {
        (int, int) current = FindTile(tile);
        current.Item1 = current.Item1 + mov.Item1;
        current.Item2 = current.Item2 + mov.Item2;
        if ((current.Item1 >= 0 && current.Item1 < gridSize.Item1) && (current.Item2 >= 0 && current.Item2 < gridSize.Item2))
            return tiles_arr[current.Item1, current.Item2];
        else
            return null;
    }

    public void HighlightCross(Tile tile, bool val)
    {
        (int, int) current = FindTile(tile);

        for (int j = 0; j < gridSize.Item1; j++)
        {
                tiles_arr[current.Item1, j].SetHover(val);
        }
        for (int i = 0; i < gridSize.Item2; i++)
        {
                tiles_arr[i, current.Item2].SetHover(val);
        }
    }

    public int CheckDistance(Tile tile1, Tile tile2)
    {
        (int, int) t1 = FindTile(tile1);
        (int, int) t2 = FindTile(tile2);

        int val = Mathf.Abs(t1.Item1 - t2.Item1) + Mathf.Abs(t1.Item2 - t2.Item2);
        return val;
    }

    public void HighlightDistance(int dist, Tile mid)
    {
        for (int j = 0; j < gridSize.Item1; j++)
        {
            for (int i = 0; i < gridSize.Item2; i++)
            {
                if ((dist == CheckDistance(mid, tiles_arr[i, j]) || tiles_arr[i, j] == mid))
                {
                    if(tiles_arr[i, j].GetUnit() == null)
                        tiles_arr[i, j].SetHighlight(true, new Color32(0, 255, 255, 100)); //teall/blue
                    else if(tiles_arr[i, j].GetUnit().GetOwner() != mid.GetUnit().GetOwner())
                        tiles_arr[i, j].SetHighlight(true, new Color32(0, 200, 255, 100)); //Dark blue
                }  
            }
        }
    }

    public void UnHighlightDistance()
    {
        for (int j = 0; j < gridSize.Item1; j++)
        {
            for (int i = 0; i < gridSize.Item2; i++)
            {
                tiles_arr[i, j].SetHighlight(false, new Color32(250, 250, 250, 96));
            }
        }
    }

    /*
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
                if (0 <= i && i < 8)
                {
                    if (tiles_arr[i, val_j].GetComponent<GridSelector>().GetCharacter() == null)
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
    */
}

