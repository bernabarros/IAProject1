using UnityEngine;
/// <summary>
/// Representa um local de trabalho onde os agentes podem realizar tarefas para reduzir suas necessidades de trabalho.
/// </summary>
public class WorkSpot : MonoBehaviour
{
    /// <summary>
    /// Indica se o local de trabalho está ocupado por um agente.
    /// </summary>
    public bool IsOccupied { get; private set; }

    /// <summary>
    /// Marca o local de trabalho como ocupado por um agente.
     /// O agente deve chamar este método ao iniciar uma tarefa e chamar Release() ao finalizar para liberar o local para outros agentes.
     /// </summary>
     /// <returns>True se o local foi ocupado com sucesso, false se já estava ocupado.</returns>
    /// </summary>
    public void Occupy()
    {
        IsOccupied = true;
    }
    /// <summary>
    /// Marca o local de trabalho como desocupado, permitindo que outros agentes possam utilizá-lo.
     /// O agente deve chamar este método ao finalizar uma tarefa para liberar o local para outros agentes.
    /// </summary>
    public void Release()
    {
        IsOccupied = false;
    }
}
