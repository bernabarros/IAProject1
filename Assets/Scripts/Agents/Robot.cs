using UnityEngine;
/// <summary>
/// Representa um robot autónomo da base.
/// Gere a sua própria bateria e tarefas.
/// </summary>
public class Robot : Agent
{
    /// <summary>
    /// Necessidade de energia (bateria).
    /// </summary>
    public float EnergyNeed { get; private set; }

    [SerializeField] private float energyWeight = 1.5f;

    [SerializeField] private float energyRate;

    private bool isDealingWithIncident = false;

    /// <summary>
    /// Inicializa a FSM do robô com energia randomizada e define o estado inicial.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        
        EnergyNeed = Random.Range(0f, 100f);
        energyRate = Random.Range(5f, 10f);

        fsm.ChangeState(new RobotDecideState(this));
    }

    /// <summary>
    /// Aumenta a necessidade de energia ao longo do tempo.
    /// </summary>
    protected override void Update()
    {
        base.Update();
        UpdateNeeds();
    }

    /// <summary>
    /// Atualiza a bateria ao longo do tempo.
    /// </summary>
    private void UpdateNeeds()
    {
        EnergyNeed += Time.deltaTime * energyRate;
    }

    /// <summary>
    /// Reduz a necessidade de energia.
    /// </summary>
    public void Recharge(float amount)
    {
        EnergyNeed -= amount;
        EnergyNeed = Mathf.Max(EnergyNeed, 0);
    }

    /// <summary>
    /// Prioridade de energia.
    /// </summary>
    public float GetEnergyPriority()
    {
        return EnergyNeed * energyWeight;
    }
    public void RespondToIncident(HazardZone zone)
    {
        if(!isDealingWithIncident)
        {
            isDealingWithIncident = true;
            fsm.ChangeState(new ContainIncidentState(this, zone));
        }
            
    }

    public void IncidentOver(bool isResolved)
    {
        isDealingWithIncident = isResolved;
    }
}
