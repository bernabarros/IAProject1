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

    public float workNeed { get; private set;}

    [SerializeField] private float workWeight = 1.1f;
    [SerializeField] private float restWeight = 1.2f;
    [SerializeField] private float resourceWeight = 1.0f;

    [SerializeField] private float restRate;
    [SerializeField] private float resourceRate;
    [SerializeField] private float workRate;

    /// <summary>
    /// Ponto de descanso atribuído ao agente.
    /// </summary>
    [SerializeField] private Transform restPoint;

    /// <summary>
    /// Acesso ao ponto de descanso.
    /// </summary>
    public Transform RestPoint => restPoint;

    /// <summary>
    /// Inicializa a FSM do tripulante e define o estado inicial.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        RestNeed = Random.Range(0f, 100f);
        ResourceNeed = Random.Range(0f, 100f);
        workNeed = Random.Range(0f, 100f);

        restRate = Random.Range(3f, 7f);
        resourceRate = Random.Range(2f, 5f);
        workRate = Random.Range(3f, 6f);

        fsm.ChangeState(new DecideState(this));
    }

    /// <summary>
    /// Aumenta a necessidade de descanso ao longo do tempo.
    /// </summary>
    private void UpdateNeeds()
    {
        RestNeed += Time.deltaTime * restRate;
        ResourceNeed += Time.deltaTime * resourceRate;
        workNeed += Time.deltaTime * workRate;
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
    /// Inicializa a evacuacao do agente.
    /// </summary>
    public void TriggerEvacuation()
    {
        fsm.ChangeState(new EvacuateState(this));
    }

    /// <summary>
    /// Reduz a necessidade de recursos.
    /// </summary>
    /// <param name="amount"></param>
    public void ReduceResource(float amount)
    {
        ResourceNeed -= amount;
        ResourceNeed = Mathf.Max(ResourceNeed, 0);
    }

    /// <summary>
    /// Reduz a necessidade de trabalho.
    /// </summary>
    /// <param name="amount"></param>
    public void ReduceWork(float amount)
    {
        workNeed -= amount;
        workNeed = Mathf.Max(workNeed, 0);
    }

    /// <summary>
    /// Obtém a prioridade de trabalho com base na necessidade atual.
    /// </summary>
    /// <returns></returns>
    public float GetWorkPriority()
    {
        return workNeed * workWeight;
    }

    /// <summary>
    /// Obtém a prioridade de descanso com base na necessidade atual.
    /// </summary>
    /// <returns></returns>
    public float GetRestPriority()
    {
        return RestNeed * restWeight;
    }

    /// <summary>
    /// Obtém a prioridade de recursos com base na necessidade atual.
    /// </summary>
    /// <returns></returns>
    public float GetResourcePriority()
    {
        return ResourceNeed * resourceWeight;
    }
}
