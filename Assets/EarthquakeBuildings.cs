using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapzen;

public class EarthquakeBuildings : MonoBehaviour
{
    public RegionMap map;

    void Start()
    {
        map = gameObject.GetComponent<RegionMap>();
    }

}
