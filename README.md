# Photon Fusion FPS Prototype

A Unity multiplayer prototype built around Photon Fusion. The project combines Fusion SDK files with a small set of game scripts for player spawning, first-person movement, synchronized health and color, and a simple raycast attack.

This is a prototype foundation, not a finished multiplayer game. It does not include a complete map, matchmaking flow, production-ready combat, or a polished player experience.

## What is implemented

- Spawn a player object when the local player joins.
- Move and jump the local player using Unity's Input Manager.
- Follow the local player with a first-person camera.
- Synchronize health and color through Fusion network properties.
- Send damage requests to the state authority with an RPC.
- Test a center-screen raycast attack.

Controls: **WASD** to move, **Space** to jump, **F** to fire, **E** to change the player's color, and the mouse to look around.

## Open in Unity

The checked-in project targets Unity **6000.3.12f1**. Open the repository root with that editor version. Before testing multiplayer:

1. Configure a Photon Fusion App ID for your own Photon project.
2. Open the scene that contains the NetworkRunner, PlayerSpawner, and player prefab.
3. Assign the player prefab to PlayerSpawner.PlayerPrefab.
4. Check that the player prefab includes the required Fusion network object, character controller, movement, health, color, and raycast components.
5. Ensure the scene has a main camera with FirstPersonCamera attached.

The scripts use Unity's classic Input Manager axes and buttons. Set the project's Active Input Handling to support the Input Manager if your editor setup does not already do so.

The repository includes Photon Fusion SDK files under Assets/Photon/. Review Photon’s own terms and licensing before redistributing those files.

## Main scripts

- Assets/PlayerSpawner.cs: spawns the local player's prefab.
- Assets/PlayerMovement.cs: owner-controlled movement and jump.
- Assets/FirstPersonCamera.cs: mouse-look camera follow.
- Assets/Health.cs: networked health and state-authority damage RPC.
- Assets/PlayerColor.cs: networked color change.
- Assets/RaycastAttack.cs: center-screen raycast attack input.

## Current limits

This project is a learning prototype. Network session creation and scene/prefab wiring must be completed in the Unity editor. The raycast attack is a basic demonstration, not a complete or cheat-resistant combat system.
