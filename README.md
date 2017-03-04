# None Shall Pass

A mobile top down action RPG / siege defence game using gesture based spells & abilities.

![None Shall Pass](none-shall-pass.gif?raw=true "None Shall Pass")

*Project Status:* Technical Prototype

## Action Framework
This project uses an action framework based around [Action Triggers](Input/ActionTrigger.cs) which monitor touch input using the custom [Input Controller](Input/InputController.cs) across a number of frames to detect gesture patterns and trigger a corresponding action such as using an ability or casting a spell.

## Fruit Ninja Style Swipe
One such action trigger implemented for this prototype is the [Melee Swipe Action Trigger](Input/MeleeActionTrigger.cs) which tracks a swipe across the screen then transforms the input to produce a smooth curve in 3D world space and render a slice particle along this curve.

### Info
This repository contains only the scripts from the Unity project. Binary file are kept out of the public repo as they contain art assets purchased from the Unity Asset Store. A rudimentary [Player Controller](HeroController.cs) and an [AI Controller](EnemyKnight.cs) with basic detect and chase behaviours is included. The prototype employs tap to move and swipe to attack controls.

