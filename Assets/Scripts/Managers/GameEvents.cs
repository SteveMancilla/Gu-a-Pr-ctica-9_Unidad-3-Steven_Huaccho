using UnityEngine;
using System;

/// <summary>
/// Contenedor estatico para los eventos globales del juego
/// Permite una comunicación desacoplada entre diferentes sistemas (Patron Observer)
/// </summary>

public static class GameEvents
{
    // evento que se dispara cuando un terminal de objetivo es activado
    public static event Action OnObjetiveActivated;

    // metodo para invocar el evento desde cualquier lugar, de forma segura
    public static void TriggerObjectiveActivated()
    {
        // el '?' comprueba si hay algún suscriptor antes de invocar el evento
        OnObjetiveActivated?.Invoke();
    }
}
