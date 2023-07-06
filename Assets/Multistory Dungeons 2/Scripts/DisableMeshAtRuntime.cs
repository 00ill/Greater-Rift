using UnityEngine;

namespace manastation.multistorydungeons
{

    public class DisableMeshAtRuntime : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GetComponent<Renderer>().enabled = false;
        }

    }
}
