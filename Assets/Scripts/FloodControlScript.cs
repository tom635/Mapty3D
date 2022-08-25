using UnityEngine;
using UnityEngine.UI;

public class FloodControlScript : MonoBehaviour
{
    [SerializeField]
    private Slider floodSlider;
    [SerializeField]
    private GameObject waterObject;
    // Start is called before the first frame update
    void Start()
    {
        floodSlider.onValueChanged.AddListener((v) =>
        {
            if (v == 0)
            {
                waterObject.SetActive(false);
            }
            else
            {
                waterObject.SetActive(true);
                waterObject.transform.position = new Vector3(waterObject.transform.position.x, v, waterObject.transform.position.z);
            } 
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
