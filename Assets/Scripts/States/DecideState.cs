using UnityEngine;
/// <summary>
/// Estado responsável por decidir a próxima ação de um agente com base nas suas necessidades.
/// </summary>
public class DecideState : IState
{
    private CrewMember agent;

    /// <summary>
    /// Construtor do estado de decisão.
    /// </summary>
    /// <param name="agent"></param>
    public DecideState(CrewMember agent)
    {
        this.agent = agent;
    }

    /// <summary>
    /// Executado quando o agente entra no estado de decisão.
    /// </summary>
    public void Enter()
    {
        float randomFactor = Random.Range(0.9f, 1.1f);

        float restScore = agent.GetRestPriority() * randomFactor;
        float resourceScore = agent.GetResourcePriority() * randomFactor;
        float work = agent.GetWorkPriority() * randomFactor;

        float highest = Mathf.Max(restScore, resourceScore, work);

        if (highest == restScore && restScore > 20f)
        {
            agent.fsm.ChangeState(new GoToRestState(agent));
        }
        else if (highest == resourceScore && resourceScore > 20f)
        {
            agent.fsm.ChangeState(new GoToResourceState(agent));
        }
        else if (highest == work && work > 20f)
        {
            agent.fsm.ChangeState(new GoToWorkState(agent));
        }
        else
        {
            agent.fsm.ChangeState(new WanderState(agent));
        }
    }
    
    /// <summary>
    /// Atualiza o estado de decisão, verificando se as necessidades do agente mudaram e tomando uma nova decisão se necessário.
    /// </summary>
    public void Update()
    {}

    public void Exit()
    {}
}
