using System.Diagnostics.CodeAnalysis;

namespace Presentation.Console.Scenarios.Interfaces;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}