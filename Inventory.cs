using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

  #region Singleton

  public static Inventory instance;

  void Awake()
  {
    if (instance != null)
    {
      Debug.LogWarning("More than one instance of Inventory found!");
      return;
    }

    instance = this;
  }
  #endregion

  public PlayerStats playerStats;
  public InventoryUI inventoryUI;
  public Transform player;
  public ItemList itemList;

  public GameObject RuneHealPrefab;

  public int space = 8;

  public List<Item> items = new List<Item>();

  [HideInInspector]
  public AudioManager audioManager;

  private void Start()
  {
    audioManager = playerStats.GetComponentInParent<AudioManager>();
  }

  public bool Add (Item item)
  {
    if (items.Count >= space)
    {
      Debug.Log("Not enough room");
      return false;
    }
    items.Add(item);
    playerStats.ApplyItemEffects(item);
    inventoryUI.UpdateUI();
    CheckForItemCombinations();
    return true;
  }

  public void Remove (Item item)
  {
    items.Remove(item);
    playerStats.RemoveItemEffects(item);
    inventoryUI.UpdateUI();
  }

  public void UseItem (Item item)
  {
    if (item.name == "Healing Rune")
    {
      // Play Item use Sound Effect
      audioManager.PlaySound("HerbCloud");

      GameObject runeHealParticle = Instantiate(RuneHealPrefab, player.position, Quaternion.identity);
      Destroy(runeHealParticle, 2.0f);
      playerStats.GetHeal(10);
    }
  }

  // Go through all items in the game and see if the player can craft any of them with the items they have in their inventory
  public void CheckForItemCombinations()
  {
    foreach (Item item in itemList.allItems)
    {
      if (item.isCraftableItem == true)
      {
        bool craftable = IsItemCraftable(item);
        if (craftable == true)
        {
          CraftItem(item);
        }
      }
    }
  }

  private void CraftItem(Item item)
  {
    // Play Crafting Item Sound Effect
    AudioManager hud_audioManager = Camera.main.GetComponent<AudioManager>();
    hud_audioManager.PlaySound("ButtonClick");

    Debug.Log("Crafting Item");

    // Remove components required for crafting the item and then add the item itself to the players inventory
    foreach (Item component in item.requiredItems)
    {
      items.Remove(component);
    }

    items.Add(item);
    inventoryUI.UpdateUI();
  }

  // Compare required components of an item and the players inventory, and return whether it is craftable is true or false
  private bool IsItemCraftable(Item item)
  {
    List<Item> removeDupeList = new List<Item>();

    foreach (Item i in items)
    {
      removeDupeList.Add(i);
    }

    int reqItemsOwned = 0;

    foreach (Item comp in item.requiredItems)
    {
      if (removeDupeList.Contains(comp))
      {
        removeDupeList.Remove(comp);
        reqItemsOwned += 1;
      }
    }

    // If player does not have all the required items, the function will not continue
    if (reqItemsOwned == item.requiredItems.Count)
    {
      return true;
    }

    return false;
  }
}
