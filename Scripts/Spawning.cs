using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject allies_Parent; //for orginization
    public GameObject enimeies_Parent;

    public List<GameObject> allies_List; // allie characters list
    public List<GameObject> enimeies_List; // Make public and place prefabs here

    private List<GameObject> allies_Selected_Spawns; // Selected Spawn locations allies
    public List<GameObject> allies_Spawns; // Selected Spawn locations allies
    public List<GameObject> enimeies_Spawns; //All posible Spawn locatinos for enimeies

    // Start is called before the first frame update
    void Start()
    {
        allies_Selected_Spawns = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlightSpawn()
    {
        IEnumerator<GameObject> allies_Spawns_Enum = allies_Spawns.GetEnumerator();

        allies_Spawns_Enum.MoveNext();

        for (int i = 0; i < allies_Spawns.Count; i++)
        {
            allies_Spawns_Enum.Current.GetComponent<GridSelector>().SetColour(5, 0);
            allies_Spawns_Enum.MoveNext();
        }
    }

    // looks throught all posible spawn locations and removes selected from "allies_Spawns" and adds to "allies_Selected_Spawns"
    public bool Add_Spawn(GameObject spawn_Tile)
    {
        bool name_check = false;

        IEnumerator<GameObject> allies_Spawns_Enum = allies_Spawns.GetEnumerator();
        allies_Spawns_Enum.MoveNext();

        for (int i = 0; i < allies_Spawns.Count; i++)
        {
            if(spawn_Tile.name.Equals(allies_Spawns_Enum.Current.name))
            {
                name_check = true;
                allies_Spawns.Remove(allies_Spawns_Enum.Current);
                break;
            }
                
            allies_Spawns_Enum.MoveNext();
        }

        if (name_check)
        {
            allies_Selected_Spawns.Add(spawn_Tile);
            return true;
        }
        else
            return false;
    }

    public void SpawnAllies()
    {
        IEnumerator<GameObject> allies_List_Enum = allies_List.GetEnumerator();
        IEnumerator<GameObject> allies_Spawns_Enum = allies_Selected_Spawns.GetEnumerator();

        allies_List_Enum.MoveNext();
        allies_Spawns_Enum.MoveNext();

        //spawn allies
        for (int i = 0; i < allies_List.Count; i++)
        {
            GameObject clone;

            clone = Instantiate(allies_List_Enum.Current, allies_Spawns_Enum.Current.transform.position, Quaternion.identity);


            allies_Spawns_Enum.Current.GetComponent<GridSelector>().SetCharacter(clone.GetComponent<Character>());
            allies_Spawns_Enum.Current.GetComponent<GridSelector>().unSelect();

            allies_List_Enum.MoveNext();
            allies_Spawns_Enum.MoveNext();
        }

        IEnumerator<GameObject> allies_Spawns_Enum1 = allies_Spawns.GetEnumerator();

        allies_Spawns_Enum1.MoveNext();

        for (int i = 0; i < allies_Spawns.Count; i++)
        {
            allies_Spawns_Enum1.Current.GetComponent<GridSelector>().unSelect();
            allies_Spawns_Enum1.MoveNext();
        }
    }

    public void SpawnEnimes()
    {
        //spawn enimes
        //Randomly select enimes location from last 2 rows

        IEnumerator<GameObject> enimeies_List_Enum = enimeies_List.GetEnumerator();
        IEnumerator<GameObject> enimeies_Spawns_Enum = enimeies_Spawns.GetEnumerator();

        enimeies_List_Enum.MoveNext();
        enimeies_Spawns_Enum.MoveNext();

        int[] randnumbers = new int[4];

        for (int i = 0; i < randnumbers.Length; i++)
        {
            randnumbers[i] = -1;

            int temp = Random.Range(0, 16);
            bool retry = false;

            for (int j = 0; j < i; j++)
            {
                if (randnumbers[j] == temp)
                    retry = true;
            }

            if (retry)
                i -= 1;
            else
                randnumbers[i] = temp;

        }

        for (int j = 0; j < randnumbers.Length; j++)
        {
            for (int i = 0; i < randnumbers[j]; i++)
            {
                //(i == randnumbers[j] -1 )
                //enimeies_Spawns_Enum.MoveNext();
            }
            enimeies_Spawns_Enum.Reset();
            enimeies_Spawns_Enum.MoveNext();
        }
    }

    public void Highlight_Sides()
    {

    }

    public int GetAlliesCount()
    {
        return allies_Selected_Spawns.Count;
    }
}
