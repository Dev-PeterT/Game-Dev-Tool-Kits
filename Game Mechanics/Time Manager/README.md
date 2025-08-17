# Game-Dev-Tool-Kits | TimeManager

## Overview
`TimeManager` is a modular and reusable Unity class that provides **global time scaling** and **customizable timer functionality**. It supports both **count-up** and **countdown** timers with adjustable precision and optional **TextMeshPro UI integration**. Designed for modular use, it can be extended to support gameplay mechanics that rely on precise timing, slow-motion effects, or countdowns.

## Key Features
- **Smooth Time Scaling**: Gradually interpolates Unity's `Time.timeScale` over a configurable duration.
- **Timer Modes**:
  - **CountUp** – Tracks elapsed time starting from zero.
  - **CountDown** – Counts down from a set time limit until zero.
- **Adjustable Time Limits**: Modify countdown durations dynamically at runtime.
- **Timer Precision**:
  - Display time in **Days**, **Hours**, **Minutes**, **Seconds**, and/or **Milliseconds**.
  - Flexible bitwise flags allow combining multiple units.
- **Optional UI Integration**: Display formatted timer text in a `TMP_Text` component.
- **Pause/Resume Control**: Easily pause or unpause the timer during gameplay.
- **Extensible Design**: Virtual methods allow overriding and customization for game-specific time-based mechanics.
- **Debugging Tools**:
  - Access current interpolated time scale.
  - Inspect final formatted timer text.

## Dependencies
This script only uses my `CustomAttributes` library.

## Usage

### 1. Attach the Script
Inherit the Class, alternatively you can attach TimeManager to any GameObject in your scene to manage both global time scale and in-game timer functionality.
```csharp
	public class MyClass : TimeManager {
		... Your Code
	}
```

### 2. Call the necessary functions in Update
```csharp
  void ChangeTimeScale(float value, float value);

  void UpdateTimer();

  void ChangeTimerFunction_CountUp(bool value);

  void ChangeTimerFunction_CountDown(float value, bool value);

  void AdjustTimeLimit(float value);

  void ResetTimer(bool value);

  void PauseUnpauseTimer();
```
