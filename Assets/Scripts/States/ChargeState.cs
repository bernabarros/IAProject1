using UnityEngine;

/// <summary>
/// Estado de carregamento do robo.
/// </summary>
public class ChargeState : IState
{
    private Robot agent;
    private ChargingSpot spot;

    public ChargeState(Robot agent, ChargingSpot spot)
    {
        this.agent = agent;
        this.spot = spot;
    }

    public void Enter() { }

    public void Update()
    {
        agent.Recharge(Time.deltaTime * 15f);

        if (agent.EnergyNeed <= 0)
        {
            spot.Release();
            agent.fsm.ChangeState(new RobotDecideState(agent));
        }
    }

    public void Exit() { }
}
