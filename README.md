# IA Project1

## Group Members  

### Bernardo Barros a22401588

- Design of simulation base layout;
- Writting project report;

### Ivan Emídio a22301234

- Pathfinding implementation;
- Fixed State Machine implementation;

### Simão Durão a22408594

- Accident creation logic;
- Accident management State logic for simulation agents;
- Evacuation State logic for simulation agents;

## Introduction

In the context of our Artificial Intelligence course subject, we were proposed a project to develop a simulation in Unity that replicates the behaviour of agents on a Mars habitat colony, in which the agents of the simulation would be required to perform work tasks on specific areas of the base as well as satisfy their need for rest. Agents would also be required to react to incidents in the base and act accordingly either by attempting to contain and resolve the incident or escaping through the emergency exits.

Research performed for project resolution:

-Layout of base: Zhong, Y., Wu, T., Han, Y., Wang, F., Zhao, D., Fang, Z., Pan, L., & Tang, C. (2025). Advancements in Mars Habitation Technologies and Terrestrial Simulation Projects: A Comprehensive Review. Aerospace, 12(6), 510. <https://doi.org/10.3390/aerospace12060510>

## Methodology

```mermaid

---
title: UML Diagram
---

classDiagram
    class Agent{<<abstract>>}
    Agent --|> MonoBehaviour
    Agent --> FSM
    Agent --> NavMeshAgent
    class ContainIncidentState
    ContainIncidentState ..|> IState
    ContainIncidentState --> Robot
    ContainIncidentState --> Transform
    ContainIncidentState ..> DecideState
    ContainIncidentState ..> EvacuateState
    class CrewMember
    CrewMember --|> Agent
    CrewMember --> Transform
    CrewMember ..> DecideState
    CrewMember ..> EvacuateState
    class HazardType{<<enumeration>>}
    class DecideState
    DecideState ..|> IState
    DecideState --> CrewMember
    DecideState ..> GoToRestState
    DecideState ..> GoToResourceState
    DecideState ..> GoToWorkState
    DecideState ..> WanderState
    class EvacuateState
    EvacuateState ..|> IState
    EvacuateState --> Agent
    class FSM
    FSM --> IState
    class GoToRestState
    GoToRestState ..|> IState
    GoToRestState --> CrewMember
    GoToRestState --> RestSpot
    GoToRestState ..> RestManager
    GoToRestState ..> WanderState
    GoToRestState ..> RestState
    class GoToChargeState
    GoToChargeState ..|> IState
    GoToChargeState --> Robot
    GoToChargeState --> ChargingSpot
    GoToChargeState ..> WanderState
    GoToChargeState ..> ChargeState
    class GoToResourceState
    GoToResourceState ..|> IState
    GoToResourceState --> CrewMember
    GoToResourceState --> ResourceSpot
    GoToResourceState ..> ResourceState
    class GoToWorkState
    GoToWorkState ..|> IState
    GoToWorkState --> CrewMember
    GoToWorkState --> WorkSpot
    GoToWorkState ..> WorkManager
    GoToWorkState ..> WanderState
    GoToWorkState ..> WorkState
    class IState{<<interface>>}
    class IncidentManager
    IncidentManager --|> MonoBehaviour
    IncidentManager --o HazardZone
    IncidentManager ..> HazardType
    class RestManager
    RestManager --|> MonoBehaviour
    RestManager --o RestSpot
    class ChargingManager
    ChargingManager --|> MonoBehaviour
    ChargingManager --o ChargingSpot
    class ResourceManager
    ResourceManager --|> MonoBehaviour
    ResourceManager --o ResourceSpot
    class RestManager
    RestManager --|> MonoBehaviour
    RestManager --o RestSpot
    class SpawnManager
    SpawnManager --|> MonoBehaviour
    SpawnManager --> GameObject
    class WorkManager
    WorkManager --|> MonoBehaviour
    WorkManager --o WorkSpot
    class ChargingSpot
    ChargingSpot --|> MonoBehaviour
    class RestSpot
    RestSpot --|> MonoBehaviour
    class ResourceSpot
    ResourceSpot --|> MonoBehaviour
    class RestState
    RestState ..|> IState
    RestState --> CrewMember
    RestState --> RestSpot
    RestState ..> DecideState
    class Robot
    Robot --|> MonoBehaviour
    Robot ..> RobotDecideState
    Robot ..> ContainIncidentState
    class RobotDecideState
    RobotDecideState ..|> IState
    RobotDecideState --> Robot
    RobotDecideState ..> GoToChargeState
    RobotDecideState ..> WanderState
    class WanderState
    WanderState ..|> IState
    WanderState --> Agent
    WanderState ..> DecideState
    WanderState ..> CrewMember
    class ChargeState
    ChargeState ..|> IState
    ChargeState --> Robot
    ChargeState --> ChargingSpot
    ChargeState ..> RobotDecideState
    class ResourceState
    ResourceState ..|> IState
    ResourceState --> CrewMember
    ResourceState --> ResourceSpot
    ResourceState ..> DecideState
    class WorkState
    WorkState ..|> IState
    WorkState --> CrewMember
    WorkState --> WorkSpot
    WorkState ..> DecideState
    class HazardZone
    HazardZone --|> MonoBehaviour
    HazardZone --> HazardType
    HazardZone ..> Agent
    HazardZone ..> CrewMember
```



## Results and Discussion

## Conclusions