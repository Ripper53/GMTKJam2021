using UnityEngine;

public class View : MonoBehaviour {
    public    Transform     rootCore;
        private     Transform       rootX;
        private     Transform       rootZ;

    void Start () {
        if( rootCore != null ) {
            rootZ = rootCore.GetChild ( 0 );
            rootX = rootZ.GetChild ( 0 );
        }    
    }

    void Update () {

    }
}
