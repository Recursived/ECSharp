﻿using ECSharp.core;
using static SpaceInvaders.Game;

namespace SpaceInvaders.components
{
    class GameState : Component
    {
        public State state;
        public int score;
        public int lives;
        public int level;
        public int enemiesCount;

        public GameState(int lives) : base()
        {
            this.lives = lives;
            score = 0;
            enemiesCount = 0;
            level = 1;
            state = State.Begin;
        }

        public GameState(GameState gs) : this(gs.lives) { }
        public override Component CreateCopy()
        {
            return new GameState(this);
        }
    }
}
