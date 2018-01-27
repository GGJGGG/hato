using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipsManager : MonoBehaviour {
    public GameObject BattleShips;
	// Use this for initialization

    private void OnBecameVisible()
    {
        BattleShips.GetComponent<BattleShips>().BattleShipsStart();
    }
}
