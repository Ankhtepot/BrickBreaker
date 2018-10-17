using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBWaypoints : MonoBehaviour {

    [SerializeField] List<GameObject> waypoints;

    public List<GameObject> GetWaypoints() {
        return waypoints;
    }
}
