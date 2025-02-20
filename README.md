# Snake - ECS Implementation in C#

## Description
This is a modern implementation of the classic Snake game, developed in C# using an Entity-Component-System (ECS) architecture. The game is built with Raylib-cs, providing a clean and modular design for managing game entities and behaviors.

## Features
- ECS-based architecture for scalability and flexibility.
- Simple but engaging gameplay with smooth movement.
- Raylib-cs for rendering lightweight and fast graphics.
- Classic snake mechanics (growing, collisions, etc.).

## Installation & Setup
### Prerequisites
- .NET SDK (>= 6.0)
- Raylib-cs library
- A compatible C# IDE (e.g., Visual Studio, Rider, VS Code)

### Clone the Repository
```sh
git clone <repository-url>
cd Snake
```

### Install Dependencies
```sh
dotnet restore
```

### Run the Game
```sh
dotnet run --project Snake.csproj
```

## Project Structure
```
Snake/
│── README.md          # Project documentation
│── Snake.csproj       # C# project file
│── assets/            # Game assets (sprites, sounds, etc.)
│── src/               # Source code
│   ├── Systems/       # ECS systems (Rendering, Input, etc.)
│   ├── Components/    # ECS components (Position, Velocity, etc.)
│   ├── Entities/      # Snake, Food, etc.
│   ├── Game.cs        # Main game loop
│── bin/               # Build output (ignored in Git)
│── obj/               # Intermediate build files (ignored in Git)
```

## How to Play
- Space to Start the Game.
- Arrow Keys → Move the snake.
- Eat food to grow longer.
- Avoid colliding with yourself or the walls.

## Contributing
Feel free to fork the repository and submit pull requests. Suggestions and improvements are always welcome.

## License
This project is licensed under the MIT License.
