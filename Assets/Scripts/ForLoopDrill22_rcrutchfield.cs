using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLoopDrill22_rcrutchfield : MonoBehaviour
{
    void Start()
    {
    int[] nums = new int[100];
    for (int i = 0; i < 100; i++)
    {
        nums[i] = (i % 5) + 4;
    }
    }
}
