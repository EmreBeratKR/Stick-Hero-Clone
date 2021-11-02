using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    [SerializeField] private GameObject stickPrefab;
    [SerializeField] private float stickOffsetX;
    private GameObject stick;
    private PillarManager pillarManager;


    void Start()
    {
        pillarManager = GetComponent<PillarManager>();
    }

    public void spawnStick()
    {
        Transform crnt = pillarManager.currentPillar.transform;
        stick = Instantiate(stickPrefab, new Vector3(crnt.position.x + crnt.localScale.x - stickOffsetX, 0f), Quaternion.identity);
    }
}
