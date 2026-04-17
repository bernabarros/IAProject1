using UnityEngine;

public class ContainIncidentState : IState
{
    private Robot robot;
    private HazardZone incidentLoc;
    private float originalSpeed;

    private float containmentTimer = 0f;
    private float timeToContain = 5f;
    private float interactionRadius = 6f;

    public ContainIncidentState(Robot robot, HazardZone incidentLoc)
    {
        this.robot = robot;
        this.incidentLoc = incidentLoc;
    }

    public void Enter()
    {
        originalSpeed = robot.navAgent.speed;
        robot.navAgent.speed *= 1.5f;

        robot.navAgent.SetDestination(incidentLoc.transform.position);
    }

    public void Exit()
    {
        robot.navAgent.speed = originalSpeed;
        robot.navAgent.isStopped = false;
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
        if (incidentLoc == null || incidentLoc.GetCurrentHazard() == HazardType.None)
        {
            FinishContainment();
            return;
        }

        float distanceToTarget = Vector3.Distance(robot.transform.position, incidentLoc.transform.position);

        if (distanceToTarget <= interactionRadius)
        {
            robot.navAgent.isStopped = true;
            containmentTimer += Time.deltaTime;

            if (containmentTimer >= timeToContain)
            {
                incidentLoc.ResolveHazard();
                FinishContainment();
            }

        }
        else
        {
            robot.navAgent.isStopped = false;
        }
    }

    private void FinishContainment()
    {
        HazardZone closestZone = IncidentManager._Instance.NextHazard(robot.transform.position);

        if (closestZone != null)
        {
            robot.fsm.ChangeState(new ContainIncidentState(robot, closestZone));
        }
        else
        {
            robot.IncidentOver(false);
            robot.fsm.ChangeState(new RobotDecideState(robot));
        }

    }
}
