using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VDFramework.Extensions;
using VDUnityFramework.BaseClasses;

public class FoliageGenerator : BetterMonoBehaviour
{
    public GameObject[] smallFoliages;

    private ShapeGenerator shapeGenerator;

    
    // Only spawn foliage at specific height;
    // Have function that takes the mesh, grabs its vertices, loop over them, check height, if height matches / falls within range → spawn foliage

    //Maybe static?
}
