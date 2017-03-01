# none-shall-pass

A mobile top down action RPG / siege defence game using gesture based spells & abilities.

![None Shall Pass](none-shall-pass.gif?raw=true "None Shall Pass")

*Project Status:* Technical Prototype

## Action Framework
This project uses an action framework based around !(Input/ActionTrigger.cs "Action Triggers") which monitor touch input using the custom !(Input/InputController.cs "Input Controller") across a number of frames to detect gesture patterns and trigger a corresponding action such as using an ability or casting a spell.

## Fruit Ninja Style Swipe
One such action trigger implemented for this prototype is the !(Input/MeleeActionTrigger.cs "Melee Swipe Action Trigger") which tracks a swipe across the screen then transforms the input to produce a smooth curve in 3D world space and render a slice particle along this curve.

### Info
This repository contains only the scripts from the Unity project. Binary file are kept out of the public repo as they contain art assets purchased from the Unity Asset Store. A rudimentary !(HeroController.cs "Player Controller") and !(EnemyKnight.cs "AI Controller") with basic detect and chase behaviours is included. The animations are targeted to the (https://www.assetstore.unity3d.com/en/#!/content/39419 "Boxy Knight") but could easily be switched out for another model.

