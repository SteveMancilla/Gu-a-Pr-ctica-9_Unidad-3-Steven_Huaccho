using UnityEngine;
using NUnit.Framework;

public class GameLogicTests
{

    // Una prueba simple que verifica la inicialización correcta. 
    [Test]
    public void GameLogic_Initialization_SetsObjectiveCorrectly()
    {
        // ARRANGE: Preparamos el escenario de la prueba
        GameLogic gamelogic;

        // ACT: Ejecutamos la acción que queremos probar (en este caso, el constructor)
        gamelogic = new GameLogic(5);

        // ASSERT: Verificamos que el  resultado es el esperado
        Assert.AreEqual(5, gamelogic.ObjectivesToWin);
        Assert.AreEqual(0, gamelogic.ObjectivesCompleted);
        Assert.IsFalse(gamelogic.IsVictoryConditionMet);
    }

    [Test]
    public void GameLogic_Constructor_ReplacesZeroWithOne()
    {
        // ARRANGE & ACT
        var gameLogic = new GameLogic(0);

        // ASSERT
        Assert.AreEqual(1, gameLogic.ObjectivesToWin,
            "Si se pasa 0, el sistema debe reemplazarlo por 1 para evitar valores inválidos.");
        Assert.AreEqual(0, gameLogic.ObjectivesCompleted);
        Assert.IsFalse(gameLogic.IsVictoryConditionMet);
    }

    [Test]
    public void GameLogic_Constructor_ReplacesNegativeValueWithOne()
    {
        // ARRANGE & ACT
        var gameLogic = new GameLogic(-10);

        // ASSERT
        Assert.AreEqual(1, gameLogic.ObjectivesToWin,
            "Si se pasa un número negativo, debe normalizarse a 1.");
        Assert.AreEqual(0, gameLogic.ObjectivesCompleted);
        Assert.IsFalse(gameLogic.IsVictoryConditionMet);
    }

    [Test]
    public void GameLogic_CompleteObjective_IncrementsCount()
    {
        // ARRANGE
        var gameLogic = new GameLogic(3);

        // ACT
        gameLogic.CompleteObjective();

        // ASSERT
        Assert.AreEqual(1, gameLogic.ObjectivesCompleted,
            "Después de completar un objetivo, el contador debe incrementarse en 1.");
        Assert.IsFalse(gameLogic.IsVictoryConditionMet,
            "Todavía no se debe cumplir la condición de victoria.");
    }

    [Test]
    public void GameLogic_CompleteObjective_DoesNotExceedObjectivesToWin()
    {
        // ARRANGE
        var gameLogic = new GameLogic(2);

        // ACT
        gameLogic.CompleteObjective(); // 1/2
        gameLogic.CompleteObjective(); // 2/2
        gameLogic.CompleteObjective(); // intento extra, debería ignorarse

        // ASSERT
        Assert.AreEqual(2, gameLogic.ObjectivesCompleted,
            "El número de objetivos completados no debe superar ObjectivesToWin.");
        Assert.IsTrue(gameLogic.IsVictoryConditionMet,
            "Una vez alcanzado el máximo, la condición de victoria debe ser verdadera.");
    }

    [Test]
    public void GameLogic_IsVictoryConditionMet_ReturnsTrue_WhenReachedGoal()
    {
        // ARRANGE
        var gameLogic = new GameLogic(3);

        // ACT
        gameLogic.CompleteObjective(); // 1/3
        gameLogic.CompleteObjective(); // 2/3
        gameLogic.CompleteObjective(); // 3/3

        // ASSERT
        Assert.IsTrue(gameLogic.IsVictoryConditionMet,
            "Cuando ObjectivesCompleted >= ObjectivesToWin, debe indicar victoria.");
    }

    [Test]
    public void GameLogic_IsVictoryConditionMet_ReturnsFalse_WhenNotEnoughObjectives()
    {
        // ARRANGE
        var gameLogic = new GameLogic(3);

        // ACT
        gameLogic.CompleteObjective(); // 1/3
        gameLogic.CompleteObjective(); // 2/3

        // ASSERT
        Assert.IsFalse(gameLogic.IsVictoryConditionMet,
            "Si aún no se alcanza el objetivo, no debe considerarse victoria.");
    }
}