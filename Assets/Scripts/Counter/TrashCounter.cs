using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrash;
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            player.DestroyKitchenObject();
            OnObjectTrash?.Invoke(this, EventArgs.Empty);
        }
    }
    public static void ClearStaticData()
    {
        OnObjectTrash = null;
    }
}
