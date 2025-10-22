using UnityEngine;

public class LootChestController : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;

    public void Interact()
    {
        if (_isOpen)
        {
            Debug.Log("Cofre ya está abierto.");
            return;
        }

        _isOpen = true;
        Debug.Log("Cofre abierto! Has obtenido un tesoro.");
    }
}
