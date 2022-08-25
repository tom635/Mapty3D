using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class GeoCoder : MonoBehaviour
{
    public GameObject MapBuilder;
    public static Mapzen.RegionMap regionMap;
    public GameObject loadingScene;

    public GameObject searchItemPrefab;
    public List<IGeoCoderSearchItem> searchItems;
    public GameObject searchMenuItem;

    public static string[] currLatLng = null;

    public static int zoomLevel = 16;
    public static int maxZoomLevel = 16;
    public static int minZoomLevel = 0;



    private void Awake()
    {
    }

    private void Start()
    {
        regionMap = MapBuilder.GetComponent<Mapzen.RegionMap>();
    }

    private void Update()
    {
        if (regionMap && regionMap.HasPendingTasks())
        {
            if (regionMap.FinishedRunningTasks())
            {
                regionMap.GenerateSceneGraph();
            }
        }
    }


    public async void Search(String searchInput)
    {
        var searchEndpoint = new Uri(string.Format("https://nominatim.openstreetmap.org/search/{0}?format=json", searchInput));
        try
        {
            WWWForm form = new WWWForm();
            UnityWebRequest request = UnityWebRequest.Get(searchEndpoint);
            var handler = request.SendWebRequest();

            while (!handler.isDone)
            {
                await Task.Yield();
            }

            Debug.Log(request.result.ToString());

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("SUCCESSFULLY SEARCHED");
                GeoCoderSearchResponse response = JsonUtility.FromJson<GeoCoderSearchResponse>("{\"searchData\":" + request.downloadHandler.text + "}");
                SetItems(response.searchData);
            }
            request.Dispose();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public static void LoadMap(string[] latlng)
    {
        Debug.Log(zoomLevel);
        currLatLng = latlng;
        regionMap.DownloadTilesAsync(LatLngConvertor.GetBoundingBox(new List<string>(latlng), 1f, zoomLevel));
    }

    public void ZoomIn()
    {
        if (currLatLng != null)
        {
            if (zoomLevel < maxZoomLevel)
            {
                zoomLevel++;
                LoadMap(currLatLng);
            }
        }
    }

    public void ZoomOut()
    {
        if (currLatLng != null)
        {
            if (zoomLevel > minZoomLevel)
            {
                zoomLevel--;
                LoadMap(currLatLng);
            }
        }
    }

    public void SetItems(IGeoCoderSearchItem[] items)
    {
        this.ClearAllItems();
        foreach (IGeoCoderSearchItem item in items)
        {
            GameObject temp = Instantiate(searchItemPrefab);
            temp.transform.SetParent(searchMenuItem.transform);
            temp.transform.localScale = Vector3.one;
            GeoCoderSearchItem ct = temp.GetComponent<GeoCoderSearchItem>();
            ct.setItem(item);
            searchItems.Add(item);
        }
    }

    public void ClearAllItems()
    {
        foreach (Transform tr in searchMenuItem.transform)
        {
            Destroy(tr.gameObject);
        }
        searchItems.Clear();
    }
}

[System.Serializable]
public class GeoCoderSearchResponse
{
    public IGeoCoderSearchItem[] searchData;
}

[System.Serializable]
public class IGeoCoderSearchItem
{
    public int place_id;
    public string licence;
    public string osm_type;
    public long osm_id;
    public List<string> boundingbox;
    public string lat;
    public string lon;
    public string display_name;
    public string @class;
    public string type;
    public double importance;
    public string icon;
}
