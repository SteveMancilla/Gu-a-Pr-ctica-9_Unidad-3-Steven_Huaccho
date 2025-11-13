using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [Header("Setup")]
    public Camera playerCamera;          // Cámara del jugador
    public Transform gunPoint;           // Origen del láser (tu GunPoint)
    public LineRenderer lineRenderer;    // LineRenderer ya agregado en GunPoint
    public KeyCode fireKey = KeyCode.F;

    [Header("Laser")]
    public float maxDistance = 50f;
    public LayerMask hitMask = ~0;

    [Tooltip("Cada cuántos segundos 'aplica efecto' al objetivo mientras mantienes F.")]
    public float tickInterval = 0.5f;    // evita aturdir miles de veces por segundo
    public float stunDuration = 2f;      // segundos de aturdimiento por tick

    // Para rate-limit por objetivo
    private readonly Dictionary<AIController, float> _nextTickAllowed = new();

    void Awake()
    {
        if (lineRenderer) lineRenderer.enabled = false;
    }

    void Update()
    {
        bool firing = Input.GetKey(fireKey);

        if (!firing)
        {
            if (lineRenderer) lineRenderer.enabled = false;
            return;
        }

        if (!playerCamera || !gunPoint || !lineRenderer) return;

        // 1) Activar/actualizar línea
        lineRenderer.enabled = true;
        Vector3 origin = gunPoint.position;
        Vector3 dir    = playerCamera.transform.forward;

        // 2) Raycast instantáneo
        if (Physics.Raycast(origin, dir, out RaycastHit hit, maxDistance, hitMask, QueryTriggerInteraction.Ignore))
        {
            // Dibuja desde el GunPoint hasta el punto de impacto
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);

            // 3) Aplicar efectos con rate-limit
            // a) Primero, si el objetivo implementa IInteractable, usamos su flujo
            if (hit.collider.GetComponentInParent<IInteractable>() is IInteractable interactable)
            {
                // Rate-limit por frame general (evitamos spam excesivo)
                if (Time.frameCount % 5 == 0) // cada ~5 frames
                    interactable.Interact();
            }
            // b) Si no, buscamos una IA y la aturdimos con tickInterval
            else if (hit.collider.GetComponentInParent<AIController>() is AIController ai)
            {
                float now = Time.time;
                if (!_nextTickAllowed.TryGetValue(ai, out float next) || now >= next)
                {
                    ai.Stun(stunDuration);
                    _nextTickAllowed[ai] = now + tickInterval;
                }
            }
        }
        else
        {
            // No golpeó nada: dibuja el láser hasta la distancia máxima
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, origin + dir * maxDistance);
        }
    }
}