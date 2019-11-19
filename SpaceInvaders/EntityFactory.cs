using ECSharp.core;
using SpaceInvaders.components;
using System;
using System.Collections.Generic;
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

        public Entity createDisplaybleEntity()
        {
            Entity entity = new Entity();
            entity.AddComponent(new Position(50, 50))
                .AddComponent(new Display(Properties.Resources.ship3));
            e.AddEntity(entity);
            return entity;
        }
    }
}
