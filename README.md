# IA Project1

## Group Members  

### Bernardo Barros a22401588

- Design of simulation base layout;
- Writing project report;

### Ivan Emídio a22301234

- Pathfinding implementation;
- Fixed State Machine implementation;

### Simão Durão a22408594

- Accident creation logic;
- Accident management State logic for simulation agents;
- Evacuation State logic for simulation agents;

## Introduction

In the context of our Artificial Intelligence course subject, we were proposed a project to develop a simulation in Unity that replicates the behavior of agents on a Mars habitat colony, in which the agents of the simulation would be required to perform work tasks on specific areas of the base as well as satisfy their need for rest. Agents would also be required to react to incidents in the base and act accordingly either by attempting to contain and resolve the incident or escaping through the emergency exits.

Research performed for project resolution:

- [^1]Layout of base was made by dividing work areas and rest areas, left side has the habitat and recharge stations and right side has the lab, storage and greenhouse, with airlocks connected to storage and greenhouse

## Methodology

The simulation was implemented in a Unity 3D scene, with the agents using Unity's NavMesh system (kinematic movement).

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

```mermaid

---
title: Crew Member FSM
---

stateDiagram-v2
    [*] --> DecideState
    DecideState --> GoToRestState : restScore highest > 20
    DecideState --> GoToResourceState : resourceScore highest > 20
    DecideState --> GoToWorkState : workScore highest > 20
    DecideState --> WanderState : else
    DecideState --> EvacuateState : evacuation trigger

    GoToRestState --> RestState : reached destination
    GoToRestState --> WanderState : no rest spot
    GoToRestState --> EvacuateState : evacuation trigger

    GoToResourceState --> ResourceState : reached destination
    GoToResourceState --> WanderState : no resource spot
    GoToResourceState --> EvacuateState : evacuation trigger

    GoToWorkState --> WorkState : reached destination
    GoToWorkState --> WanderState : no work spot
    GoToWorkState --> EvacuateState : evacuation trigger

    RestState --> DecideState : RestNeed <= 0
    RestState --> EvacuateState : evacuation trigger

    ResourceState --> DecideState : ResourceNeed <= 0
    ResourceState --> EvacuateState : evacuation trigger

    WorkState --> DecideState : WorkNeed <= 0
    WorkState --> EvacuateState : evacuation trigger

    WanderState --> DecideState : destination reached
    WanderState --> EvacuateState : evacuation trigger

    EvacuateState --> [*] : reached capsule (agent disabled)
```

```mermaid

---
title: Robot FSM
---

stateDiagram-v2
    [*] --> RobotDecideState

    RobotDecideState --> GoToChargeState : energy > 30
    RobotDecideState --> WanderState : else
    RobotDecideState --> ContainIncidentState : incident

    GoToChargeState --> ChargeState : reached destination
    GoToChargeState --> WanderState : no charging spot
    GoToChargeState --> ContainIncidentState : incident

    ChargeState --> RobotDecideState : EnergyNeed <= 0
    ChargeState --> ContainIncidentState : incident

    WanderState --> RobotDecideState : destination reached
    WanderState --> ContainIncidentState : incident

    ContainIncidentState --> RobotDecideState : all resolved
```

## Results and Discussion

The implementation of the simulation was successful, it containts the core requirements of the project. The combination of NavMesh system and FSM (Finite State Machine) provided a nice framework for agent autonomy

FSM allowed both Crew Members and Robots to act. Dinamically calculation scores for RestNeed, ResourceNeed and WorkNeed. Crew Members transitioned between modules without getting stuck. Robots managed their battery levels, prioritizing charging only when necessary.

The best aspect of this simulation was the dynamic response between emergencies. The Incident manager allowed randomized hazards to spawn and propagate. It has visual feedback for every hazard; Red for fire; Cyan for O2 Leak; Black for Power Failure.

In crisis, the robots transitioned to the ContainIncidentState, and navigated to the closest hazard. Crew Members avoided those hazardous zones. That was achieved by the NavMeshObstacle, and guided by EvacuateState.

The robots would enter an infinite recalculation loop when approaching an obstacle. This was resolved by destroying the NavMeshAgent upon an agent's death. And turning them into an obstacle creating a solid barrier.
## Conclusions


 [^1]:Zhong, Y., Wu, T., Han, Y., Wang, F., Zhao, D., Fang, Z., Pan, L., & Tang, C. (2025). Advancements in Mars Habitation Technologies and Terrestrial Simulation Projects: A Comprehensive Review. Aerospace, 12(6), 510. <https://doi.org/10.3390/aerospace12060510>