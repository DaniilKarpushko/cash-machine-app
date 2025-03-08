namespace Presentation.Console.Scenarios.Interfaces;

public interface IScenario
{
    string Name { get; }
    Task Run();
}