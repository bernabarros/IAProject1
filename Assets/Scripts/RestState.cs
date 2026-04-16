using UnityEngine;
/// <summary>
/// Estado responsável pelo descanso de um tripulante.
/// </summary>
public class RestState : IState
{
    /// <summary>
    /// Referencia ao agente associado ao estado.
    /// </summary>
    private CrewMember agent;

    private RestSpot restSpot;

    /// <summary>
    /// Construtor do estado de descanso.
    /// </summary>
    /// <param name="agent">Tripulante associado.</param>
    public RestState(CrewMember agent, RestSpot restSpot)
    {
        this.agent = agent;
        this.restSpot = restSpot;
    }
    /// <summary>
    /// Executado quando o agente entra no estado de descanso.
    /// </summary>
    public void Enter()
    {  
        Debug.Log("A descansar");
    }
    /// <summary>
    /// Atualiza o estado de descanso.
    /// Reduz a necessidade de descanso ao longo do tempo.
    /// </summary>
    public void Update()
    {
        agent.ReduceRest(Time.deltaTime * 10f);

        if (agent.RestNeed <= 0)
        {
            restSpot.Release();
            agent.fsm.ChangeState(new DecideState(agent));
        }
    }

    /// <summary>
    /// Executado quando o agente sai do estado.
    /// </summary>
    public void Exit()
    {
        Debug.Log("A sair do estado de descanso");
    }

}
