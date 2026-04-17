using UnityEngine;

/// <summary>
/// Estado de trabalho onde o agente realiza tarefas para reduzir sua necessidade de trabalho.
/// </summary>
public class WorkState : IState
{
    private CrewMember agent;
    private WorkSpot spot;

    public WorkState(CrewMember agent, WorkSpot spot)
    {
        this.agent = agent;
        this.spot = spot;
    }

    public void Enter()
    {
        Debug.Log("Trabalhando");
    }

    public void Update()
    {
        agent.ReduceWork(Time.deltaTime * 10f);

        if (agent.workNeed <= 0f)
        {
            spot.Release();
            agent.fsm.ChangeState(new DecideState(agent));
        }
    }

    public void Exit()
    {
        Debug.Log("Terminou de trabalhar");
    }
}
