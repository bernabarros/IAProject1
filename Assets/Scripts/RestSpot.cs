using UnityEngine;
/// <summary>
/// Representa um ponto de descanso na base onde os tripulantes podem ir para reduzir sua necessidade de descanso.
/// </summary>
public class RestSpot : MonoBehaviour
{
    /// <summary>
    /// Indica se o ponto de descanso está ocupado por um agente.
    /// </summary>
    public bool IsOccupied { get; private set; }

    /// <summary>
    /// Marca o ponto de descanso como ocupado.
    /// </summary>
    public void Occupy()
    {
        IsOccupied = true;
    }

    /// <summary>
    /// Libera o ponto de descanso, marcando-o como disponível para outros agentes.
    /// </summary>
    public void Release()
    {
        IsOccupied = false;
    }
}
