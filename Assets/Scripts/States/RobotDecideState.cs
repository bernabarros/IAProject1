using UnityEngine;

/// <summary>
/// Estado de decisão do robô.
/// </summary>
public class RobotDecideState : IState
{
    private Robot agent;

    public RobotDecideState(Robot agent)
    {
        this.agent = agent;
    }

    public void Enter()
    {
        float energy = agent.GetEnergyPriority();

        if (energy > 30f)
        {
            agent.fsm.ChangeState(new GoToChargeState(agent));
        }
        else
        {
            agent.fsm.ChangeState(new WanderState(agent));
        }
    }

    public void Update() { }
    public void Exit() { }
}
