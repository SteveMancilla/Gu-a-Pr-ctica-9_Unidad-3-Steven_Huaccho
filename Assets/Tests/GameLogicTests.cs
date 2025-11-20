using UnityEngine;
using NUnit.Framework;

public class GameLogicTests
{
    //Una prueba simple que verifica la inicialización correcta. 
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
}
