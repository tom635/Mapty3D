using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject searchMenu;
    public GameObject zoomMenu;
    public GameObject floodMenu;
    private bool isMenuOpen = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (isMenuOpen)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public void Show()
    {
        if (!isMenuOpen)
        {
            searchMenu.SetActive(true);
            zoomMenu.SetActive(true);
            floodMenu.SetActive(true);
            isMenuOpen = true;
        }
    }

    public void Hide()
    {
        if (isMenuOpen)
        {
            searchMenu.SetActive(false);
            zoomMenu.SetActive(false);
            floodMenu.SetActive(false);
            isMenuOpen = false;
        }
    }
}
