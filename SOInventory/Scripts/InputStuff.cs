using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputStuff : MonoBehaviour
{
    public GameObject inventoryScreen;
    public GameObject equipementScreen;

    public int xpToGive;
    public RpgPlayer player;
    public Level level;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UiPanel();
        DevModeGiveStuff();
    }

    public void UiPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            equipementScreen.SetActive(false);
            inventoryScreen.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = inventoryScreen.activeInHierarchy;
            inventoryScreen.SetActive(!isActive);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = equipementScreen.activeInHierarchy;
            equipementScreen.SetActive(!isActive);
        }
    }

    public void DevModeGiveStuff()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    level.AddXp(100);
        //}
    }
}
