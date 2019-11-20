using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.systems
{
    class MovementSystem : Systeme
    {
        public LinkedList<Node> lst;
        private readonly Node n = new MovementNode();
        private readonly Size size;

        public MovementSystem(Size s) : base()
        {
            size = s;
            n.SetUp();
        }

        public override void AddIntoEngine(Engine e)
        {
            lst = e.GetNodeList(n);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst = null;
        }

        public override void update(int time, Graphics g)
        {
            foreach (Node n in lst)
            {
                MovementNode mn = (MovementNode)n;
                Velocity v = mn.vitesse;
                Position p = mn.pos;

                // On vérifie qu'on ne sort pas de l'écran on fonction de la size
                



                

            }
        }
    }
}
