using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LootChestOptimizationTests
{
    [UnityTest]
    public IEnumerator LootChest_DoesNotCallCostlyFunctionOnFailedInteraction()
    {
        // ARRANGE
        var chestGO = new GameObject("TestChest");
        var lootChest = chestGO.AddComponent<LootChestController>();

        // 1ª interacción: abre el cofre
        lootChest.Interact();

        // Comprobamos que el cofre está abierto
        // (usando reflection sobre _isOpen, o una propiedad IsOpen si quisieras)
        var isOpenField = typeof(LootChestController)
            .GetField("_isOpen", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        bool isOpen = (bool)isOpenField.GetValue(lootChest);
        Assert.IsTrue(isOpen, "El cofre debería estar abierto tras la primera interacción.");

        // Y que la función costosa SÍ se llamó en la apertura
        Assert.IsTrue(lootChest.CostlyFunctionWasCalled,
            "La función costosa debería haberse llamado al abrir el cofre.");

        yield return null;

        // 2ª interacción: cofre ya abierto (interacción fallida)
        lootChest.Interact();

        // Ahora la función costosa NO debería haberse llamado
        Assert.IsFalse(lootChest.CostlyFunctionWasCalled,
            "En la interacción fallida (cofre ya abierto) la función costosa no debería llamarse.");

        Object.DestroyImmediate(chestGO);
    }
}