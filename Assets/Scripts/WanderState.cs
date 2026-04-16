using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Estado onde o agente se move aleatoriamente pelo mapa.
/// </summary>
public class WanderState : IState
{
    private Agent agent;
    private float wanderRadius = 10f;
    private float timer;
    private float maxTimer = 3f;

    /// <summary>
    /// Construtor do estado de wander.
    /// </summary>
    /// <param name="agent"></param>
    public WanderState(Agent agent)
    {
        this.agent = agent;
    }

    /// <summary>
    /// executado ao entrar no estado.
    /// </summary>
    public void Enter()
    {
        SetNewDestination();
    }

    /// <summary>
    /// Atualiza o estado de wander
    /// </summary>
    public void Update()
    {
        // Se chegou ao destino
        if (!agent.navAgent.pathPending &&
            agent.navAgent.remainingDistance < 1f)
        {
            /// <summary>
            /// Quando chega ao destino, volta a decidir.
            /// </summary>
            agent.fsm.ChangeState(new DecideState((CrewMember)agent));
        }
    }

    /// <summary>
    /// Define um novo destino aleatorio dentro do navmesh
    /// </summary>
    private void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += agent.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
        {
            agent.navAgent.SetDestination(hit.position);
        }
    }

    /// <summary>
    /// Executado ao sair do estado.
    /// </summary>
    public void Exit()
    {
        // Nada a fazer ao sair do estado de wander
    }
}
