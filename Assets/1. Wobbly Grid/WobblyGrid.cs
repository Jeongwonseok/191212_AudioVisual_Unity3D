using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblyGrid : MonoBehaviour
{
    public GameObject Block;
    public GameObject[] BlockList;
    public Material BlockMat;
    public float BlockLength, BlockWidth, GridHeight, SpacingBetween, Scale, Amplitude, Frequency;

    public int GridX, GridZ, GridDivision;
    private int GridSize, BlockIndex;

    public bool MakeColors, AnimateBlocks;

    // Start is called before the first frame update
    void Start()
    {
        GridSize = GridX * GridZ;
        BlockList = new GameObject[GridSize];

        for(int x=1; x<=GridX; x++)
        {
            for(int z=1; z<=GridZ; z++)
            {
                GameObject BlockInstance = (GameObject)Instantiate(Block);
                BlockInstance.transform.parent = this.transform;
                BlockInstance.name = "Block " + BlockIndex;

                float Xtemp = 1;
                float Ztemp = 1;

                if(x > GridX/ GridDivision)
                {
                    Xtemp = (GridX / GridDivision) - (x - (GridX / GridDivision));
                }
                else
                {
                    Xtemp = x;
                }

                if (z > GridZ / GridDivision)
                {
                    Ztemp = (GridZ / GridDivision) - (z - (GridZ / GridDivision));
                }
                else
                {
                    Ztemp = z;
                }

                if(MakeColors)
                {
                    float ColorPercentR = Xtemp / GridX;
                    float ColorPercentG = (Xtemp * Ztemp) / (GridSize / 2);
                    float ColorPercentB = Ztemp / GridZ;

                    BlockMat.color = new Vector4(ColorPercentR, ColorPercentG, ColorPercentB, 1);
                    BlockInstance.GetComponent<Renderer>().material.color = BlockMat.color;
                }

                BlockList[BlockIndex] = BlockInstance;
                BlockList[BlockIndex].transform.position = new Vector3((x-1) * SpacingBetween, (Xtemp * Ztemp) * GridHeight, (z-1) * SpacingBetween);
                BlockList[BlockIndex].transform.localScale = new Vector3(BlockWidth, BlockLength, BlockWidth);
                BlockIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(AnimateBlocks)
        {
            for(int i=0; i<GridSize; i++)
            {
                float wave = Scale * Mathf.Sin(Time.fixedTime * Amplitude + (i * Frequency));
                float wave2 = Scale * Mathf.PerlinNoise(Time.fixedTime * Amplitude + (i * Frequency), Time.fixedTime * Amplitude + (i * Frequency));
                BlockList[i].transform.position = new Vector3(BlockList[i].transform.position.x, BlockList[i].transform.position.y + (wave * wave2) * Random.Range(1, 3), BlockList[i].transform.position.z);

            }
        }
    }
}
