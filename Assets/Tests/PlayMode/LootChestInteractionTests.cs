using System.Collections;
using System.Reflection;          // IMPORTANTE para acceder a campos privados
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LootChestInteractionTests
{
    [UnityTest]
    public IEnumerator LootChest_Interact_OpensChestAndBecomesNonInteractable()
    {
        // ARRANGE: Creamos un cofre en escena de prueba
        var chestGO = new GameObject("TestChest");
        var lootChest = chestGO.AddComponent<LootChestController>();

        // Obtenemos referencia al campo privado _isOpen mediante reflexión
        var isOpenField = typeof(LootChestController)
            .GetField("_isOpen", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.IsNotNull(isOpenField, "No se encontró el campo privado _isOpen en LootChestController.");

        // ACT 1: primera interacción
        lootChest.Interact();

        // ASSERT 1: después de la primera interacción debe estar abierto
        bool isOpenAfterFirst = (bool)isOpenField.GetValue(lootChest);
        Assert.IsTrue(isOpenAfterFirst, "El cofre debería estar abierto después de la primera interacción.");

        // Esperamos un frame (buena práctica en tests de PlayMode)
        yield return null;

        // ACT 2: intentamos interactuar otra vez
        lootChest.Interact();

        // ASSERT 2: debe seguir abierto (no volver a cerrarse)
        bool isOpenAfterSecond = (bool)isOpenField.GetValue(lootChest);
        Assert.IsTrue(isOpenAfterSecond, "El cofre debería permanecer abierto después de la segunda interacción.");

        // Limpieza
        Object.Destroy(chestGO);
    }
}