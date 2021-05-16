using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{

  public Text currencyText;
  public GameObject[] currencyIcon;

  private int playerCurrency;

  void Update()
  {
    if (playerCurrency == PlayerStats.currency)
    {
      return;
    }

    if (playerCurrency > PlayerStats.currency)
    {
      ResetCoins();
    }

    playerCurrency = PlayerStats.currency;

    string currencyCount = PlayerStats.currency.ToString();
    currencyText.text = currencyCount;

    int coinValue = Mathf.RoundToInt(PlayerStats.currency / 20);
    RefreshCoins(coinValue);
  }

  private void ResetCoins()
  {
    foreach (GameObject coin in currencyIcon)
    {
      coin.SetActive(false);
    }
  }

  private void RefreshCoins(int value)
  {
    for (int i = 0; i < value; i++)
    {
      currencyIcon[i].SetActive(true);
    }
  }
}
