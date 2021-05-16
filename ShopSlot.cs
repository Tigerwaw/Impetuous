using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour
{
  [HideInInspector]
  public Item item = null;

  public Image itemIcon = null;
  public Inventory inventory;

  public GameObject itemToolTip;
  public Image itemToolTip_Icon;
  public Text itemNameText;
  public Text itemDescText;
  public Text itemCostText;

  public GameObject[] itemComponentIconBgs;
  public Image[] itemComponentIcons;

  [HideInInspector]
  public AudioManager audioManager;

  private void Start()
  {
    audioManager = Camera.main.GetComponentInChildren<AudioManager>();
  }

  public void OnBuyClick()
  {
    if (PlayerStats.currency < item.shopCost)
    {
      // Play Not Enough gold Sound Effect
      audioManager.PlaySound("NotEnoughGold");

      Debug.Log("Not enough gold");
      return;
    }

    BuyItem();
  }

  void BuyItem()
  {
    bool itemWasBought = Inventory.instance.Add(item);
    if (itemWasBought)
    {
      // Play Bought Item Sound Effect
      audioManager.PlaySound("ButtonClick");

      PlayerStats.currency -= item.shopCost;
    }
  }

  public void OnHoverOverEnter()
  {
    if (item == null)
    {
      return;
    }

    itemToolTip_Icon.sprite = itemIcon.sprite;
    itemNameText.text = item.name;
    itemDescText.text = item.description;
    itemCostText.text = (item.shopCost).ToString();
    itemToolTip.gameObject.SetActive(true);

    foreach (GameObject c in itemComponentIconBgs)
    {
      c.SetActive(false);
    }

    if (item.isCraftableItem)
    {
      for (int i = 0; i < item.requiredItems.Count; i++)
      {
        itemComponentIcons[i].sprite = item.requiredItems[i].icon;
        itemComponentIconBgs[i].SetActive(true);
      }
    }
  }

  public void OnHoverOverExit()
  {
    itemToolTip.gameObject.SetActive(false);
  }
}
