using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayMenu : MonoBehaviour
{
    public GameObject HTPMenu;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        HTPMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isOpen)
            {
                close();
            }
            else
            {
                open();
            }
        }
    }

    public void open()
    {
        HTPMenu.SetActive(true);
        Time.timeScale = 0;
        isOpen = true;
    }

    public void close()
    {
        HTPMenu.SetActive(false);
        Time.timeScale = 1;
        isOpen = false;
    }
}
