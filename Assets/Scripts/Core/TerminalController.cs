using UnityEngine;

[DisallowMultipleComponent]
public class TerminalController : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Light terminalLight;

    [Header("Settings")]
    [SerializeField] private bool startActive = false;

    private bool _isActive;

    private void Awake()
    {
        
        if (terminalLight == null)
        {
            terminalLight = GetComponentInChildren<Light>();
        }
    }

    private void Start()
    {
        ApplyState(startActive);
    }

    
    public void Interact()
    {
        Toggle();

        Debug.Log("Terminal activado. Disparando evento OnObjectiveActivated.");
        GameEvents.TriggerObjectiveActivated();
    }

    private void Toggle()
    {
        ApplyState(!_isActive);
        Debug.Log($"Estado del sistema: {(_isActive ? "Activo" : "Inactivo")}");
    }

    private void ApplyState(bool value)
    {
        _isActive = value;

        if (terminalLight != null)
        {
            terminalLight.color = _isActive ? Color.green : Color.red;
            terminalLight.intensity = _isActive ? 4f : 2f; // más notorio
        }
    }

    #if UNITY_EDITOR
        // Para ver el color correcto en el editor cuando cambias startActive
        private void OnValidate()
        {
            if (terminalLight != null)
                ApplyState(startActive);
        }
    #endif
}
