using TMPro;
using UnityEngine;
using CustomAttributes;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <para>
/// Manages global time scaling and customizable timer functionality for Unity projects.<br/>
/// Supports both count-up and countdown timers with adjustable precision and optional UI display.<br/>
/// Designed for modular integration with gameplay systems requiring time-based logic or slow-motion effects.
/// </para>
/// 
/// <para>
/// <b>Key Features:</b><br/>
/// <b>- Smooth Time Scaling:</b> Gradually interpolates the game's time scale over a set duration.<br/>
/// <b>- Timer Modes:</b> Switch between CountUp (elapsed time) and CountDown (time remaining).<br/>
/// <b>- Adjustable Time Limits:</b> Modify countdown durations dynamically at runtime.<br/>
/// <b>- Timer Precision:</b> Display time in days, hours, minutes, seconds, and/or milliseconds.<br/>
/// <b>- Optional UI Integration:</b> Output formatted timer text directly to a TextMeshPro component.<br/>
/// <b>- Pause Control:</b> Easily toggle between paused and unpaused timer states.<br/>
/// <b>- Modular Design:</b> Extendable structure for integrating time-related gameplay mechanics.
/// </para>
/// 
/// <para>
/// <b>Usage:</b><br/>
/// Attach this script to a GameObject to control both global time scaling and an in-game timer.<br/>
/// Configure the timer mode, precision, and UI settings in the Inspector.<br/>
/// Use public/protected methods to change time scale, reset the timer, or adjust countdown limits during gameplay.
/// </para>
/// </summary>
public class TimeManager : MonoBehaviour {
    #region Time Manager Variables
    /// <summary>
    /// Defines the behavior of the timer:
    /// <list type="bullet">
    /// <item><description><b>CountUp</b> - The timer starts at zero and increases over time.</description></item>
    /// <item><description><b>CountDown</b> - The timer starts at a set limit and decreases until reaching zero.</description></item>
    /// </list>
    /// </summary>
    enum TimerFunction { CountUp, CountDown }

    /// <summary>
    /// Specifies which units of time should be displayed in the timer text:
    /// <list type="bullet">
    /// <item><description><b>Days</b> - Displays days.</description></item>
    /// <item><description><b>Hours</b> - Displays hours.</description></item>
    /// <item><description><b>Minutes</b> - Displays minutes.</description></item>
    /// <item><description><b>Seconds</b> - Displays seconds.</description></item>
    /// <item><description><b>Millisecond</b> - Displays milliseconds.</description></item>
    /// </list>
    /// Multiple values can be combined using bitwise flags. 
    /// </summary>
    enum TimerTextPrecision { Days, Hours, Minutes, Seconds, Millisecond }

    [Header("Time Scale Properties")]
    [Tooltip("If true, allows time scale changes to be interrupted by new requests.")]
    [SerializeField] bool allowTimeScaleInteruption = false;
    [Tooltip("The speed at which time scale smoothly interpolates to the new value.")]
    [SerializeField][MinValue(0)] float smoothTimeScaleSpeed = 0.5f;

    [Space(10)][LineDivider(1, LineColors.Gray)]

    [Header("Timer Properties")]
    [Tooltip("Indicates whether the timer is currently paused.")]
    [SerializeField][ReadOnly] bool timerIsPaused = true;
    [Tooltip("Determines if the timer counts up or down.")]
    [SerializeField] TimerFunction timerFunction;
    [Tooltip("The maximum time for countdown timers.")]
    [SerializeField][ConditionalEnumHide("timerFunction", (int)TimerFunction.CountDown)][MinValue(0)] float timeLimit = 0;

    [Space(10)][LineDivider(1, LineColors.Gray)]

    [Tooltip("Enable this to display a timer text UI.")]
    [SerializeField] bool haveTimerText = false;
    [Tooltip("The TextMeshPro component used to display the timer.")]
    [SerializeField][ShowIf("haveTimerText", true)][NotNullable] TMP_Text timerText = null;
    [Tooltip("The precision of the timer text (days, hours, minutes, seconds, milliseconds).")]
    [SerializeField][ShowIf("haveTimerText", true)][EnumFlags] TimerTextPrecision timerPrecision;
    
    [Space(15)][LineDivider(3, LineColors.Black)]

    [Header("Guided Projectile Debug Properties")][Space(5)]
    [Tooltip("Current interpolated time scale, for debugging purposes.")]
    [ReadOnly][SerializeField] float currentTimeScale;
    [Space(15)]
    [Tooltip("Current timer count in seconds.")]
    [ReadOnly][SerializeField] float currentTimerCount;
    [Tooltip("Final formatted timer text, for debugging or UI display.")]
    [ReadOnly][SerializeField] string finalTimerText;

    Coroutine timeScaleCoroutine;
    List<string> _timerTextPrecision = new List<string>();
    #endregion

    #region Getters and Setters
    public bool TimerIsPaused {
        get => timerIsPaused;
        set => timerIsPaused = value;
    }
    public float TimeLimit {
        get => timeLimit;
        set => timeLimit = value;
    }
    public float CurrentTimerCount { 
        get => currentTimerCount;
        set => currentTimerCount = value;
    }
    #endregion

    #region Script Functionality
    /// <summary>
    /// Smoothly changes the game's time scale to the specified value.
    /// </summary>
    /// <param name="desiredTimeScale">The target time scale value. Values below zero are clamped to zero.</param>
    protected virtual void ChangeTimeScale(float desiredTimeScale) {
        if (desiredTimeScale < 0) {
            desiredTimeScale = 0;
        }

        if (allowTimeScaleInteruption) {
            timeScaleCoroutine = StartCoroutine(LerpTimeScale(desiredTimeScale, smoothTimeScaleSpeed));
        }
        else {
            if (timeScaleCoroutine != null) {
                StopCoroutine(timeScaleCoroutine);
            }
            timeScaleCoroutine = StartCoroutine(LerpTimeScale(desiredTimeScale, smoothTimeScaleSpeed));
        }
    }

    /// <summary>
    /// Coroutine that interpolates Time.timeScale smoothly over a given duration.
    /// </summary>
    /// <param name="newTimeScale">The target time scale value.</param>
    /// <param name="scaleSpeed">The duration of the interpolation, in seconds (unscaled time).</param>
    IEnumerator LerpTimeScale(float newTimeScale, float scaleSpeed) {
        float startTimeScale = Time.timeScale;
        float elapsed = 0f;

        while (elapsed < scaleSpeed) {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(startTimeScale, newTimeScale, elapsed / scaleSpeed);
            yield return null;

            currentTimeScale = Time.timeScale;
        }

        Time.timeScale = newTimeScale;
        currentTimeScale = Time.timeScale;
    }

    /// <summary>
    /// Updates the timer count based on the selected TimerFunction
    /// and refreshes the displayed timer text if enabled.
    /// </summary>
    protected virtual void UpdateTimer() {
        if (!timerIsPaused) {
            switch (timerFunction) {
                case TimerFunction.CountUp:
                    currentTimerCount += Time.deltaTime;
                    break;
                case TimerFunction.CountDown:
                    currentTimerCount -= Time.deltaTime;
                    if (currentTimerCount <= 0) {
                        timerIsPaused = true;
                    }
                    break;
            }
        }

        if (haveTimerText) {
            int _totalSeconds = (int)currentTimerCount;
            _timerTextPrecision.Clear();

            if (timerPrecision.HasFlag(TimerTextPrecision.Days)) {
                _timerTextPrecision.Add((_totalSeconds / 86400).ToString());
                _totalSeconds %= 86400;
            }
            if (timerPrecision.HasFlag(TimerTextPrecision.Hours)) {
                _timerTextPrecision.Add((_totalSeconds / 3600).ToString("D2"));
                _totalSeconds %= 3600;
            }
            if (timerPrecision.HasFlag(TimerTextPrecision.Minutes)) {
                _timerTextPrecision.Add((_totalSeconds / 60).ToString("D2"));
                _totalSeconds %= 60;
            }
            if (timerPrecision.HasFlag(TimerTextPrecision.Seconds)) {
                _timerTextPrecision.Add(_totalSeconds.ToString("D2"));
            }
            if (timerPrecision.HasFlag(TimerTextPrecision.Millisecond)) {
                int _milliseconds = (int)((currentTimerCount % 1f) * 1000);
                _timerTextPrecision.Add(_milliseconds.ToString("D3"));
            }

            finalTimerText = string.Join(" : ", _timerTextPrecision);
        }
    }

    /// <summary>
    /// Switches the timer to CountUp mode and resets the timer.
    /// </summary>
    protected virtual void ChangeTimerFunction_CountUp(bool pauseOnReset) {
        timerFunction = TimerFunction.CountUp;
        ResetTimer(pauseOnReset);
    }
    /// <summary>
    /// Switches the timer to CountDown mode with a new time limit and resets the timer.
    /// </summary>
    /// <param name="newTimeLimit">The maximum countdown time in seconds.</param>
    protected virtual void ChangeTimerFunction_CountDown(float newTimeLimit, bool pauseOnReset) {
        if (newTimeLimit < 0) {
            Debug.Log("You cannot have a negative time limit to count down from.");
            return;
        }

        timerFunction = TimerFunction.CountDown;
        timeLimit = newTimeLimit;
        ResetTimer(pauseOnReset);
    }
    /// <summary>
    /// Adjusts the time limit for countdown timers by adding or subtracting seconds.
    /// Has no effect if the timer is in CountUp mode.
    /// </summary>
    /// <param name="adjustedTimeLimit">
    /// The number of seconds to add (positive) or subtract (negative) from the countdown limit.
    /// </param>
    protected virtual void AdjustTimeLimit(float adjustedTimeLimit) {
        if (timerFunction == TimerFunction.CountDown) {
            timeLimit += adjustedTimeLimit;
        }
    }
    /// <summary>
    /// Resets the timer count according to the selected TimerFunction.
    /// Optionally pauses the timer immediately after resetting.
    /// </summary>
    /// <param name="pauseOnReset">
    /// If true, the timer will be paused after reset.  
    /// If false, the timer will continue running from the reset value.
    /// </param>
    protected virtual void ResetTimer(bool pauseOnReset) {
        if (pauseOnReset) {
            timerIsPaused = true;
        }
        
        switch (timerFunction) {
            case TimerFunction.CountUp:
                currentTimerCount = 0;
                break;
            case TimerFunction.CountDown:
                currentTimerCount = timeLimit;
                break;
        }
    }
    /// <summary>
    /// Toggles between paused and unpaused timer states.
    /// </summary>
    protected virtual void PauseUnpauseTimer() { 
        timerIsPaused = !timerIsPaused;
    }
    #endregion
}