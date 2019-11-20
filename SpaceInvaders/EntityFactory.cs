using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EntityFactory
    {

        private Engine e;

        public EntityFactory(Engine e)
        {
            this.e = e;
        }

        public Entity CreateDisplaybleEntity(Size s)
        {
            Entity entity = new Entity();
            entity.AddComponent(new Position((s.Width / 7) * 3, (int)(s.Height * 0.85)))
                .AddComponent(new Display(Properties.Resources.ship3));
            e.AddEntity(entity);
            return entity;
        }

        public List<Entity> CreateBunkerEntities(Size s)
        {
            List<Entity> bunkers = new List<Entity>();
            Entity b1 = new Entity();
            Entity b2 = new Entity();
            Entity b3 = new Entity();
            b1.AddComponent(new Position((s.Width / 7), (int) (s.Height * 0.7)))
                .AddComponent(new Display(Properties.Resources.bunker));
            b2.AddComponent(new Position((s.Width / 7)*3, (int)(s.Height * 0.7)))
               .AddComponent(new Display(Properties.Resources.bunker));
            b3.AddComponent(new Position((s.Width / 7)*5, (int)(s.Height * 0.7)))
               .AddComponent(new Display(Properties.Resources.bunker));
            e.AddEntity(b1);
            e.AddEntity(b2);
            e.AddEntity(b3);
            bunkers.Add(b1);
            bunkers.Add(b2);
            bunkers.Add(b3);
            return bunkers;

        }
    }
}
