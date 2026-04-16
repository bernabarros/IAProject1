using UnityEngine;
/// <summary>
/// Estado responsável por levar um agente para a área de descanso quando a sua necessidade de descanso é alta.
/// </summary>
public class GoToRestState : IState
{
    private CrewMember agent;
    private RestSpot restSpot;

    /// <summary>
    /// Construtor do estado de ir para descanso.
    /// </summary>
    /// <param name="agent"></param>
    public GoToRestState(CrewMember agent)
    {
        this.agent = agent;
    }

    /// <summary>
    /// Executado quando o agente entra no estado de ir para descanso.
    /// </summary>
    public void Enter()
    {
        if (RestManager.Instance == null)
        {
            Debug.Log("RestManager não existe!");
            agent.fsm.ChangeState(new WanderState(agent));
            return;
        }

        restSpot = RestManager.Instance.GetFreeSpot();

        if (restSpot == null)
        {
            Debug.Log("Sem spots disponíveis!");
            agent.fsm.ChangeState(new WanderState(agent));
            return;
        }

        restSpot.Occupy();
        agent.navAgent.SetDestination(restSpot.transform.position);
    }

    /// <summary>
    /// Atualiza o estado de ir para descanso.
    /// </summary>
    public void Update()
    {
        if(restSpot == null) return;

        if(!agent.navAgent.pathPending && 
        agent.navAgent.remainingDistance < 1f)
        {
            agent.fsm.ChangeState(new RestState(agent, restSpot));
        }
    }

    /// <summary>
    /// Executado quando o agente sai do estado de ir para descanso.
    /// </summary>
    public void Exit()
    {}
}
