/// <summary>
/// Estado de deslocação para carregamento.
/// </summary>
public class GoToChargeState : IState
{
    private Robot agent;
    private ChargingSpot targetSpot;

    public GoToChargeState(Robot agent)
    {
        this.agent = agent;
    }

    /// <summary>
    /// Ao entrar, o agente procura um ponto de carregamento livre.
    /// </summary>
    public void Enter()
    {
        targetSpot = ChargingManager.Instance.GetFreeSpot();

        if (targetSpot == null)
        {
            agent.fsm.ChangeState(new WanderState(agent));
            return;
        }

        targetSpot.Occupy();
        agent.navAgent.SetDestination(targetSpot.transform.position);
    }

    /// <summary>
    /// Durante a atualização, o agente verifica se chegou ao ponto de carregamento. Se sim, muda para o estado de carregamento.
    /// </summary>
    public void Update()
    {
        if (targetSpot == null) return;

        if (!agent.navAgent.pathPending &&
            agent.navAgent.remainingDistance < 1f)
        {
            agent.fsm.ChangeState(new ChargeState(agent, targetSpot));
        }
    }

    public void Exit() { }
}