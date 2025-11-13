using UnityEngine;
using System.Collections;

///<summary>
/// Controlador principal de la IA. Gestiona el estado actual y las transiciones
/// </summary>


public class AIController : MonoBehaviour
{
    [Header("AI Settings")]

    public Transform[] waypoints; //Para que el Diseñador asigne la ruta

    public float patrolSpeed = 2f;

    public float  chaseSpeed = 5f;

    public float detectionRadius = 10f;

    public float loseSightRadius = 15f;

    private AIState _currentState;

    [Header("Stun")]  ///// stuner al enemigo por un tiempo
    public float stunDuration = 3f;
    private Coroutine _stunRoutine;

    private void Awake()
    {
        //El estado incial
        ChangeState(new PatrolState(this));
    }
    
    private void Update()
    {
        //Delega la lógica de  actulización al estado actual
        //Principio de Responsabilidad Única
        _currentState?.UpdateState();
    }

    public void ChangeState(AIState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();
    }
    
    //stun
    public void Stun(float? customDuration = null)
    {
        ChangeState(new StunState(this));
        if (_stunRoutine != null) StopCoroutine(_stunRoutine);
        _stunRoutine = StartCoroutine(StunCountdown(customDuration ?? stunDuration));
    }
    private IEnumerator StunCountdown(float duration)
    {
        yield return new WaitForSeconds(duration);
        ChangeState(new PatrolState(this));   // volver a patrulla
        _stunRoutine = null;
    }
}
