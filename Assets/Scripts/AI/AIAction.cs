using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public abstract class AIAction : MonoBehaviour {
        public abstract void Execute(AIToken token);
    }
}
