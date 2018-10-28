using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes {
    public class ScoreItem {
        public readonly DateTime Date;
        public readonly int Score;
        public readonly int HighestLevel;
        public bool isNewRecord;

        public ScoreItem(int score, int hl) {
            Date = DateTime.Now;
            Score = score;
            HighestLevel = hl;
            isNewRecord = true;
        }
    }
}
