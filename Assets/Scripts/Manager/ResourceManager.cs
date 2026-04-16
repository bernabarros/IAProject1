using UnityEngine;
using System.Linq;
/// <summary>
/// Gere pontos de recursos
/// </summary>

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private ResourceSpot[] spots;

    /// <summary>
    /// Inicializa o ResourceManager e encontra todos os pontos de recolha de recursos na cena.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        spots = FindObjectsByType<ResourceSpot>(FindObjectsSortMode.None);
    }

    /// <summary>
    /// Obtém um ponto de recolha de recursos livre para um agente usar.
    /// </summary>
    /// <returns></returns>
    public ResourceSpot GetFreeSpot()
    {
        return spots.FirstOrDefault(s => !s.IsOccupied);
    }
}
