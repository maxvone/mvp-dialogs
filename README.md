## Architecture Overview

This project follows a modular, scalable architecture designed for maintainability and testability. The core principles include separation of concerns, dependency injection, and game flow relying on State Machine.

### Key Architectural Patterns

#### 1. **Model-View-Presenter (MVP) Pattern**
- **Purpose**: Separates UI logic from presentation logic
- **Implementation**:
  - `BaseView`: Abstract MonoBehaviour base class for all UI views
  - `BasePresenter<TView>`: Abstract presenter base class that manages view lifecycle and business logic
  - Views handle only visual representation and user input events
  - Presenters contain all UI logic and communicate with services

#### 2. **Finite State Machine**
- **Purpose**: Manages game flow and scene transitions
- **Implementation**:
  - `GameStateMachine`: Core state machine implementation
  - States: `BootstrapState`, `LoadMenuState`, `MainMenuLoopState`, etc.
  - Each state handles specific game phases (loading, menus, gameplay)

#### 3. **DI Pattern**
- **Purpose**: Provides dependency injection and service management
- **Implementation**:
  - `AllServices.Container`: Singleton service registry
  - Services registrations happen in `BootstrapState`
  - Services implement `IService` interface
  - Examples: `SceneLoaderService`, `AssetProvider`

### Project Structure

```
Assets/
├── CodeBase/
│   ├── AssetManagement/     # Asset loading
│   ├── Infrastructure/      # Game bootstrapper and state machine
│   ├── Services/           # Service implementations and interfaces
│   └── UI/
│       └── Mvp/            # MVP pattern base classes and implementations
│           └── MvpImpl     # MVP pattern specific implementations
├── Scenes/                 # Unity scenes (BootScene, MainMenuScene)
├── StaticData/            # ScriptableObject-based configuration data
```

### Key Technologies

- **Unity ** with Universal Render Pipeline
- **UniTask**: For async/await support in Unity
- **Unity Addressables**: For asset loading
- **Input System**: Modern input handling
- **ScriptableObjects**: For static data management

### Game Flow

1. **Bootstrap**: `GameBootstrapper` initializes the game and enters `BootstrapState`
2. **Load Menu**: Transitions to `LoadMenuState` to load the main menu scene
3. **Future States**: Additional states for puzzle gameplay, etc.

### Static Data Management

- Puzzle configurations stored in `PuzzlesStaticData` ScriptableObject
- `PuzzleData` contains puzzle metadata (ID, title, path)
- Easily extensible for additional puzzle types and configurations
- Can easily be replaced with server JSON and CDN

### Development Guidelines

- **UI Components**: Always use MVP pattern - create View and Presenter pairs
- **Services**: Register new services in `BootstrapState`
- **States**: Add new game states by implementing `IState` interface
- **Async Operations**: Prefer UniTask over coroutines and Tasks for better performance

This architecture provides a solid foundation for a puzzle game with dialogs, allowing for easy expansion of features while maintaining clean, testable code.