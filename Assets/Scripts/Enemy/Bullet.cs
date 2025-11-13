using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Life & Behavior")]
    public float lifeSeconds = 3f;
    public bool useTrigger = true;   // true si tu collider es Trigger
    public float stunDuration = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeSeconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!useTrigger) return;
        HandleHit(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (useTrigger) return;
        HandleHit(collision.collider.gameObject);
    }

    private void HandleHit(GameObject hitGO)
    {
        // 1) Pipeline existente: IInteractable
        if (hitGO.GetComponentInParent<IInteractable>() is IInteractable interactable)
        {
            interactable.Interact();
            Destroy(gameObject);
            return;
        }

        // 2) Si no hay IInteractable, intenta IA directa
        if (hitGO.GetComponentInParent<AIController>() is AIController ai)
        {
            ai.Stun(stunDuration);
            Destroy(gameObject);
            return;
        }

        // 3) Otro objeto: destruye la bala
        Destroy(gameObject);
    }
}