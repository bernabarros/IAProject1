using UnityEngine;

public class EvacuateState : IState
{
    private Agent _agent;
    private float originalSpeed;


    /// <summary>
    /// Initializes a new instance of the EvacuateState class for the agent
    /// </summary>
    /// <param name="agent">The agent for which the evacuation state is being created</param>
    public EvacuateState(Agent agent)
    {
        this._agent = agent;
    }

    /// <summary>
    /// Increases the agent navigation speed by 50 percent entering a specific state
    /// </summary>
    /// <remarks>This method should be called when the agent transitions into a state that requires faster
    /// movement. The original speed is preserved and can be restored when exiting the state</remarks>
    void IState.Enter()
    {
        originalSpeed = _agent.navAgent.speed;
        _agent.navAgent.speed *= 1.5f;


        GameObject[] caps = GameObject.FindGameObjectsWithTag("Capsules");
        Transform closestCapsule = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject capsule in caps)
        {
            float distance = Vector3.Distance(_agent.transform.position, capsule.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCapsule = capsule.transform;
            }
        }

        if (closestCapsule != null)
        {
            _agent.navAgent.SetDestination(closestCapsule.position);
        }
    }


    /// <summary>
    /// Updates the agent state based on its navigation progress
    /// </summary>
    /// <remarks>Deactivates the agent game object when the navigation agent has reached its destination and
    /// is no longer pending a path. This method is intended to be called, within a game loop or
    /// update cycle</remarks>
    void IState.Update()
    {
        if(!_agent.navAgent.pathPending && _agent.navAgent.remainingDistance <= _agent.navAgent.stoppingDistance)
        {
            _agent.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// Restores the agent navigation speed to its original value.
    /// </summary>
    /// <remarks>Call this method to reset any temporary speed modifications applied to the agent's navigation
    /// component. This is typically used after an operation that required changing the agent's speed, ensuring
    /// consistent movement behavior.</remarks>
    void IState.Exit()
    {
        _agent.navAgent.speed = originalSpeed;
    }
  
}
