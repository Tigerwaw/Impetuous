using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
  public Interactable focus;

  public LayerMask movementMask;

  Camera cam;
  PlayerMotor motor;
  public GameObject clickParticlePrefab;
  public GameObject focusParticlePrefab;

  // Use this for initialization
  void Start ()
  {
    cam = Camera.main;
    motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (EventSystem.current.IsPointerOverGameObject())
      return;

    if (Input.GetMouseButtonDown(1))
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit, 100))
      {
        Interactable interactable = hit.collider.GetComponent<Interactable>();
        if(interactable != null)
        {
          SetFocus(interactable);
          GameObject focusParticle = Instantiate(focusParticlePrefab, new Vector3(hit.point.x, hit.point.y - 0.6f, hit.point.z), Quaternion.identity);
          Destroy(focusParticle, 2.0f);
        }
        else if (Physics.Raycast(ray, out hit, 100, movementMask))
        {
          GameObject clickParticle = Instantiate(clickParticlePrefab, new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z), Quaternion.identity);
          Destroy(clickParticle, 2.0f);
          motor.MoveToPoint(hit.point);
          RemoveFocus();
        }
      }
    }
  }

  void SetFocus(Interactable newFocus)
  {
    if(newFocus != focus)
    {
      if(focus != null)
        focus.OnDefocused();
      focus = newFocus;
      motor.FollowTarget(newFocus);
    }
    newFocus.OnFocused(transform);
  }

  public void RemoveFocus()
  {
    if(focus != null)
      focus.OnDefocused();
    focus = null;
    motor.StopFollowingTarget();
  }
}
