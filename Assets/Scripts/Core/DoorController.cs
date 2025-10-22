using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;

    public void Interact()
    {
        _isOpen = !_isOpen;
        // Aquí puedes agregar la lógica para animar la puerta o cambiar su estado visual.
        Debug.Log(_isOpen ? "Puerta Abierta" : "Puerta Cerrada");
    }
}
