using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class AnvilController : Interactable
{

    public override void Interact()
    {
        PlayerController.player.sword.durability = PlayerController.player.sword.maxDurability;
        AudioManager.PlayVariedEffect("SwordRepair");
    }
}
