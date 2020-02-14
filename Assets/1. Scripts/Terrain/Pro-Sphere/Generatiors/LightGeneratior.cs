using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LightGeneratior
{
    //Try static
    //TODO on Update functions:
    //Ray cast from the player and NPCs to the directional light if collision detected activate light on player
    //Ray cast from the special objects tp the directional light if collision detected activate light on object

    //public
    [SerializeField]
    static bool ownerIsInLight;
    //private
    private static GameObject[] owner;
    private static float range;

   public static bool ObjectIsLit(bool isLit)
    {

        return isLit;
    }
}
