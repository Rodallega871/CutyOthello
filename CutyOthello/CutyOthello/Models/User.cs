using System;
using System.Collections.Generic;
using System.Text;

namespace CutyOthello.Models
{
    public class User : Model
    {
        public string GameStatus { get;  set; }
        public int CompleteCount { get;  set; }
        public string StoryModeInfo { get;  set; }
        public string BattleModeInfo { get;  set; }
        public string StoryBoardInfo { get;  set; }
        public string BattleBoardInfo{ get;  set; }
    }
}
