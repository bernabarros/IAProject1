using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Classe base abstrata para todos os agentes da simulação.
/// Contém a lógica comum como energia e a máquina de estados (FSM).
/// </summary>
public abstract class Agent : MonoBehaviour
{
    /// <summary>
    /// Variaveis de energia, valor maximo de energia e maquina de estados
    /// </summary>
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy = 100f;
    public FSM fsm;
    public NavMeshAgent navAgent;
  
    /// <summary>
    /// Inicializa a FSM do agente.
    /// </summary>
    protected virtual void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        fsm = new FSM();
    }
    
    /// <summary>
    /// Atualiza a FSM a cada frame.
    /// </summary>
    protected virtual void Update()
    {
        fsm.Update();
    }

}
