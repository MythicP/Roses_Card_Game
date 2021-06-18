using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGrid : MonoBehaviour
{
    private List<TerrainTile> tiles;
    private IEnumerator<TerrainTile> list;

    private TerrainTile[,] tiles_arr;

    public (int, int) gridSize = (7, 7);

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<TerrainTile>();

        for (int i = 0; i < transform.childCount; i++)
        {
            tiles.Add(transform.GetChild(i).gameObject.GetComponent<TerrainTile>());
        }

        list = tiles.GetEnumerator();

        //////////////////////////////////////////////////
        tiles_arr = new TerrainTile[gridSize.Item1, gridSize.Item2];

        for (int j = 0; j < gridSize.Item1; j++)
        {
            for (int i = 0; i < gridSize.Item2; i++)
            {
                tiles_arr[i, j] = transform.GetChild((i) + (j * (gridSize.Item2 - 1)) + j).gameObject.GetComponent<TerrainTile>();
    }
        }
    }

    void Update()
    {

    }

    public void reset_list()
    {
        list.Reset();
    }

    public (int, int) FindTile(TerrainTile til)
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

    public TerrainTile GetNextTile(TerrainTile tile, (int, int) mov)
    {
        (int, int) current = FindTile(tile);
        current.Item1 = current.Item1 + mov.Item1;
        current.Item2 = current.Item2 + mov.Item2;
        if ((current.Item1 >= 0 && current.Item1 < gridSize.Item1) && (current.Item2 >= 0 && current.Item2 < gridSize.Item2))
            return tiles_arr[current.Item1, current.Item2];
        else
            return null;
    }

    public void HighlightCross(TerrainTile tile, bool val)
    {
        (int, int) current = FindTile(tile);

        for (int j = 0; j < gridSize.Item1; j++)
        {
            tiles_arr[current.Item1, j].GetComponent<Tile>().SetHover(val);
        }
        for (int i = 0; i < gridSize.Item2; i++)
        {
            tiles_arr[i, current.Item2].GetComponent<Tile>().SetHover(val);
        }
    }
}
