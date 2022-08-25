using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodBuilding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("helo");
        var r = this.GetComponent<Renderer>();
        if (r == null)
            return;
        var bounds = r.bounds;
        Debug.Log(bounds);
    }
}
