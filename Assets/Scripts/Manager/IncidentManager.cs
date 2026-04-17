using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class IncidentManager : MonoBehaviour
{
    public static IncidentManager _Instance { get; private set; }

    [SerializeField] private List<HazardZone> allZones;

    [SerializeField] private List<HazardZone> activeHazards = new List<HazardZone>();

    private void Awake()
    {
        _Instance = this;

        allZones = new List<HazardZone>(FindObjectsByType<HazardZone>(FindObjectsSortMode.None));

    }

    private void Update()
    {
        if(Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            Debug.Log("Triggering random incident...");
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

    private void RegisterActiveHazard(HazardZone zone)
    {
        if (!activeHazards.Contains(zone)) activeHazards.Add(zone);
    }

    private void RemoveActiveHazard(HazardZone zone)
    {
        if (activeHazards.Contains(zone)) activeHazards.Remove(zone);
    }

    private HazardZone GetNextHazard(Vector3 robotPosition)
    {
        if (activeHazards.Count == 0) return null;
        
        HazardZone closestZone = null;
        float minDistance = Mathf.Infinity;

        foreach(HazardZone zone in activeHazards)
        {
            float distance = Vector3.Distance(robotPosition, zone.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestZone = zone;
            }
        }

        return closestZone;
    }

    public void ActiveHazard(HazardZone zone) => RegisterActiveHazard(zone);

    public void RemoveHazard(HazardZone zone) => RemoveActiveHazard(zone);

    public HazardZone NextHazard(Vector3 robotPosition) => GetNextHazard(robotPosition);
}
