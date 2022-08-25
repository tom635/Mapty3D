using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapzen;

public class FloodSimulator : MonoBehaviour
{
    // Start is called before the first frame update
    public RegionMap map;
    private List<KeyValuePair<GameObject, float>> heightMap = new List<KeyValuePair<GameObject, float>>();

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;



    void Start()
    {
        gradient = new Gradient();
        map = gameObject.GetComponent<RegionMap>();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        // What's the color at the relative time 0.25 (25 %) ?
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void CalculateHeights()
    {
        Transform bldg = map.regionMap.transform.Find("Buildings");
        Transform[] temp = bldg.Find("buildings").GetComponentsInChildren<Transform>();
        Debug.Log(temp[0]);
        foreach (Transform item in temp)
        {
            Debug.Log(temp);
            MeshCollider meshCollider = item.gameObject.GetComponent<MeshCollider>();
            float height = meshCollider.bounds.max.y;
            //- meshCollider.bounds.min.y;
            heightMap.Add(new KeyValuePair<GameObject, float>(item.gameObject, height));
            Debug.Log(item + "-> " + height);
        }

    }

    public void FloodColoring(float FloodValue)
    {
        foreach (var item in heightMap)
        {
            if (item.Key == null || item.Key.name == "buildings")
            {
                continue;
            }
            item.Key.GetComponent<MeshRenderer>().material.color = gradient.Evaluate(FloodValue * 4 / item.Value);
        }
    }
}
