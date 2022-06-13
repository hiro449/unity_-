using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rounding
{
   public static Vector2 Round(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public static Vector3 Round(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

}
