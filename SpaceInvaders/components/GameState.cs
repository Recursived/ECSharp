using ECSharp.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SpaceInvaders.Game;

namespace SpaceInvaders.components
{
    class GameState : Component
    {
        public State state;
        public int score;
        public int lives;
        public int level;

        public GameState(int lives) : base()
        {
            this.lives = lives;
            score = 0;
            level = 0;
            state = State.Begin;
        }

        public GameState(GameState gs) : this(gs.lives) { }
        public override Component CreateCopy()
        {
            return new GameState(this);
        }
    }
}
