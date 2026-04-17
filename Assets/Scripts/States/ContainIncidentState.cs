using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Estado responsável por levar o robô até um incidente e resolvê-lo.
/// </summary>
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
        if (robot.navAgent == null || !robot.navAgent.enabled) return;

        /// <summary>
        /// Aumenta a velocidade do robô durante resposta ao incidente.
        /// </summary>
        originalSpeed = robot.navAgent.speed;
        robot.navAgent.speed = originalSpeed * 1.5f;

        SetSafeDestination();
    }

    public void Exit()
    {
        if (robot.navAgent == null) return;

        robot.navAgent.speed = originalSpeed;
        robot.navAgent.isStopped = false;
    }

    /// <summary>
    /// Atualiza o comportamento do robô durante o incidente.
    /// </summary>
    public void Update()
    {
        if (robot.navAgent == null || !robot.navAgent.enabled) return;

        /// <summary>
        /// Se o incidente já não existir, termina.
        /// </summary>
        if (incidentLoc == null || incidentLoc.GetCurrentHazard() == HazardType.None)
        {
            FinishContainment();
            return;
        }

        float distanceToTarget = Vector3.Distance(robot.transform.position, incidentLoc.transform.position);

        /// <summary>
        /// Se estiver perto o suficiente, começa a resolver o incidente.
        /// </summary>
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

            /// <summary>
            /// Recalcula constantemente o destino para evitar ficar preso.
            /// </summary>
            SetSafeDestination();
        }
    }

    /// <summary>
    /// Define um ponto seguro perto do incidente no NavMesh.
    /// </summary>
    private void SetSafeDestination()
    {
        if (incidentLoc == null) return;

        Vector3 direction = (incidentLoc.transform.position - robot.transform.position).normalized;
        Vector3 safePoint = incidentLoc.transform.position - direction * 2f;

        if (NavMesh.SamplePosition(safePoint, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            robot.navAgent.SetDestination(hit.position);
        }
    }

    /// <summary>
    /// Finaliza o tratamento do incidente e decide o próximo passo.
    /// </summary>
    private void FinishContainment()
    {
        HazardZone closestZone = IncidentManager._Instance.NextHazard(robot.transform.position);

        if (closestZone != null)
        {
            robot.fsm.ChangeState(new ContainIncidentState(robot, closestZone));
        }
        else
        {
            robot.IncidentOver();
            incidentLoc.ReleaseRobot();
            robot.fsm.ChangeState(new RobotDecideState(robot));
        }
    }
}