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
This script uses my `CustomAttributes` library for enhanced inspector features such as `[ReadOnly]`, `[MinValue]`, `[EnumFlags]`, `[ShowIf]`, and `[LineDivider]`.

## Usage

### 1. Attach the Script
Attach `TimeManager` to any GameObject in your scene to manage both global time scale and in-game timer functionality.

### 2. Configure Timer in Inspector
- Choose **Timer Mode** (`CountUp` or `CountDown`).
- Set **Time Limit** (only for countdown mode).
- Enable **Timer Text** and assign a `TMP_Text` component if you want UI display.
- Select **Timer Precision** flags for the desired units.

### 3. Control the Timer in Code
```csharp
// Change timer mode and optionally pause on reset
timeManager.ChangeTimerFunction_CountUp(true);
timeManager.ChangeTimerFunction_CountDown(60f, true);

// Adjust the time limit dynamically
timeManager.AdjustTimeLimit(10f);

// Pause or resume the timer
timeManager.PauseUnpauseTimer();

// Update timer every frame
timeManager.UpdateTimer();