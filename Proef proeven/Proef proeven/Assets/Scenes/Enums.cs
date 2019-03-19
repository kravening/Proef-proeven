using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Enums
    {
        public enum GamePhase
        {
            None,
            NextTurn,
            MovementPhase,
            EventPhase,
            PickupPhase,
            DefendPhase,
            BattlePhase,
            RestPhase

        }
    }
}

