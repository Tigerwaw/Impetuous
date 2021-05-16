using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator {

  protected Inventory inventory;

  public AnimationClip replaceableItemAnim;
  public AnimationClip[] defaultItemAnimSet;
  protected AnimationClip[] currentItemAnimSet;

  protected override void Start()
  {
    base.Start();
    inventory = GetComponent<Inventory>();

    currentItemAnimSet = defaultItemAnimSet;

    // inventory.OnRuneUse += OnRuneUse;
  }

  protected virtual void OnRuneUse()
  {
    animator.SetTrigger("runeUse");
    overrideController[replaceableItemAnim.name] = currentItemAnimSet[0];
  }

  protected virtual void OnEarthSpikeCast()
  {
    animator.SetTrigger("earthSpikeCast");
  }
}
