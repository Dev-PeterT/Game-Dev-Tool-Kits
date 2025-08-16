# Game-Dev-Tool-Kits | Gravity Manipulator

## Overview
`GravityManipulator` is a versatile and modular Unity class that simulates customizable gravity for both **2D** and **3D** game environments. It supports **spherical** and **directional** gravity types and is designed to integrate seamlessly with `GravityBody` components to create rich gravity-based interactions, such as planetary or localized gravitational fields.

## Key Features
- **Multi-Dimensional Support**: Works in both 2D (`Rigidbody2D`) and 3D (`Rigidbody`) physics environments.

- **Multiple Gravity Types**: 
	- **Spherical**: Pulls objects toward the gravity source’s center (e.g., planets).
	- **Directional**: Applies gravity in a fixed or bidirectional axis (e.g., Earth-like gravity).

- **Gravity Fall-Off**: Optional inverse-square law to simulate realistic weakening of gravity over distance.

- **Custom Range Control**: Supports both unlimited global gravity and localized fields via automatically configured trigger colliders.

- **Rotation Alignment**: Affected objects can rotate smoothly to align with the direction of gravitational pull.

- **Optimized Update System**: Uses interval checking when in unlimited range mode to reduce overhead.

- **Automatic Collider Setup**: Automatically assigns and sizes 2D or 3D colliders based on gravity settings.

## Dependencies
This script uses my `Custom Attributes` and the `GameMechanicUtility` script.

## Usage

### 1. Inherit the Class
```csharp
	public class GravitySource : GravityManipulator {
		... Your Code
	}

	public class MyClass : GravityBody {
		... Your Code
	}
```
>**Note:** Attach GravityManipulator to any GameObject to make it a gravity source. Attach GravityBody to any Rigidbody-based object to make it responsive to custom gravity.

### 2. Call the necessary functions in Start and FixedUpdate

```csharp
	void Start() {
		InitializeGravityBody();
	}
	
	void FixedUpdate () {
		UpdateGravity();
	}
```

>**Note:** In most cases, you only need to interact with the script that inherits from `GravityBody` — gravity behavior is handled automatically via `GravityManipulator`.
