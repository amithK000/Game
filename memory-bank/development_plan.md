# Development Plan for Truck vs. Zombies: Arlington Rampage

This document outlines the development timeline, priorities, team organization, technical dependencies, testing strategies, performance goals, environment design, MVP features, tools, and asset requirements for *Truck vs. Zombies: Arlington Rampage*. The game is being developed by AI, leveraging tools like Cursor, with a focus on efficiency and optimization.

---

## 1. Development Timeline and Milestone Schedule
**Total Duration**: 2 weeks (10 working days)  
**Context**: AI-driven development allows for rapid iteration and parallel task execution.  
**Timeline**:
- **Phase 1: Setup and Core Mechanics (Days 1-3)**  
  - *Milestone 1: Unity Project Setup* (Day 1, 0.5 day): Set up Unity, import assets, create basic scene.  
    - Test: Scene loads with truck, zombies, and environment visible.  
  - *Milestone 2: Truck Movement* (Day 1-2, 1 day): Mouse-based truck steering and physics.  
    - Test: Truck moves and turns with mouse input.  
  - *Milestone 3: Zombie Collision* (Day 2-3, 1.5 days): Truck destroys zombies on impact.  
    - Test: Drive over zombie, confirm it’s removed.  
- **Phase 2: Combat and Systems (Days 4-7)**  
  - *Milestone 4: Basic Shooting* (Day 4, 1 day): Pistol with mouse-aimed raycasting.  
    - Test: Click to shoot, zombie destroyed.  
  - *Milestone 5: Truck Health System* (Day 5, 1 day): Health bar and zombie attacks.  
    - Test: Health decreases near zombies, stops at zero.  
  - *Milestone 6: Scoring System* (Day 6-7, 1.5 days): Score updates on kills.  
    - Test: Kill zombie, score increases on UI.  
- **Phase 3: Gameplay Loop and Polish (Days 7-10)**  
  - *Milestone 7: Zombie Spawning* (Day 7-8, 1 day): Random spawning and basic AI.  
    - Test: Zombies spawn and pursue truck.  
  - *Milestone 8: Level Progression* (Day 8, 0.5 day): Kill 10 zombies to complete level.  
    - Test: “Level Complete” message appears.  
  - *Milestone 9: Performance Optimization* (Day 9, 1 day): Optimize for 20-30 zombies.  
    - Test: 60 FPS with 20-30 zombies.  
  - *Milestone 10: Final Polish* (Day 10, 1 day): Add sounds, fix bugs.  
    - Test: Playthrough works with audio and UI.

---

## 2. Priority Order for Implementing Core Features
**Order**:  
1. **Truck Mechanics**: Foundation of gameplay; player control must be established first.  
2. **Zombie AI**: Enables interaction and conflict with the truck.  
3. **Weapons System**: Adds depth to combat after basic mechanics are functional.  
**Reasoning**: Truck mechanics are the core player experience, followed by zombies as the primary antagonist. Weapons enhance engagement but depend on prior systems being in place.

---

## 3. Team Organization and Responsibilities
**Team**: AI-driven (e.g., Cursor or similar), with human oversight (you).  
- **AI Developer (Primary)**:  
  - Responsibilities: Code generation, asset integration, optimization, debugging.  
  - Tasks: Implement all scripts (e.g., `TruckController.cs`, `ZombieSpawner.cs`), configure Unity, run tests.  
- **Human Lead (You)**:  
  - Responsibilities: Define requirements, validate milestones, adjust AI prompts.  
  - Tasks: Review tests, provide feedback, approve deliverables.  
**Structure**: AI handles execution; human ensures alignment with vision. No traditional team; AI acts as a multi-role developer.

---

## 4. Specific Technical Dependencies and Implementation Order
**Dependencies**:  
1. **Unity Engine**: Core platform for all development.  
   - Order: Install first (Day 1).  
2. **C# Scripting**: Used within Unity for logic.  
   - Order: Begins with truck mechanics (Day 1).  
3. **Unity Physics**: Built-in, for collisions and movement.  
   - Order: Configured with truck movement (Day 2).  
4. **Unity UI Toolkit**: For health bar, score display.  
   - Order: Added with health system (Day 5).  
5. **Unity Audio Engine**: For sound effects.  
   - Order: Integrated during polish (Day 10).  
**Reasoning**: Dependencies follow a logical build order, starting with the engine and layering systems as needed.

---

## 5. Testing Strategies During Development
- **Unit Testing**: Test individual components (e.g., truck movement, shooting) after each milestone.  
  - Example: Verify truck turns with mouse input.  
- **Integration Testing**: Check combined systems (e.g., truck hitting zombies, health decreasing).  
  - Example: Drive into zombie, confirm health drops.  
- **Performance Testing**: Use Unity Profiler to ensure 60 FPS during optimization (Day 9).  
- **Playtesting**: Full playthrough on Day 10 to validate gameplay loop.  
- **Automation**: AI runs tests per milestone, logs results for human review.  
**Frequency**: After every step, with a final comprehensive test.

---

## 6. Performance Benchmarks and Optimization Goals
- **Benchmarks**:  
  - Frame Rate: 60 FPS minimum with 20-30 zombies.  
  - Load Time: Scene loads in under 5 seconds.  
  - CPU/GPU Usage: No spikes above 80% during peak action.  
- **Goals**:  
  - Smooth gameplay with 30 zombies on screen.  
  - Minimal lag during collisions or shooting.  
  - Optimized physics and rendering for PC performance.  
**Validation**: Use Unity Profiler on Day 9 to meet benchmarks.

---

## 7. Arlington Environment Creation
- **Approach**: Stylized design based on Arlington, TX.  
  - No real map data (e.g., Mapbox) for the base game to keep it simple.  
  - Use generic urban assets (streets, buildings) to evoke Arlington’s feel.  
- **Reasoning**: Stylized saves time and avoids data integration complexity. Real map data can be a post-launch enhancement.  
- **Implementation**: Basic assets imported on Day 1, refined during polish (Day 10).

---

## 8. Minimum Viable Product (MVP) Features vs. Post-Launch Additions
- **MVP Features** (Base Game, 10 Days):  
  - Truck movement with mouse control.  
  - Zombie spawning and basic AI (move to truck).  
  - Collision-based zombie elimination.  
  - Pistol with unlimited ammo.  
  - Truck health system with UI bar.  
  - Scoring system with UI display.  
  - Single level (kill 10 zombies).  
  - Basic sound effects (engine, gunshots, groans).  
- **Post-Launch Additions**:  
  - Gun upgrades (damage, rate, ammo).  
  - Multiple zombie types (fast, tank).  
  - Additional levels and objectives.  
  - Real Arlington map data (Mapbox).  
  - Multiplayer mode.  
**Reasoning**: MVP delivers a playable core; extras enhance replayability later.

---

## 9. Source Control and Project Management Tools
- **Source Control**: Git with GitHub  
  - Why: Simple, widely supported, allows versioning and rollback.  
  - Usage: AI commits after each milestone; human reviews commits.  
- **Project Management**: Trello  
  - Why: Visual task tracking, easy to manage milestones.  
  - Usage: Boards for “To Do,” “In Progress,” “Done”; AI updates task status.  
**Setup**: Initialize Git repo and Trello board on Day 1.

---

## 10. Specific Asset Requirements and Sources
- **3D Models**:  
  - Truck: Simple rigged truck model.  
  - Zombies: Basic humanoid model with walk animation.  
  - Environment: Urban assets (roads, buildings, props).  
  - Source: Unity Asset Store (free tier) or Mixamo for zombies.  
- **Animations**:  
  - Truck: Idle, movement (tilting).  
  - Zombies: Walk, attack (melee swing).  
  - Source: Mixamo or Unity Asset Store.  
- **Sound Effects**:  
  - Truck engine, gunshots, zombie groans.  
  - Source: FreeSound.org or Unity Asset Store.  
**Acquisition**: AI downloads and imports on Day 1; polish on Day 10.

---

## Summary
This plan leverages AI efficiency for a 10-day development cycle, prioritizing truck mechanics, zombie AI, and weapons in that order. The AI handles all tasks with human oversight, using Unity’s built-in tools and stylized assets for simplicity. Testing is rigorous, performance is optimized for 60 FPS, and the MVP focuses on core gameplay, with extras planned post-launch. Git and Trello ensure smooth management.