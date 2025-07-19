# Custom Attributes

## Absolute Value Attribute
Automatically converts any numeric value assigned in the Inspector to its absolute (positive) equivalent.
Prevents negative values from being assigned where only positives make sense (e.g., speed, time, health).

Works with: `int`, `float`, `double`
```csharp
[AbsoluteValue]
public float positiveVariable;
```

## Negative Value Attribute
Automatically ensures that the value assigned to a numeric field in the Inspector is always negative.
If a positive number is entered, it will be converted to its negative equivalent when the value is applied.

Works with: `int`, `float`, `double`
```csharp
[NegativeValue]
public float negativeVariable;
```

## Min Value Attribute
Restricts a numeric field in the Inspector to a specified minimum value.
If a lower value is entered, it will be automatically clamped to the defined minimum.

Works with: `int`, `float`, `double`
```csharp
[MinValue(4)]
public int minimumVariable;
```

## Max Value Attribute
Restricts a numeric field in the Inspector to a specified maximum value.
If a higher value is entered, it will be automatically clamped to the defined maximum.

Works with: `int`, `float`, `double`
```csharp
[MaxValue(40)]
public int maximumVariable;
```

## Show If Attribute
Displays a field in the Inspector **only when** a specified boolean variable is `true`.
If the condition is false, the field is **hidden**.
```csharp
public bool enableDebug;

[ShowIf("enableDebug", true)]
public string showIfVariable;
```

## Hide If Attribute
Hides a field in the Inspector **when** a specified boolean variable is `true`.
If the condition is `false`, the field will be **visible**.
```csharp
public bool enableDebug;

[HideIf("enableDebug", true)]
public float hideIfVariable;
```

## Conditional Hide Attribute
Disables editing of a field in the Inspector **based on the value of another boolean variable**.
Unlike `ShowIf` or `HideIf`, the field remains visible, but becomes read-only when the condition is not met.

This attribute provides a flexible way to **lock fields conditionally**, helping prevent unintended edits without hiding important information.
```csharp
public bool conditionalVariable;

[ConditionalHide("conditionalVariable", false)]
public string conditionalVariableTest;
```

## Conditional Enum Hide Attribute
Disables editing of a field in the Inspector **based on the value of an enum**.
The field remains visible but becomes **read-only** when the enum **matches** (**or doesn't match**) a specified **value**.

This attribute is useful for controlling when fields should be editable based on the selected mode, state, or type represented by an enum.
```csharp
public enum Directions {Left, Right, Up, Down, Forward, Backward}
public Directions conditionalDirection;

[ConditionalEnumHide("conditionalDirection", (int)Directions.Forward)]
public Directions forwardDirection;
```

## Not Nullable Attribute
Highlights any `null` object reference fields in red within the Unity Inspector — both the field value and the field label.

This attribute ensures critical references are not left unassigned, making it easier to catch missing dependencies during development.
```csharp
[NotNullable]
public GameObject notNullableObject;
```

## Read Only Attribute
Prevents a variable's value from being edited in the Unity Inspector, while still displaying it for reference. This attribute works with any data type, including primitives, structs, Unity objects, and custom classes.

Useful for debugging, visualizing internal state, or protecting values that are set at runtime or elsewhere in code.
```csharp
[ReadOnly]
public int readOnlyVariable;
```

## Tag Attribute
Replaces a `string` field with a dropdown menu populated by Unity's existing Tags.
```csharp
[Tag]
public string gameTags;
```

## Line Divider Attribute
Visually separates fields in the Inspector with a customizable horizontal line to improve layout and readability.

- Supports multiple colors and thickness levels for visual organization.

- Useful for grouping related properties or emphasizing sections.

Available colors:
`Orange`, `Yellow`, `Indigo`, `Violet`, `Clear`, `White`, `Black`, `Green`, `Blue`, `Gray`, `Pink`, `Red`

```csharp
[LineDivider(1, LineColors.Gray)]
```

## Enum Flags Attribute
Displays an `enum` field using Unity's multi-select flag UI in the Inspector.

Great for selecting multiple options from a set.
```csharp
public enum Directions {Left, Right, Up, Down, Forward, Backward}
[EnumFlags] public Directions direction;
```