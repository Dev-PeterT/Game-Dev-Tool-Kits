# Game-Dev-Tool-Kits

## Introduction
**Game Dev Tool Kits** is a curated collection of **modular, reusable, and production-ready** Unity tools and game mechanic scripts designed to accelerate your development workflow.

This repository is split into two main categories:

- **Custom Attributes:**
	A toolkit of Unity editor extensions built using custom attributes to enhance the Inspector experience. These attributes help manage serialized variables in a more intuitive, organized, and user-friendly way—improving clarity and productivity when working in the Unity Editor.

- **Game Mechanics:**
	A collection of foundational **MonoBehaviour-based scripts** representing core gameplay systems. Each mechanic is structured as a base class that can be inherited and extended, allowing for clean integration and high reusability across projects. These scripts promote **clean architecture, separation of concerns**, and **scalable game design**.

This toolkit provides a solid starting point that emphasizes scalability, clarity, and real-world usability.

## Unity Engine Compatibility 
Compatible with **Unity 6.0 or newer**

## Dependencies
All of the **Game Mechanics** scripts depend on the utilities and attributes found in the **Custom Attributes** folder. If you're using any scripts from the **Game-Mechanics** folder, make sure to also include the **Custom-Attributes** folder in your project.

>:warning: **Note:** Deleting or omitting the **Custom-Attributes** folder will cause the scripts to throw errors. If you want to use the **Game Mechanics** scripts **without** including **Custom-Attributes**, you must remove all custom attributes (non-native Unity attributes) from the scripts and delete any `using CustomAttributes;` statements to prevent compilation errors.

## Installation
To use any of the tools or game mechanic scripts in this repository, simply:

1. **Copy**  the desired folder(s) (e.g., `Custom-Attributes` or a specific `Game-Mechanics` subfolder) from this repository.

2. **Paste** them into your Unity project’s `Assets` folder.

Unity will automatically import the scripts and make the tools available in your project.

>**Note:** The Custom-Attributes folder should be included as a whole due to shared dependencies between editor extensions and utilities.

Unity will automatically import the scripts and make the tools available in your project.

## Table of Contents
