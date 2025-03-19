# Tech Stack for Truck vs. Zombies: Arlington Rampage

This document outlines the technology stack chosen for the development of *Truck vs. Zombies: Arlington Rampage*, a 3D action game set in a realistic environment. The stack is designed to be simple yet robust, ensuring efficient development while maintaining high performance and scalability.

---

## Game Engine
- **Unity**  
  Unity is the chosen game engine due to its powerful 3D rendering capabilities, ease of use, and extensive support for PC platforms. It provides a comprehensive set of tools for creating realistic environments, managing physics, and handling complex game logic, making it ideal for this project.  
  - **Why Unity?**  
    - Optimized for 3D graphics and physics.  
    - Large community and extensive documentation.  
    - Cross-platform support, with a primary focus on PC for this game.

---

## Programming Language
- **C#**  
  C# is the primary scripting language used in Unity. It is straightforward to learn and provides the flexibility needed to implement game mechanics such as zombie AI, gun upgrades, and score tracking.  
  - **Why C#?**  
    - Native support in Unity.  
    - Balances simplicity and power for game logic.  
    - Strong community support with many available resources.

---

## Physics Engine
- **Unity’s Built-in Physics**  
  Unity’s integrated physics engine is used to handle real-time interactions such as truck movement, zombie collisions, and gunplay. It is robust enough to manage the game’s requirements without the need for external libraries.  
  - **Why Unity’s Physics?**  
    - Seamless integration with the engine.  
    - Handles collision detection and physics simulations efficiently.  
    - Reduces complexity by avoiding third-party dependencies.

---

## UI System
- **Unity’s UI Toolkit**  
  The UI Toolkit is used to create 2D elements such as health bars, ammo counters, and score displays. It is flexible and allows for easy customization without complicating the development process.  
  - **Why UI Toolkit?**  
    - Integrated with Unity, ensuring compatibility.  
    - Supports responsive and scalable UI designs.  
    - Simplifies the creation and management of UI elements.

---

## Audio System
- **Unity’s Audio Engine**  
  Unity’s built-in audio engine manages all sound effects and background music, including 3D spatial audio for immersive gameplay. It handles multiple sound sources efficiently, such as truck engines, gunshots, and zombie sounds.  
  - **Why Unity’s Audio Engine?**  
    - Supports 3D spatial audio for realistic sound positioning.  
    - Easy to implement and manage within the Unity ecosystem.  
    - No need for external audio libraries, keeping the stack lean.

---

## Map Integration (Optional)
- **Mapbox Plugin**  
  For optional integration of real-world map data, the Mapbox plugin can be used to import satellite imagery or street layouts of Arlington, TX. This enhances the game’s realism but is not essential for core gameplay.  
  - **Why Mapbox?**  
    - Provides accurate, real-world geographic data.  
    - Easy to integrate with Unity via the Mapbox SDK.  
    - Can be added later without disrupting the core development.

---

## Networking
- **Not Included (Single-Player Focus)**  
  The game is currently designed as a single-player experience, so no networking stack is included. However, Unity’s networking tools (e.g., Netcode for GameObjects) or third-party solutions like Photon can be integrated later if multiplayer features are desired.  
  - **Why No Networking?**  
    - Simplifies the initial development process.  
    - Reduces complexity and potential performance issues.  
    - Multiplayer can be added as an extension if needed.

---

## Summary
This tech stack is designed to be as simple and robust as possible, leveraging Unity’s integrated tools to minimize external dependencies while ensuring the game runs smoothly on PC. Each component is chosen to streamline development, maintain high performance, and allow for future scalability if needed.

---

**Note**: This stack avoids unnecessary complexity by focusing on Unity’s built-in systems, ensuring that the game can be developed efficiently without compromising on quality or performance.