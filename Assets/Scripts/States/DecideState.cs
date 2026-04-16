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
        if (agent.RestNeed >= 70f)
        {
            agent.fsm.ChangeState(new GoToRestState(agent));
        }
        else if (agent.ResourceNeed >= 60f)
        {
            agent.fsm.ChangeState(new GoToResourceState(agent));
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
