using UnityEngine;
/// <summary>
/// Representa um tripulante humano da base.
/// Possui necessidades como descanso e recursos.
/// </summary>
public class CrewMember : Agent
{
    /// <summary>
    /// Necessidade de descanso do agente.
    /// </summary>
    public float RestNeed { get; private set; }
    /// <summary>
    /// Necessidade de recursos do agente.
    /// </summary>
    public float ResourceNeed { get; private set; }

    /// <summary>
    /// Inicializa a FSM do tripulante e define o estado inicial.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        fsm.ChangeState(new DecideState(this));
    }

    /// <summary>
    /// Aumenta a necessidade de descanso ao longo do tempo.
    /// </summary>
    private void UpdateNeeds()
    {
        RestNeed += Time.deltaTime * 2f;
    }

    /// <summary>
    /// Reduz a necessidade de descanso.
    /// </summary>
    public void ReduceRest(float amount)
    {
        RestNeed -= amount;
        RestNeed = Mathf.Max(RestNeed, 0);
    }

    protected override void Update()
    {
        base.Update();
        UpdateNeeds();
    }

    /// <summary>
    /// Inicializa a evacuação do agente.
    /// </summary>
    public void TriggerEvacuation()
    {
        fsm.ChangeState(new EvacuateState(this));
    }
}
