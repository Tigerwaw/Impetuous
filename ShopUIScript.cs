using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIScript : MonoBehaviour {

  public Item[] ShopKeeperItems;

  public ShopSlot[] shopSlots;

	void Start ()
  {
    for (int i = 0; i < shopSlots.Length; i++)
    {
      shopSlots[i].item = ShopKeeperItems[i];
      shopSlots[i].itemIcon.sprite = ShopKeeperItems[i].icon;
    }
  }
}
