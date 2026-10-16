# Drone vs Enemy Combat Simulation

**Developed for:** IPage UM Services  
**Engine:** Unity 6.6  
**Platform:** Windows PC  
**Project Type:** 3D Drone Combat Simulation Prototype

## Project Overview

Drone vs Enemy Combat Simulation is a 3D action prototype in which the player controls a combat drone, engages enemy soldiers, and attempts to eliminate all enemies while avoiding incoming fire.

The project demonstrates drone movement, missile combat, enemy AI, health and damage systems, animation, audio, visual effects, and a complete gameplay loop with menus and mission outcomes.

## Features Implemented

- **Drone Controls:** Camera-relative movement, altitude control, mouse aiming, and missile firing.
- **Missile Combat:** Straight-line missile movement, collision detection, area-of-effect damage, launch effects, and explosion effects.
- **Enemy AI:** Enemy detection, patrol movement, aiming, and projectile-based attacks.
- **Enemy Health:** Health bars, damage handling, and enemy destruction when health reaches zero.
- **Drone Health:** Health display and game-over handling when the drone is destroyed.
- **Animations:** Enemy patrol and shooting animations.
- **Audio:** Gunshot, missile-launch, and explosion sound effects.
- **Visual Effects:** Missile trails, launch effects, muzzle flashes, and explosion effects.
- **Main Menu:** Start Mission, Controls, and Quit options.
- **Pause Menu:** Resume, Restart Mission, and Main Menu options.
- **Mission Outcomes:** Mission Complete and Mission Failed screens with retry and main-menu navigation.

## Controls

| Input | Action |
|---|---|
| W / A / S / D | Move the drone |
| Q / E | Descend / Ascend |
| Mouse | Aim |
| Left Mouse Button or Space | Fire missile |
| Esc | Pause or resume the mission |

## How to Open the Unity Project

1. Install a compatible version of Unity 6.
2. Open Unity Hub.
3. Select **Add** or **Open** and choose the `DroneCombatSimulation_Submission` folder.
4. Allow Unity to import assets and resolve packages.
5. Open `MainMenu` to inspect the main menu scene or run the game from the Editor.

The project contains the Unity source assets, scripts, scenes, prefabs, materials, animations, and supporting resources.

## How to Run the Windows Build

1. Open the provided Windows build folder.
2. Run the game executable.
3. Select **Start Mission** to begin playing.

The standalone Windows build has been tested successfully.

## Technologies Used

- Unity 6.6
- C#
- Universal Render Pipeline (URP)
- Unity Animator
- TextMeshPro
- Unity UI
- Rigidbody and collider-based physics
- Particle systems and audio sources

## Testing

The following gameplay flows were tested in the Unity Editor and standalone Windows build:

- Main menu and controls navigation
- Drone movement and missile combat
- Enemy attacks and health handling
- Pause, resume, and restart
- Mission failure and retry
- Mission completion and retry
- Navigation back to the main menu

## Known Limitations

- This is a prototype, not a full-scale commercial game.
- The gameplay focuses on a single combat mission.
- Enemy behavior and combat balance are intentionally simple.
- Additional levels, objectives, and advanced AI could be added in future versions.

## Screenshots

### Main Menu
![Main Menu](Screenshots/01-main-menu.png)

### Controls
![Controls](Screenshots/02-controls.png)

### Gameplay
![Gameplay](Screenshots/03-gameplay.png)

### Pause Menu
![Pause Menu](Screenshots/04-pause-menu.png)

### Mission Failed
![Mission Failed](Screenshots/05-mission-failed.png)

### Mission Complete
![Mission Complete](Screenshots/06-mission-complete.png)

## Demo Video

The recorded gameplay demonstration is included in the `DemoVideo` folder.

**Video file:** [DroneCombatSimulation_Demo.mp4](DemoVideo/DroneCombatSimulation_Demo.mp4)

The demo shows the main menu, drone controls, combat, pause menu, mission failure, mission retry, and mission completion.

## GitHub Repository

**Repository URL:** To be added after publishing the project to GitHub.

## Credits

Developed as an internship project for IPage UM Services.

Third-party assets and audio should be credited according to their respective licenses where applicable.