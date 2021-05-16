using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

  public Image icon;

  [HideInInspector]
  Item item;

  public GameObject itemToolTip;
  public Image itemToolTip_Icon;
  public Text itemNameText;
  public Text itemDescText;
  public Text itemCostText;

  public GameObject[] itemComponentIconBgs;
  public Image[] itemComponentIcons;

  public void AddItem (Item newItem)
  {
    item = newItem;

    icon.sprite = item.icon;
    icon.enabled = true;
  }

  public void ClearSlot()
  {
    item = null;

    icon.sprite = null;
    icon.enabled = false;
  }

  public void UseItem()
  {
    if (item != null)
    {
      item.Use();
    }
  }

  public void OnHoverOverEnter()
  {
    if (item == null)
    {
      return;
    }

    itemToolTip_Icon.sprite = icon.sprite;
    itemNameText.text = item.name;
    itemDescText.text = item.description;
    itemCostText.text = (item.shopCost).ToString();
    itemToolTip.gameObject.SetActive(true);

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
