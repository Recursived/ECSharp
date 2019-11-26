using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.nodes;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.systems
{
    class MovementSystem : Systeme
    {
        public LinkedList<Node> lstmovement;
        public LinkedList<Node> lstgun;
        private readonly Node mn = new MovementNode();
        private readonly Node gc = new GunControlNode();
        private readonly Size size;
        private EntityFactory factory;

        public MovementSystem(Size s, EntityFactory f) : base()
        {
            size = s;
            mn.SetUp();
            gc.SetUp();
            factory = f;
        }

        public override void AddIntoEngine(Engine e)
        {
            lstmovement = e.GetNodeList(mn);
            lstgun = e.GetNodeList(gc);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lstmovement = null;
        }

        public override void update(float time, Graphics g)
        {
            if (runnable)
            {
                GunControlNode gunnode = (GunControlNode) lstgun.First();
                foreach (Node n in lstmovement.ToList()) // We transform to list to entity from node
                {
                    MovementNode mn = (MovementNode)n;
                    Velocity v = mn.vitesse;
                    Position p = mn.pos;
                    Vector2D newv = time * v.speedVect + p.point;
                    if (newv.x < size.Width || newv.x > 0)
                    {
                        p.point = newv;
                    }


                    if (newv.y < 0)
                    {
                        factory.removeEntity(mn.entity);
                        gunnode.gun.shoot = true;
                    }
                }
                
            }
            
        }
    }
}
