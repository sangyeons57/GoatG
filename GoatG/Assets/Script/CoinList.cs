using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinList : MonoBehaviour
{
    public void resetCoins()
    {
        Debug.Log(gameObject.transform.childCount);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }

    }
}
