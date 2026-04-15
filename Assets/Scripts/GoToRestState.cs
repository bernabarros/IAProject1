using UnityEngine;
/// <summary>
/// Estado responsável por levar um agente para a área de descanso quando a sua necessidade de descanso é alta.
/// </summary>
public class GoToRestState : IState
{
    private CrewMember agent;
    private Transform restPoint;

    /// <summary>
    /// Construtor do estado de ir para descanso.
    /// </summary>
    /// <param name="agent"></param>
    public GoToRestState(CrewMember agent)
    {
        this.agent = agent;

        restPoint = GameObject.Find("RestPoint").transform;
    }

    /// <summary>
    /// Executado quando o agente entra no estado de ir para descanso.
    /// </summary>
    public void Enter()
    {
        agent.navAgent.SetDestination(restPoint.position);
    }

    /// <summary>
    /// Atualiza o estado de ir para descanso.
    /// </summary>
    public void Update()
    {
        if(!agent.navAgent.pathPending && 
        agent.navAgent.remainingDistance < 1f)
        {
            agent.fsm.ChangeState(new RestState(agent));
        }
    }

    /// <summary>
    /// Executado quando o agente sai do estado de ir para descanso.
    /// </summary>
    public void Exit()
    {}
}
