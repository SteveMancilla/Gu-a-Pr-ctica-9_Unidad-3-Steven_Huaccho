using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 3.5f;
    [SerializeField] private LayerMask _interactableMask = ~0; // por defecto: todo

    private Camera _mainCamera;
    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        _inputActions.Player.Interact.performed -= OnInteract;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        DetectAndShowFeedback();

        // Dibuja SIEMPRE el rayo para depuración
        Ray dbgRay = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        Debug.DrawRay(dbgRay.origin, dbgRay.direction * _interactionDistance, Color.cyan);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("INTERACT PRESSED");
        PerformRaycastInteraction();
    }

    private void PerformRaycastInteraction()
    {
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _interactionDistance, Color.green, 0.25f);

        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactableMask, QueryTriggerInteraction.Ignore))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("Interacción con: " + hit.collider.name);
                interactable.Interact();
            }
            else
            {
                Debug.Log("Golpeé algo sin IInteractable: " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("No golpeé nada dentro del rango.");
        }
    }

    private void DetectAndShowFeedback()
    {
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactableMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.GetComponentInParent<IInteractable>() != null)
            {
                Debug.Log("Objeto interactuable a la vista: " + hit.collider.name);
                // aquí podrías mostrar un "[E] Interactuar" en UI
            }
        }
    }
}