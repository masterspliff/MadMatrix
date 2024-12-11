# List Components Architecture

## Overview
The list components in this directory implement a flexible and reusable pattern for displaying different types of items in a consistent way. The architecture uses Blazor's RenderFragment feature to enable component composition while maintaining clean separation of concerns.

## Components Structure

### ListContainer
- Acts as a wrapper component providing consistent styling
- Uses RenderFragment to accept child content
- Applies common list-group styling to all list types

### Type-Specific Lists (EventList, TaskList, LocationList)
Each list component:
- Accepts a generic IEnumerable of its specific type (TaskEvent, TaskItem, Location)
- Implements consistent button styling and click behavior
- Uses EventCallback for handling item selection
- Provides null-safe enumeration with Enumerable.Empty() fallback

## Usage Pattern

```csharp
<ListContainer>
    <EventList Items="events" OnItemSelected="HandleEventSelected" />
</ListContainer>
```

## Benefits
1. **Separation of Concerns**
   - Each list type handles only its specific data type
   - Common styling is centralized in ListContainer
   - Selection logic is delegated to parent components

2. **Reusability**
   - Lists can be used independently or within ListContainer
   - Consistent interface across all list types
   - Easy to add new list types following the same pattern

3. **Maintainability**
   - Changes to styling can be made in one place
   - Each list component is small and focused
   - Clear component hierarchy

4. **Type Safety**
   - Each list is strongly typed to its data model
   - Compile-time checking of item properties
   - IntelliSense support for development

## Adding New List Types
To add a new list type:
1. Create a new component following the existing pattern
2. Define Parameters for Items and OnItemSelected
3. Implement the item rendering logic
4. Use within ListContainer as needed
