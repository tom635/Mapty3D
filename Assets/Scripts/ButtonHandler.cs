using UnityEngine;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public GameObject searchWindow;
    public TMP_InputField inputField;
    public GeoCoder geoCoder;
    public GameObject mainCanvas;
    public void HideHandler()
    {
        mainCanvas.GetComponent<MenuScript>().Hide();
    }

    public void SearchHandler()
    {
        geoCoder.Search(inputField.text);
    }
}
