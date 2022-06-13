using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private Block[] Blocks;

    Block RandomBlocks()
    {
        int i = Random.Range(0, Blocks.Length);

        if (Blocks[i])
        {
            return Blocks[i];
        } else
        {
            return null;
        }
    }

    public Block SpawnBlock()
    {
        Block block = Instantiate(RandomBlocks(), transform.position, Quaternion.identity);

        if (block)
        {
            return block;
        } else
        {
            return null;
        }

    }


}
