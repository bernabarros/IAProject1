using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;

public class HazardZone : MonoBehaviour
{
    [SerializeField] private HazardType _currentHazard = HazardType.None;

    [SerializeField] private List<HazardZone> _adjacentZones;
    [SerializeField] private float _propagationTime = 15f;

    private NavMeshObstacle _navObstacle;
    private Color _originalColor;

    /// <summary>
    /// Gets the NavMeshObstacle component and initializes it if the hazard is already active at the start
    /// </summary>
    private void Awake()
    {
        _navObstacle = GetComponent<NavMeshObstacle>();
        Renderer zoneRender = GetComponent<Renderer>();
        if (zoneRender != null)
        {
            _originalColor = zoneRender.material.color;
        }
    }
    /// <summary>
    /// Activate Hazard and starts to propagate to zones
    /// </summary>
    /// <param name="hazard"></param>
    public void ActivateHazard(HazardType hazard)
    {
        if (_currentHazard != HazardType.None) return;

        _currentHazard = hazard;


        Renderer zoneRender = GetComponent<Renderer>();
        if (zoneRender != null)
        {
            switch (hazard)
            {
                case HazardType.Fire:
                    zoneRender.material.color = Color.red;
                    break;
                case HazardType.O2Leak:
                    zoneRender.material.color = Color.cyan;
                    break;
                case HazardType.PowerFailure:
                    zoneRender.material.color = Color.black;
                    break;
            }
        }

        if ((hazard == HazardType.Fire || hazard == HazardType.O2Leak) && _navObstacle != null)
        {
            _navObstacle.enabled = true;
        }

        IncidentManager._Instance.ActiveHazard(this);

        TriggerAlarm();

        StartCoroutine(PropagateHazard());
    }

    /// <summary>
    /// Coroutine that waits for a specified time before propagating the hazard
    /// </summary>
    /// <returns></returns>
    private IEnumerator PropagateHazard()
    {
        yield return new WaitForSeconds(_propagationTime);

        foreach (HazardZone zone in _adjacentZones)
        {
            if (zone._currentHazard == HazardType.None)
            {
                zone.ActivateHazard(_currentHazard);
            }
        }
    }

    /// <summary>
    /// Kills a agent during an hazard
    /// </summary>
    /// <param name="agent"></param>
    private void KillAgent(Agent agent)
    {
        if (!agent.enabled) return;

        if(_currentHazard == HazardType.Fire)
        {
            ExecuteDeath(agent) ;
        }
        else if (_currentHazard == HazardType.O2Leak)
        {
            if(agent is CrewMember)
            {
                ExecuteDeath(agent);
            }
        }
    }

    /// <summary>
    /// Method that handles the death of an agent
    /// </summary>
    /// <param name="agent"></param>
    private void ExecuteDeath(Agent agent)
    {
        agent.fsm.ChangeState(null);
        agent.navAgent.enabled = false;
        agent.enabled = false;
        

        NavMeshObstacle corpseObstacle = agent.gameObject.AddComponent<NavMeshObstacle>();
        corpseObstacle.shape = NavMeshObstacleShape.Capsule;
        corpseObstacle.carving = true;

    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if (_currentHazard == HazardType.None) return;

        Agent agent = other.GetComponent<Agent>();

        if(agent != null)
        {
            KillAgent(agent);
        }
        */
         

        if (_currentHazard == HazardType.None) return;

        Agent agent = other.GetComponentInParent<Agent>();

        if(agent != null)
        {
            
            KillAgent(agent);
        }
    }

    public HazardType GetCurrentHazard()
    {
        return _currentHazard;
    }

    private void ResolvingHazard()
    {
        if (_currentHazard == HazardType.None) return;

        _currentHazard = HazardType.None;

        if(_navObstacle != null)
        {
            _navObstacle.enabled = false;
        }
        
        Renderer zoneRenderer = GetComponent<Renderer>();
        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = _originalColor;
        }

        IncidentManager._Instance.RemoveHazard(this);
    }

    private void TriggerAlarm()
    {
        Robot[] allRobot = FindObjectsOfType<Robot>();

        foreach(Robot robot in allRobot)
        {
            robot.RespondToIncident(this);

            Debug.Log("a responder ao incidente");
        }

        CrewMember[] allCrew = FindObjectsOfType<CrewMember>();
        foreach (CrewMember crew in allCrew)
        {
            if (crew.enabled)
            {
                Vector3 currentDest = crew.navAgent.destination;

                crew.navAgent.ResetPath();

                crew.navAgent.SetDestination(currentDest);
            }
        }
    }

    public void ResolveHazard()
    {
        ResolvingHazard();
    }

    
}


