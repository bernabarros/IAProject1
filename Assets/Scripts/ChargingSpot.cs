using UnityEngine;
/// <summary>
/// Ponto de carregamento de robos.
/// </summary>
public class ChargingSpot : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    public void Occupy()
    {
        IsOccupied = true;
    }

    public void Release()
    {
        IsOccupied = false;
    }
}