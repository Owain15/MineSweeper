using MineSweeper.Class;
using MineSweeper.ConsoleUI;

UI Ui = new UI();

GI.initializeConsoleWindow();

int fieldDimensionX = 20;
int fieldDimensionY = 10;

int numberOfMines = 5;
    
Minefield field = new Minefield(fieldDimensionX,fieldDimensionY, numberOfMines);

RunGame();

Console.Read();




void RunGame()
{
    GI.RenderScreen(field);
}