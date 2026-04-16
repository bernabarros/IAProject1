using UnityEngine;
using System.Linq;

/// <summary>
/// Gerencia os pontos de descanso na base, garantindo que os agentes possam encontrar um local disponível para descansar quando necessário.
/// </summary>
public class RestManager : MonoBehaviour
{
    /// <summary>
    /// Instância singleton do RestManager para fácil acesso por outros scripts.
    /// </summary>
    public static RestManager Instance { get; private set;}

    /// <summary>
    /// Array de pontos de descanso disponíveis na base.
    /// </summary>
    private RestSpot[] spots;

    /// <summary>
    /// Inicializa o RestManager.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        spots = FindObjectsOfType<RestSpot>();
    }

    /// <summary>
    /// Retorna um ponto de descanso livre, ou null se todos estiverem ocupados.
    /// </summary>
    /// <returns></returns>
    public RestSpot GetFreeSpot()
    {
        return spots.FirstOrDefault(s => !s.IsOccupied);
    }
}
