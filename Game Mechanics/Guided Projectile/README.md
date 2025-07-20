# Game-Dev-Tool-Kits | Guided Projectile

## Overview
`GuidedProjectile` is a highly flexible and reusable Unity class that manages guided projectile behavior in both **2D** and **3D** game environments. It supports multiple guidance types such as **guided**, **controlled**, **unguided**, and **predictive**, and is designed to be extended or customized for various types of projectiles (e.g., missiles, magic spells, homing bullets).

## Key Features
- **Multi-Dimensional Support**: Works in both 2D (`Rigidbody2D`) and 3D (`Rigidbody`) physics environments.
- **Multiple Guidance Modes**:
	- **Guided** – Tracks a moving or static target in real-time.
	- **Controlled** – Moves based on player input (e.g., joystick).
	- **Unguided** – Moves in a straight path based on initial direction.
	- **Predictive** – Estimates and intercepts a moving target’s future position.
- **Customizable Movement**:
	- Adjustable `projectileSpeed` and `projectileTurnSpeed` for fine-tuned control.
- **Flexible Targeting**:
	- Supports both `Transform` targets and `Vector3` positional targeting.
- **Extendable Input Logic**:
	- Override `ProjectileInput` for custom input handling in controlled mode.
	
## Dependencies
This script uses my `Custom Attributes` and the `GameMechanicUtility` script.

## Usage

### 1. Inherit the Class
```csharp
	public class MyClass : GuidedProjectile {
		... Your Code
	}
```

### 2. Call necessary functions in FixedUpdate
```csharp
	void FixedUpdate () {
		ProjectileInput(InputVector);
		ProjectileGuidanceDirection();
	}
```

>**Note:** The `ProjectileInput` function should only be used if the your guidance mode is **Controlled**.