using MineSweeper.Class;
using MineSweeper.ConsoleUI;

UI Ui = new UI();

GI.initializeConsoleWindow();

int fieldDimensionX = 5;
int fieldDimensionY = 4;

int numberOfMines = 4;
    
Minefield field = new Minefield(fieldDimensionX,fieldDimensionY, numberOfMines);

RunGame();

Console.Read();




void RunGame()
{
    GI.RenderScreen(field);
}