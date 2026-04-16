using UnityEngine;
/// <summary>
/// Estado onde o agente está recolhendo recursos para reduzir sua necessidade de recursos.
/// </summary>

public class ResourceState : IState
{
    private CrewMember agent;

    private ResourceSpot spot;

    public ResourceState(CrewMember agent, ResourceSpot spot)
    {
        this.agent = agent;
        this.spot = spot;
    }

    public void Enter()
    {
        Debug.Log("a obter recursos");
    }

    public void Update()
    {
        agent.ReduceResource(Time.deltaTime * 10f);

        if(agent.ResourceNeed <= 0f)
        {
            spot.Release();
            agent.fsm.ChangeState(new DecideState(agent));
        }
    }

    public void Exit()
    {
        Debug.Log("ja tenho recursos");
    }
}
