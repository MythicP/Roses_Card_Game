using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Texture edge;
    public Texture hover;
    public Texture both;

    public Card unit;
    private TerrainTile terrain;

    private bool edgeLight;
    private bool hovered;
    private bool highlight;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        edgeLight = false;
        hovered = false;
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Color32 temp = new Color32(255, 255, 255, 255);

        if (unit != null)
        {
            edgeLight = true;
            if(unit.transform.parent.name == "Yin")
                temp = new Color32(255, 0, 0, 255);
            else
                temp = new Color32(255, 255, 255, 255);
        } 
        else
            edgeLight = false;

        if (edgeLight && hovered)
        {
            mat.mainTexture = both;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", temp);
        }
        else if(hovered)
        {
            mat.mainTexture = hover;
            mat.DisableKeyword("_EMISSION");
        }
        else if(edgeLight)
        {
            mat.mainTexture = edge;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", temp);
        }
    }

    public void SetEdge(bool temp)
    {
        edgeLight = temp;
        SetRender();
    }

    public void SetHover(bool temp)
    {
        hovered = temp;
        SetRender();
    }

    public void SetBoth(bool temp)
    {
        edgeLight = temp;
        hovered = temp;
        SetRender();
    }

    public void SetRender()
    {
        if (edgeLight == true || hovered == true || highlight == true)
            GetComponent<MeshRenderer>().enabled = true;
        else
            GetComponent<MeshRenderer>().enabled = false;
    }

    public void SetUnit(Card new_unit)
    {
        unit = new_unit;
    }

    public Card GetUnit()
    {
        return unit;
    }

    public void SetHighlight(bool val, Color32 col)
    {
        highlight = val;
        mat.color = col;
    }

    public bool GetHighlight()
    {
        return highlight;
    }

    public void SetTerrain(TerrainTile new_terrain)
    {
        terrain = new_terrain;
    }

    public TerrainTile GetTerrain()
    {
        return terrain;
    }
}
