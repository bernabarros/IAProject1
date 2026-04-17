using UnityEngine;
/// <summary>
/// Estado de deslocaçao para trabalho.
/// </summary>
public class GoToWorkState : IState
{
    private CrewMember agent;

    private WorkSpot targetSpot;

    public GoToWorkState(CrewMember agent)
    {
        this.agent = agent;
    }
    public void Enter()
    {
        Debug.Log("Indo trabalhar");
        targetSpot = WorkManager.Instance.GetFreeSpot();

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
            agent.navAgent.remainingDistance < 1f)
        {
            agent.fsm.ChangeState(new WorkState(agent, targetSpot));
        }
        
    }
    
    public void Exit() 
    {}
}
