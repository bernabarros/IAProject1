using UnityEngine;
/// <summary>
/// Representa um robot autónomo da base.
/// Gere a sua própria bateria e tarefas.
/// </summary>
public class Robot : Agent
{
    /// <summary>
    /// Nivel atual da bateria do robot.
    /// </summary>
    [SerializeField] private float battery;

    /// <summary>
    /// O robo responde a um incidente.
    /// </summary>
    /// <param name="incidentPos"></param>
    public void RespondToIncident(Vector3 incidentPos)
    {
        fsm.ChangeState(new ContainIncidentState(this, incidentPos));
    }
}
