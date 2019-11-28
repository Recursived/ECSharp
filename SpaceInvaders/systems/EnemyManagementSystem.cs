using ECSharp.core;
using SpaceInvaders.nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.systems
{
    class EnemyManagementSystem : Systeme
    {
        #region fields
        private readonly Node enemynode = new EnemyNode();
        private readonly Node blocknode = new EnemyBlockNode();
        private readonly Node gamenode = new GameStateNode();

        private LinkedList<Node> lst_enemies;
        private LinkedList<Node> lst_enemyblock;
        private LinkedList<Node> lst_game;

        private Size size;
        #endregion
        public EnemyManagementSystem(Size s)
        {
            enemynode.SetUp();
            blocknode.SetUp();
            gamenode.SetUp();
            size = s;

        }

        public override void AddIntoEngine(Engine e)
        {
            lst_enemies = e.GetNodeList(enemynode);
            lst_enemyblock = e.GetNodeList(blocknode);
            lst_game = e.GetNodeList(gamenode);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst_enemies = null;
            lst_enemyblock = null;
            lst_game = null;
        }

        public override void update(float time, Graphics g)
        {
            GameStateNode gstatenode = null;
            if (lst_game.Count > 0) { gstatenode = (GameStateNode)lst_game.First(); }
            

        }
    }
}
