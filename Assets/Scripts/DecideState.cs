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
        MakeDecision();
    }

    public void Update()
    {}

    public void Exit()
    {}

    /// <summary>
    /// Toma uma decisão com base nas necessidades do agente.
    /// </summary>
    private void MakeDecision()
    {
        if (agent.RestNeed > 50)
        {
            agent.fsm.ChangeState(new RestState(agent));
        }
        else
        {
            agent.fsm.ChangeState(new WanderState(agent));
        }
    }
}
