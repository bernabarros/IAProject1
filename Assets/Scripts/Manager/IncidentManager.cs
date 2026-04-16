using UnityEngine;
using System.Collections.Generic;

public class IncidentManager : MonoBehaviour
{
    public static IncidentManager _Instance { get; private set; }

    [SerializeField] private List<HazardZone> allZones;

    private void Awake()
    {
        _Instance = this;

        allZones = new List<HazardZone>(FindObjectsOfType<HazardZone>());

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            TriggerRandomIncident();
        }
    }

    private void TriggerRandomIncident()
    {
        if (allZones.Count == 0)
        {
            return;
        }

        HazardZone startZone = allZones[Random.Range(0, allZones.Count)];

        HazardType randomHazard = (HazardType)Random.Range(1, 3);

        startZone.ActivateHazard(randomHazard);
    }
}
