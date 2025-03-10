# ECS Singleton Implementation

This document describes the singleton entity pattern implementation for our custom ECS architecture.

## Overview

Singleton entities are special entities that exist only once in the game world, such as:
- Perfect Line
- Starting Note
- Game Score
- World State

Since these entities don't benefit from the chunk-based storage optimizations that are designed for many similar entities, we've implemented a specialized storage system for them.

## How It Works

### 1. Marking Components as Singleton Flags

Components that identify a singleton implement the `ISingletonFlag` interface:

```csharp
public struct PerfectLineTagComponent : IComponent, ISingletonFlag
{
    public float PerfectLineWidth;
}
```

### 2. Optimized Storage

Instead of storing singleton entities in regular chunks (which would waste memory), they are stored in a specialized `SingletonChunk` that only allocates memory for a single entity. Each component is stored in a single-element array to allow returning proper references.

```csharp
// Inside SingletonChunk
private Dictionary<Type, Array> components;

public ref T GetComponent<T>() where T : struct, IComponent
{
    if (!components.TryGetValue(typeof(T), out var componentArray))
    {
        // Create a new single-element array
        T[] newArray = new T[1] { default };
        components[typeof(T)] = newArray;
        componentArray = newArray;
    }
    
    T[] typedArray = (T[])componentArray;
    return ref typedArray[0]; // Return reference to the single element
}
```

This approach is memory-efficient while still allowing for proper reference semantics.

### 3. Creation API

Singleton entities are created the same way as regular entities:

```csharp
World.CreateEntity(
    new TransformComponent { Position = new Vector2(0, 4.0f) },
    new PerfectLineTagComponent { Width = 5.0f },
    new CornerComponent()
);
```

The system automatically detects the `ISingletonFlag` component and routes the entity to the singleton storage.

### 4. Query API

#### Getting Component References

Due to C# 7.3 limitations with tuples and ref returns, we need to get components one at a time:

```csharp
// Get a direct reference to a component
ref var perfectLine = ref World.GetSingleton<PerfectLineTagComponent, PerfectLineTagComponent>();

// Modify it directly
perfectLine.PerfectLineWidth = 3.0f;

// Get another component from the same singleton
ref var transform = ref World.GetSingleton<PerfectLineTagComponent, TransformComponent>();
transform.Position = new Vector2(0, 5.0f);
```

The changes made through these references are immediately reflected in the storage.

### 5. Entity ID Access

```csharp
// Get the entity ID if needed
int perfectLineId = World.GetSingletonEntityId<PerfectLineTagComponent>();
```

## Benefits

1. **Memory Efficiency**: No wasted arrays for singleton entities - only allocates what's needed
2. **Type Safety**: Compile-time checking of component types
3. **Clean API**: Intuitive access through type-based queries
4. **Performance**: Direct reference access without multiple indirections
5. **Integration**: Works seamlessly with existing systems through the World class
6. **Direct Modification**: Components can be modified in place through references

## Technical Implementation

The singleton implementation consists of three main parts:

1. **SingletonChunk**: Optimized storage for a single entity's components using single-element arrays for each component type.

2. **SingletonManager**: Manages all singleton entities, providing access methods that return references to components.

3. **World Integration**: The World class routes entity creation and component access through either the chunk system or singleton system as appropriate.

## How Reference Semantics Work

Since we can't use tuples with ref returns in C# 7.3, our approach uses single-element arrays for each component and returns them individually:

```csharp
// This is the key insight that enables our reference-based approach
T[] typedArray = (T[])componentArray;
return ref typedArray[0]; // Reference to array element persists
```

This is more efficient than the alternatives:
- Boxing/unboxing would require copying data
- Returning by value would lose any modifications
- Unsafe code would require external dependencies

## Example Usage

Here's a complete example of a system that uses singleton components:

```csharp
public class PerfectLineSystem : GameSystemBase
{
    protected override void Execute(float deltaTime)
    {
        // Get direct references one at a time
        ref var transform = ref World.GetSingleton<PerfectLineTagComponent, TransformComponent>();
        ref var perfectLine = ref World.GetSingleton<PerfectLineTagComponent, PerfectLineTagComponent>();
        
        // Modify directly - no need to update afterward
        transform.Position = new Vector2(transform.Position.x, 5.0f);
        perfectLine.PerfectLineWidth = 3.0f;
        
        // Changes are automatically reflected in the storage
    }
}
```

## C# Language Constraints

Due to limitations in C# 7.3, we can't use tuple returns with the ref modifier. In later C# versions (8.0+), it would be possible to return multiple component references via tuples, which would enable a more elegant API:

```csharp
// Not possible in C# 7.3, but would be nice in future versions
var (ref transform, ref perfectLine) = World.QuerySingleton<PerfectLineTagComponent, 
                                                    TransformComponent, 
                                                    PerfectLineTagComponent>();
```

For now, we have to get components one at a time using separate method calls.

## Future Improvements

- When C# version constraints are relaxed, implement tuple returns with ref modifiers
- Create editor tooling to visualize and debug singleton entities
- Add support for batch operations on singleton components
