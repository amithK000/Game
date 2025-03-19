# Truck vs. Zombies: Arlington Rampage - Architecture

## Core Components

### Game Management
- **GameManager.cs**: Singleton pattern manager handling game state, pause functionality, and core game loop.

### Enemy System
- **ZombieAI.cs**: Controls individual zombie behavior using NavMeshAgent for pathfinding.
  - Requires NavMeshAgent component for navigation
  - Integrates with Damageable component for health management

- **ZombieSpawner.cs**: Manages zombie wave spawning and difficulty progression
  - Handles wave-based spawning with configurable parameters
  - Scales difficulty over time using difficultyScalingFactor
  - Supports both random and predefined spawn points
  - Maintains list of active zombies for game state tracking

### Vehicle System
- **TruckController.cs**: Manages player-controlled truck movement and physics

### Combat System
- **Damageable.cs**: Component for entities that can take damage
  - Used by both zombies and the player's truck
  - Handles health management and destruction

## Component Interactions

1. **Zombie Spawning Flow**:
   - ZombieSpawner creates zombies at intervals
   - Each zombie is equipped with ZombieAI and Damageable components
   - Zombies are tracked in _activeZombies list for management

2. **Combat Flow**:
   - TruckController handles collision with zombies
   - Damageable components process damage events
   - ZombieAI controls zombie attack behavior

3. **Game State Management**:
   - GameManager coordinates overall game flow
   - ZombieSpawner responds to game state for wave management
   - All components check GameManager.IsGamePaused for pause state

## Design Patterns

1. **Singleton Pattern**:
   - Used in GameManager for global state access

2. **Component Pattern**:
   - Unity's component system utilized for modular functionality
   - Separate concerns between AI, damage, and spawning

3. **Observer Pattern**:
   - GameManager broadcasts game state changes
   - Components observe and react to state changes

## Future Considerations

1. **Scalability**:
   - Wave system designed for difficulty scaling
   - Spawn system supports multiple spawn point strategies

2. **Extensibility**:
   - Modular component design allows easy addition of new features
   - Separated concerns enable independent component updates

3. **Performance**:
   - Zombie cleanup handled automatically
   - Spawn distances optimized for performance
   - NavMeshAgent used for efficient pathfinding