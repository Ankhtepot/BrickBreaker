using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes {
    class ScoreItem {
        public readonly DateTime Date;
        public readonly int Score;

        public ScoreItem(int score) {
            Score = score;
            Date = DateTime.Now;
        }
    }
}
