using UnityEngine;
using System.Linq;
/// <summary>
/// Gere pontos de trabalho
/// </summary>
public class WorkManager : MonoBehaviour
{
    /// <summary>
    /// Instância singleton do WorkManager para fácil acesso por outros scripts.
    /// </summary>
    public static WorkManager Instance { get; private set; }

    private WorkSpot[] spots;

    /// <summary>
    /// Inicializa a instância singleton e coleta todos os pontos de trabalho na cena para gerenciamento.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        spots = FindObjectsByType<WorkSpot>(FindObjectsSortMode.None);
    }

    /// <summary>
    /// Retorna um local de trabalho livre para um agente usar.
    /// <returns></returns>
    public WorkSpot GetFreeSpot()
    {
        return spots.FirstOrDefault(s => !s.IsOccupied);
    }
}
