using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Market : MonoBehaviour
{
    public const int shieldCost = 10;
    public const int signCost = 10;
    public const int shoesCost = 10;

    public GameObject marketPanel;
    private void OnTriggerEnter2D(Collider2D collision) => marketPanel.SetActive(true);

    private void OnTriggerExit2D(Collider2D collision) => marketPanel.SetActive(false);

    public void buyShield()
    {
        Inventory.coin -= shieldCost; 
        if (Inventory.coin < 0)
        {
            Inventory.coin += shieldCost;
        }
        Inventory.item_shield++;
    }
    public void buySign()
    {
        Inventory.coin -= signCost; 
        if (Inventory.coin < 0)
        {
            Inventory.coin += signCost;
        }
        Inventory.item_sign++;
    }
    public void buyShoes()
    {
        Inventory.coin -= shoesCost;
        if (Inventory.coin < 0)
        {
            Inventory.coin += shoesCost;
        }
        Inventory.item_shoes++;
    }
}
