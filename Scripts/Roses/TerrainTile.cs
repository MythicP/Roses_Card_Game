using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{

    public int value;
    public List<GameObject> terreains;
    private GameObject mod;

    private GameObject unit;
    private bool pulse;
    private Material[] mat_ar;
    private float intensity;
    private bool rising;


    // Start is called before the first frame update
    void Start()
    {
        //spawnRandomTerrain();
        setTerrain(value);
        pulse = false;
        intensity = 0;
        rising = true;

        mat_ar = mod.GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        if(pulse)
            Pulse();
    }

    private void Pulse()
    {
        if(intensity < 0.3 && rising)
            intensity += 0.3f * Time.deltaTime;
        else
        {
            intensity -= 0.3f * Time.deltaTime;
            rising = false;
        }
            
        if (intensity <= 0)
            rising = true;

        SetAllMatsColor(new Color(1f, 1f, 1.0f, 1.0f) * intensity);
    }

    public void setTerrain(int i)
    {
        GameObject temp = Instantiate(terreains[i], transform.position, Quaternion.identity);
        temp.transform.eulerAngles = new Vector3(-90, 0, 0);
        temp.transform.SetParent(transform);

        if (mod != null)
            Destroy(mod);
        mod = temp;
    }

    private void spawnRandomTerrain()
    {
        int i = Random.Range(0, terreains.Count);
        setTerrain(i);
    }

    public void SetUnit(GameObject new_unit)
    {
        unit = new_unit;
    }

    public GameObject GetUnit()
    {
        return unit;
    }

    public void SetPulse(bool val)
    {
        pulse = val;
        if(val == false)
        {
            SetAllMatsEmmission(false);
             intensity = 0;
            rising = true;
        }
        else
            SetAllMatsEmmission(true);
    }

    private void SetAllMatsColor(Color32 col)
    {
        for(int i = 0; i < mat_ar.Length; i++)
        {
            mat_ar[i].SetColor("_EmissionColor", col);
        }
    }

    private void SetAllMatsEmmission(bool val)
    {
        if(val == true)
        {
            for (int i = 0; i < mat_ar.Length; i++)
            {
                mat_ar[i].EnableKeyword("_EMISSION");
            }
        }
        else
            for (int i = 0; i < mat_ar.Length; i++)
            {
                mat_ar[i].DisableKeyword("_EMISSION");
            }

    }
}
