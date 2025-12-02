using UnityEngine;

public class LootChestController : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;

    // 👇 Solo para pruebas / simulación de función costosa
    private bool _costlyFunctionWasCalled = false;

    // 👇 Propiedad de solo lectura por si quieres acceder sin reflection
    public bool CostlyFunctionWasCalled => _costlyFunctionWasCalled;

    public void Interact()
    {
        // Reset del flag en cada interacción
        _costlyFunctionWasCalled = false;

        if (_isOpen)
        {
            Debug.Log("Cofre ya está abierto.");
            // OJO: aquí NO llamamos a la función costosa
            return;
        }

        _isOpen = true;

        // Aquí sí simulamos la función cara
        PlayComplexParticleEffect();

        Debug.Log("Cofre abierto! Has obtenido un tesoro.");
    }

    // Simulación de la función muy costosa
    private void PlayComplexParticleEffect()
    {
        _costlyFunctionWasCalled = true;

        // aquí irían partículas, sonidos, etc. en tu juego real
    }
}