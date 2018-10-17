using Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes {

    public abstract class BossAttack : MonoBehaviour, IBossAttack {

        public abstract void Activate();
    }
}
