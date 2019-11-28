using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class EntityFactory
    {

        private Engine e;
        private Dictionary<string,int> cachePixel = new Dictionary<string, int>();

        public EntityFactory(Engine e)
        {
            this.e = e;
        }

        public void removeEntity(Entity entity)
        {
            e.RemoveEntity(entity);
        }

        public Entity CreateGameEntity()
        {
            Entity entity = new Entity();
            entity.AddComponent(new GameState(3));
            e.AddEntity(entity);
            return entity;
        }

        public Entity CreateDisplaybleEntity(Size s)
        {
            Entity entity = new Entity();
            Position p = new Position((s.Width / 7) * 3, (int)(s.Height * 0.85));
            entity.AddComponent(p)
                .AddComponent(new Display(Properties.Resources.ship3))
                .AddComponent(new Gun(p.point))
                .AddComponent(new GunControl(Keys.Space))
                .AddComponent(new SpaceShipControl(Keys.Left, Keys.Right, new Velocity(300, 0, 0)));
            e.AddEntity(entity);
            return entity;
        }

        public Entity CreateSpaceShipBullet(Gun g)
        {
            Entity entity = new Entity();
            entity.AddComponent(new Bullet(1))
                .AddComponent(new Position(g.shootingPoint, 0))
                .AddComponent(new Velocity(0, -300, 0))
                .AddComponent(new Display(Properties.Resources.shoot1));
            e.AddEntity(entity);
            return entity;
        }

        /*public Entity CreateEnemyBullet(Gun g, Enemy.Type t)
        {
            Entity entity = new Entity();
            entity.AddComponent(new Bullet(1))
                .AddComponent(new Position(g.shootingPoint, 0))
                .AddComponent(new Velocity(0, 300, 0))
                .AddComponent(new Display(Properties.Resources.shoot1));
            e.AddEntity(entity);
            return entity;
        }*/


        public Entity CreateBonus(Position p, Bonus.Type type)
        {
            Entity entity = new Entity();
            entity.AddComponent(new Bonus(type))
                .AddComponent(new Position(p))
                .AddComponent(new Velocity(0, 300, 0))
                .AddComponent(new Display(Properties.Resources.bonus));
            e.AddEntity(entity);
            return entity;
        }
        public List<Entity> CreateEnemyEntities(Size s)
        {
            List<Entity> entities = new List<Entity>();
            Bitmap[] arr_bitmap = 
            {
                Properties.Resources.ship1,
                Properties.Resources.ship2,
                Properties.Resources.ship6,
                Properties.Resources.ship5,
                Properties.Resources.ship5,
                Properties.Resources.ship5,
            };
            Enemy.Type[] arr_type =
            {
                Enemy.Type.Big,
                Enemy.Type.Big,
                Enemy.Type.Medium,
                Enemy.Type.Small,
                Enemy.Type.Small,
                Enemy.Type.Small,
            };

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Entity ent = new Entity();
                    Position p = new Position((s.Width / 20) * (2*j), (s.Height / 17) * i);
                    ent.AddComponent(new Display(arr_bitmap[i]))
                        .AddComponent(p)
                        .AddComponent(new Gun(p.point))
                        .AddComponent(new Enemy(1, arr_type[i], s.Width / 14, s.Height / 15));
                    entities.Add(ent);
                    e.AddEntity(ent);
                }
            }
            Entity eb = new Entity();
            eb.AddComponent(new EnemyBlock(0, 0, (s.Width / 20) * 6, (s.Height / 17) * 7));
            entities.Add(eb);
            e.AddEntity(eb);
            return entities;
        }

        public List<Entity> CreateBunkerEntities(Size s)
        {
            List<Entity> bunkers = new List<Entity>();
            Entity b1 = new Entity();
            Entity b2 = new Entity();
            Entity b3 = new Entity();
            b1.AddComponent(new Position((s.Width / 7), (int) (s.Height * 0.7)))
                .AddComponent(new Display(Properties.Resources.bunker))
                .AddComponent(new Bunker(GetLifeFromPixel(Properties.Resources.bunker, "bunker")));
            b2.AddComponent(new Position((s.Width / 7)*3, (int)(s.Height * 0.7)))
               .AddComponent(new Display(Properties.Resources.bunker))
               .AddComponent(new Bunker(GetLifeFromPixel(Properties.Resources.bunker, "bunker")));
            b3.AddComponent(new Position((s.Width / 7)*5, (int)(s.Height * 0.7)))
               .AddComponent(new Display(Properties.Resources.bunker))
               .AddComponent(new Bunker(GetLifeFromPixel(Properties.Resources.bunker, "bunker")));
            e.AddEntity(b1);
            e.AddEntity(b2);
            e.AddEntity(b3);
            bunkers.Add(b1);
            bunkers.Add(b2);
            bunkers.Add(b3);
            return bunkers;

        }

        private int GetLifeFromPixel(Bitmap b, string entityname)
        {
            
            if (!cachePixel.ContainsKey(entityname))
            {
                int black_pixels = CountPixels(b, Color.FromArgb(255, 0, 0, 0));
                cachePixel.Add(entityname, black_pixels);
                return black_pixels;
            } 
            else
            {
                return cachePixel[entityname];
            }
        }

        private int CountPixels(Bitmap bm, Color target_color)
        {
            
            int matches = 0;
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    if (bm.GetPixel(x, y) == target_color) matches++;
                }
            }
            return matches;
        }
    }
}
