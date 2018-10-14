using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Interfaces {
    interface IPlayList {
        SoundSystem.PlayListID GetPlayListID();
    }
}
