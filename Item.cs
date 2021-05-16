using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class Item : ScriptableObject {

  public Sprite icon = null;
  new public string name = "New Item";
  [TextArea]
  public string description;
  [Space]

  public bool isDefaultItem = false;
  public bool isPassiveItem = false;
  public bool isConsumableItem = false;
  public bool isCraftableItem = false;
  [Space]

  public Ability itemAbility;
  [Space]

  public int shopCost;
  public List<Item> requiredItems = new List<Item>();
  [Space]

  public int armorModifier;
  public int damageModifier;
  public int maxHealthModifier;
  public int attackSpeedModifier;
  public int healthRegenModifier;
  public int movementSpeedModifier;
  public int healthOnKillModifier;

  public virtual void Use ()
  {
    if (isPassiveItem)
    {
      Debug.Log("Not usable!");
      return;
    }

    Debug.Log("Using " + name);
    Inventory.instance.UseItem(this);
    //itemAbility.Use etc

    if (isConsumableItem)
    {
      RemoveFromInventory();
    }
  }

  public void RemoveFromInventory ()
  {
    Inventory.instance.Remove(this);
  }
}
