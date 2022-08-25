using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeoCoderSearchItem : MonoBehaviour
{
    public int place_id;
    public string lat;
    public string lon;
    public TextMeshProUGUI LocationName, Address;

    public void setItem(IGeoCoderSearchItem item)
    {
        this.place_id = item.place_id;
        this.lat = item.lat;
        this.lon = item.lon;
        var locationName = item.type + " " + item.@class;
        this.LocationName.text = char.ToUpper(locationName[0])+locationName.Substring(1);
        this.Address.text = item.display_name;
    }

    public void clickItem()
    {
        string[] latlng = { this.lon, this.lat };
        GeoCoder.LoadMap(latlng);
    }
}
