using UnityEngine;

public class EnemyInteractable : MonoBehaviour, IInteractable
{
    private AIController _ai;
    private void Awake() => _ai = GetComponent<AIController>();

    public void Interact()
    {
        if (_ai == null) return;
        _ai.Stun(); // usa el valor del Inspector (stunDuration)
    }
}