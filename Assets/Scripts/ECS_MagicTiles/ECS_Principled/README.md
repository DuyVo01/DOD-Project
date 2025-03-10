# Cache-Aware ECS Implementation

This is a cache-optimized Entity Component System (ECS) implementation designed to maximize CPU cache utilization and improve performance in data-heavy games. The implementation focuses on organizing component data in memory layouts that minimize cache misses and provide efficient data access patterns.

## Key Features

- **Chunk-Based Storage**: Components are stored in chunks of fixed size, grouped by archetype
- **Block-Based Processing**: Process components in small cache-friendly blocks
- **Dynamic Component Registration**: Auto-register new component types at runtime
- **Implicit Archetype Creation**: Create archetypes automatically from component sets
- **Flexible Query System**: Easily query entities with a fluent interface
- **True Component References**: Get direct references to components without copying

## Core Classes

- **Chunk**: Stores entities of the same archetype in typed arrays
- **ChunkManager**: Manages chunks and provides entity and component access
- **EntityQuery**: Provides a fluent interface for querying and processing entities
- **World**: The main ECS world that manages entities, components, and systems
- **GameSystemBase**: Base class for implementing game systems
- **DelegateTypes**: Custom delegates for ref parameter support

## Usage Examples

### Creating Entities

```csharp
// Create an entity with automatic archetype detection
int entityId = world.CreateEntity(
    new TransformComponent { Position = new Vector2(0, 0) },
    new MusicNoteComponent { Speed = 5.0f },
    new ActiveStateComponent { IsActive = true }
);

// Create an entity with an explicit archetype
int entityId = world.CreateEntityWithArchetype(
    Archetype.Registry.MusicNote,
    new object[] {
        new TransformComponent { Position = new Vector2(0, 0) },
        new MusicNoteComponent { Speed = 5.0f },
        /* other components for this archetype */
    }
);
```

### Processing Entities

```csharp
// Traditional approach
world.CreateQuery()
    .ForEach<TransformComponent, MusicNoteComponent>((ref TransformComponent transform, ref MusicNoteComponent note) => {
        transform.Position.y += note.Speed * deltaTime;
    });

// Cache-aware approach for better performance
world.CreateQuery()
    .ForEachBlock<TransformComponent, MusicNoteComponent>((ref TransformComponent transform, ref MusicNoteComponent note, int entityId) => {
        transform.Position.y += note.Speed * deltaTime;
    }, 64); // Block size of 64 entities

// Advanced block processing with direct array access
world.CreateQuery()
    .ForEachBlockDirect<TransformComponent, MusicNoteComponent>((TransformComponent[] transforms, MusicNoteComponent[] notes, int[] entityIds, int startIndex, int count) => {
        // Process the entire block in a cache-friendly way
        for (int i = 0; i < count; i++) {
            int index = startIndex + i;
            transforms[index].Position.y += notes[index].Speed * deltaTime;
        }
    }, 64);
```

### Creating Systems

```csharp
// Using custom delegates for ref parameters
public class MovingNoteSystem : GameSystemBase
{
    protected override void Execute(float deltaTime)
    {
        World.CreateQuery()
            .ForEachBlock<TransformComponent, MusicNoteComponent, ActiveStateComponent>(
                (ref TransformComponent transform, ref MusicNoteComponent note, ref ActiveStateComponent active, int entityId) =>
                {
                    if (active.IsActive)
                    {
                        transform.Position.y += note.Speed * deltaTime;
                    }
                }, 64);
    }
}
```

## Performance Considerations

- **Block Size Tuning**: 
  - Small components (< 16 bytes): 64-128 entities per block
  - Medium components (16-64 bytes): 32-64 entities per block
  - Large components (> 64 bytes): 16-32 entities per block

- **Component Design**:
  - Keep components small and focused
  - Group related data accessed together
  - Consider the memory layout of your components

- **System Design**:
  - Use block-based processing for performance-critical systems
  - Group related systems for better cache utilization
  - Profile and tune block sizes for your specific scenarios

## Testing and Integration

To test the system, add the `CacheAwareECSBootstrap` component to a GameObject in your scene. This will initialize the world, register systems, and create test entities. You can also run performance tests by enabling the `Run Performance Test` option and pressing the configured key during play mode.

For integrating with existing systems, the World class provides a backward compatibility layer through the `GetStorage` method, but it's recommended to update your systems to use the new query-based API for better performance.

## Extension Points

The system is designed to be extensible. Here are some potential extension points:

- **Component Change Detection**: Track when components are modified
- **Job System Integration**: Use Unity's Job System for parallel processing
- **Optimized Math**: Use SIMD or vectorized math operations
- **Entity Command Buffer**: Defer entity operations to avoid race conditions
- **Archetype Migration**: Handle adding/removing components efficiently

## Implementation Notes

This implementation avoids using advanced C# features like Span<T> and ref parameters in lambdas for better compatibility with Unity's .NET environment. Instead, it uses custom delegate types and array-based processing to achieve similar performance benefits.

## Credits

This implementation is inspired by Unity DOTS and other modern ECS frameworks, with a focus on cache efficiency and KISS principles.
