using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGameDataLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<GameMaster>().DeleteGameData();
    }
}
