using UnityEngine;
/// <summary>
/// Representa um ponto de recolha de recursos.
/// </summary>
public class ResourceSpot : MonoBehaviour
{
    /// <summary>
    /// Indica se o ponto de recolha de recursos está ocupado por um agente.
    /// </summary>
    public bool IsOccupied { get; private set; }

    /// <summary>
    /// Marca o ponto de recolha de recursos como ocupado.
    /// </summary>
    public void Occupy()
    {
        IsOccupied = true;
    }

    /// <summary>
    /// Libera o ponto de recolha de recursos, marcando-o como disponível para outros agentes.
    /// </summary>
    public void Release()
    {
        IsOccupied = false;
    }
}
