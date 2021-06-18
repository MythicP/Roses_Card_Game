using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelector : MonoBehaviour
{

    public GameCont gamecont;
    public Material mat;

    private bool selected;
    private bool highlighted;
    private Character character;

    private string value;
    private Color last_col = Color.black;
    //public Material shader2;


    void Start()
    {
        mat = GetComponent<Renderer>().material;
        selected = false;
        value = "NA";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            selected = false;
            mat.color = Color.gray;
        }     
    }

    private void OnMouseOver()
    {
        if(last_col != mat.color && mat.color != Color.green)
            last_col = mat.color;

        if (character != null)
        {
            if (character.getSide().Equals("ally"))
                mat.color = Color.green;
            else
                mat.color = Color.red;
        }
        else //if(!selected)
            mat.color = Color.green;
            
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gamecont.setSelected_Tile(gameObject);
        }
    }

    void OnMouseExit()
    {
        /*
        if (!selected && !highlighted)
            mat.color = Color.gray;
        else if(!highlighted)
            mat.color = Color.cyan;
        else
            mat.color = new Color(0, 0.7f, 0.5f);
        */

        mat.color = last_col;
        //if character is here///////////////////////////////////////////////////////////setgreen on selection
    }

    public void SetValue(string new_value)
    {
        value = new_value;
    }
    public string GetValue()
    {
        return value;
    }

    public void SetCharacter(Character new_character)
    {
        character = new_character;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public void unSelect()
    {
        mat.color = Color.gray;
        selected = false;
        highlighted = false;
    }

    public void SetColour(int i, int j)
    {
        switch(i)
        {
            case 0:
                {
                    mat.color = Color.green;
                    break;
                }
            case 1:
                {
                    mat.color = Color.cyan;
                    break;
                }
            case 2:
                {
                    mat.color = Color.yellow;
                    break;
                }
            case 3:
                {
                    mat.color = Color.red;
                    break;
                }
            case 4:
                {
                    mat.color = Color.white;
                    break;
                }
            case 5:
                {
                    mat.color = new Color(0, 0.7f, 0.5f);
                    break;
                }
            case 6:
                {
                    mat.color = new Color(0, 0.9f, 0);
                    break;
                }

        }
        if (j == 1)
        {
            selected = true;
            highlighted = false;
        }    
        else
            highlighted = true;
    }
}
