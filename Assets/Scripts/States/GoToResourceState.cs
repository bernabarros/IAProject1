using System.Resources;
using UnityEngine;

/// <summary>
/// Estado onde o agente vai até um ponto de recolha de recursos para reduzir sua necessidade de recursos.
/// </summary>
public class GoToResourceState : IState
{
    private CrewMember agent;

    private ResourceSpot targetSpot;

    public GoToResourceState(CrewMember agent)
    {
        this.agent = agent;
    }

    public void Enter()
    {
        targetSpot = ResourceManager.Instance.GetFreeSpot();

        if(targetSpot == null)
        {
            agent.fsm.ChangeState(new WanderState(agent));
            return;
        }

        targetSpot.Occupy();
        agent.navAgent.SetDestination(targetSpot.transform.position);
    }

    public void Update()
    {
        if(targetSpot == null) return;

        if(!agent.navAgent.pathPending && 
        agent.navAgent.remainingDistance <= 1f)
        {
            agent.fsm.ChangeState(new ResourceState(agent, targetSpot));
        }
    }

    public void Exit()
    {}

}
