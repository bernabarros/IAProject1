using UnityEngine;

public class ContainIncidentState : IState
{
    private Robot robot;
    private Vector3 incidentLoc;
    private float originalSpeed;

    public ContainIncidentState(Robot robot, Vector3 incidentLoc)
    {
        this.robot = robot;
        this.incidentLoc = incidentLoc;
    }

    public void Enter()
    {
        originalSpeed = robot.navAgent.speed;
        robot.navAgent.speed *= 1.5f;

        robot.navAgent.SetDestination(incidentLoc);
    }

    public void Exit()
    {
        robot.navAgent.speed = originalSpeed;
    }

    /// <summary>
    /// Updates the robot state based on its navigation status.
    /// </summary>
    /// <remarks>This method should be called, to ensure the robot responds
    /// appropriately when it reaches its navigation destination. It checks the robot has completed its current
    /// path and is within the stopping distance, and can be to implement additional logic when the robot
    /// arrives</remarks>
    public void Update()
    {
        if(!robot.navAgent.pathPending && robot.navAgent.remainingDistance <= robot.navAgent.stoppingDistance)
        {
            // Implement Containment logic
        }
    }
}
