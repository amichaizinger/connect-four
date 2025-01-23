# FourInARow Game

This is a simple implementation of the classic "Four In A Row" (Connect Four) game, built using C# and WPF (Windows Presentation Foundation). The game allows two players to take turns dropping discs into a grid, aiming to align four of their discs either horizontally, vertically, or diagonally.

### Features:
- **Two-player mode**: Players can alternate turns, represented by different colors (Blue and Red).
- **Game logic**: Includes win detection for horizontal, vertical, and diagonal (both up-right and down-right) alignments of four discs.
- **Column management**: Tracks full columns to prevent further moves in them.
- **Player turn switching**: Alternates between Blue and Red players after each turn.

### Technologies:
- **C#**: Used for game logic and event handling.
- **WPF**: Provides a graphical user interface for the game board and player interaction.

### How to Play:
1. Two players take turns to drop their colored discs into any of the seven columns.
2. The objective is to get four of your discs in a row, either horizontally, vertically, or diagonally.
3. The game ends when a player wins or all columns are full without a winner.

### Instructions to Run:
1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Build and run the project.
