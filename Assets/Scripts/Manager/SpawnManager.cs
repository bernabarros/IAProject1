using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Responsável por criar agentes na cena.
/// </summary>
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject crewPrefab;
    [SerializeField] private GameObject robotPrefab;

    [SerializeField] private int totalAgents = 20;
    [SerializeField] private int robotCount = 2;

    [SerializeField] private float spawnRadius = 10f;

    /// <summary>
    /// Spawna agentes na cena.
    /// </summary>
    public void SpawnAgents()
    {
        int crewCount = totalAgents - robotCount;

        // Spawn Crew
        for (int i = 0; i < crewCount; i++)
        {
            SpawnAgent(crewPrefab);
        }

        // Spawn Robots
        for (int i = 0; i < robotCount; i++)
        {
            SpawnAgent(robotPrefab);
        }
    }

    /// <summary>
    /// Cria um agente numa posição válida do NavMesh.
    /// </summary>
    private void SpawnAgent(GameObject prefab)
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;

        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas))
        {
            Instantiate(prefab, hit.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Não encontrou posição válida no NavMesh");
        }
    }
}