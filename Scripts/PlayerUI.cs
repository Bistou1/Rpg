using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;  
    public TextMeshProUGUI interactText;
    public Image healthBarFill;
    public Image xpBarFill;

    public RpgPlayer player;

    void Awake()
    {
        // get the player
        //player = FindObjectOfType<RpgPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelText()
    {
        //levelText.text = "Level\n" + player.curLevel;
        levelText.text = "Level\n" + player.level.currentLevel;
    }

    public void UpddateHealthBar()
    {
        healthBarFill.fillAmount = (float)player.curHp / (float)player.maxHp;
    }

    public void UpddateXpBar()
    {
        //xpBarFill.fillAmount = (float)player.curXp / (float)player.xpToNextLevel;
        xpBarFill.fillAmount = player.level.experience;
    }

    public void SetInteractText (Vector3 pos, string text)
    {
        interactText.gameObject.SetActive(true);
        interactText.text = text;

        interactText.transform.position = Camera.main.WorldToScreenPoint(pos + Vector3.up);
    }

    public void DisableInteractText()
    {
        if (interactText.gameObject.activeInHierarchy)
            interactText.gameObject.SetActive(false);
    }

    //public void UpdateInventoryText()
    //{
    //    inventoryText.text = "";

    //    foreach(string item in player.inventory)
    //    {
    //        inventoryText.text += item + "\n";
    //    }
    //}
}
