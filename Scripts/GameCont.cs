using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCont : MonoBehaviour
{
    private int mode = 0;

    private GameObject selected_Tile;
    private List<GridSelector> highlighted_Tiles;

    public Spawning spawn;
    public Tiles tiles;

    private bool start = true;

    // Start is called before the first frame update
    void Start()
    {
        //highlight allies side 
        //spawn.Highlight_Sides();


        //highlight eneimes side
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < 1 && start)
        {
            spawn.highlightSpawn();
            start = false;
        }
                
    }

    public void setSelected_Tile(GameObject tile)
    {
        GridSelector tileGS = tile.GetComponent<GridSelector>();
        if (mode == 0 && spawn.GetAlliesCount() < 4)
        { 
            if(spawn.Add_Spawn(tile))
            {
                selected_Tile = tile;
                tileGS.SetColour(1, 1);
            }
            if(spawn.GetAlliesCount() == 4)
            {
                spawn.SpawnAllies();
                spawn.SpawnEnimes();
                mode++;
            }
                
        }
        else if(mode == 1)
        {
            //if(tile has character)
            //display character Ui/ options
            //highlight tilis they can move to


            if(tileGS.GetCharacter() != null)
            {
                if(highlighted_Tiles != null)
                    clear_Highlighted_Tiles();
                Add_Highlighted_Tiles(tiles.FindNearbyTiles(tile, tileGS.GetCharacter().GetMovement()));
                Debug.Log(tileGS.GetCharacter().getName());
            }
            else
            {
                //tileGS.SetColour(1, 1);
            } 
        }
        else if(mode == 2)
        {

        }
    }

    public void Add_Highlighted_Tiles(List<GridSelector> tiles)
    {
        if (highlighted_Tiles != null)
            Debug.Log(highlighted_Tiles.Count);
        highlighted_Tiles = tiles;
        Debug.Log(highlighted_Tiles.Count);
    }

    public void clear_Highlighted_Tiles()
    {
        IEnumerator<GridSelector> highlighted_Tiles_Enum = highlighted_Tiles.GetEnumerator();

        highlighted_Tiles_Enum.MoveNext();

        for(int i = 0; i < highlighted_Tiles.Count; i++)
        {
            highlighted_Tiles_Enum.Current.unSelect();
        }

        highlighted_Tiles.Clear();
    }

}
