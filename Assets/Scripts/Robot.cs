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
}
