using UnityEngine;

public class StunState : AIState
{
    public StunState(AIController c) : base(c) { }

    public override void OnEnter()
    {
        Debug.Log("Entrando en STUN");
        if (m_agent != null)
        {
            m_agent.isStopped = true;
            m_agent.ResetPath();
        }
    }

    public override void UpdateState() { /* vacío: el timer vive en AIController */ }

    public override void OnExit()
    {
        if (m_agent != null) m_agent.isStopped = false;
    }
}