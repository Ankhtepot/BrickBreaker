using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Interfaces {
    interface IBoss {
        void Dying();

        void OnArrival();

        void OnCollisionEnter2D(Collision2D collision);

        void OnDeath();

        void StartEncounter();
    }
}
