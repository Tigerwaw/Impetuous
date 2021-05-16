using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class ShopKeeper : Interactable
{
  public GameObject shopUI;
  public GameObject itemToolTip;
  public Transform playerTransform;

  public override void Interact()
  {
    base.Interact();
    Shop();
  }

  void Shop()
  {
    shopUI.gameObject.SetActive(true);
  }

  void CloseShop()
  {
    shopUI.gameObject.SetActive(false);
    itemToolTip.SetActive(false);
  }

  void OnTriggerExit(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
      CloseShop();
  }
}
