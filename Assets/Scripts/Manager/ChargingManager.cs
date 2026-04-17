using System.Linq;
using UnityEngine;

/// <summary>
/// Gere pontos de carregamento.
/// </summary>
public class ChargingManager : MonoBehaviour
{
    public static ChargingManager Instance { get; private set; }

    private ChargingSpot[] spots;

    private void Awake()
    {
        Instance = this;
        spots = FindObjectsByType<ChargingSpot>(FindObjectsSortMode.None);
    }

    public ChargingSpot GetFreeSpot()
    {
        return spots.FirstOrDefault(s => !s.IsOccupied);
    }
}