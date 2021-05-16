using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpike : Ability
{
  public int earthSpikeHitboxForce = 250;

  public GameObject earthSpikeMeshPrefab;
  public GameObject earthSpikeParticlePrefab;
  public Rigidbody earthSpikeHitboxPrefab;

  PlayerAbilities playerAbilities;
  Transform playerTransform;
  Transform earthSpikeOrigin;

  public event System.Action OnEarthSpikeCast;

  public override void InitializeAbility(PlayerAbilities playerAbilities)
  {
    base.InitializeAbility(playerAbilities);

    playerTransform = playerAbilities.transform;
    earthSpikeOrigin = playerAbilities.earthSpikeOrigin;
  }

  public override void ExecuteAbility()
  {
    // Play Ability Sound Effect
    audioManager.PlaySound("Earthspike");

    // Instantiate and destroy Earth Spike Mesh
    GameObject earthSpikeMesh = Instantiate(earthSpikeMeshPrefab, playerTransform.position, playerTransform.rotation);
    Destroy(earthSpikeMesh, 3.0f);

    // Instantiate and destroy Earth Spike Hitbox
    Rigidbody earthSpikeHitbox = Instantiate(earthSpikeHitboxPrefab, earthSpikeOrigin.position, playerTransform.rotation);
    Destroy(earthSpikeHitbox.gameObject, 0.5f);
    earthSpikeHitbox.AddForce(earthSpikeOrigin.forward * earthSpikeHitboxForce);

    // Instantiate and destroy Earth Spike Particle Effect
    GameObject earthSpikeParticle = Instantiate(earthSpikeParticlePrefab, new Vector3(earthSpikeOrigin.position.x, earthSpikeOrigin.position.y - 0.5f, earthSpikeOrigin.position.z), playerTransform.rotation);
    Destroy(earthSpikeParticle, 3.0f);

    // Trigger Player Cast Animation
    if (OnEarthSpikeCast != null)
      OnEarthSpikeCast();
  }
}
