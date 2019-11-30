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

        public LinkedList<Entity> GetEntities()
        {
            return e.GetEntities();
        }

        public Entity CreateGameEntity()
        {
            Entity entity = new Entity("game");
            entity.AddComponent(new GameState(3));
            e.AddEntity(entity);
            return entity;
        }

        public Entity CreateDisplaybleEntity(Size s, int life)
        {
            Entity entity = new Entity();
            Position p = new Position((s.Width / 7) * 3, (int)(s.Height * 0.85));
            entity.AddComponent(p)
                .AddComponent(new Display(Properties.Resources.ship3))
                .AddComponent(new Gun(p.point))
                .AddComponent(new GunControl(Keys.Space))
                .AddComponent(new SpaceShipControl(Keys.Left, Keys.Right, new Velocity(300, 0, 0)))
                .AddComponent(new SpaceShip(life));
            e.AddEntity(entity);
            return entity;
        }

        public List<Entity> CreateSpaceShipBullet(Gun g)
        {
            List<Entity> lst = new List<Entity>();
            if (g.doubleShoot)
            {
                Entity b1 = new Entity();
                Entity b2 = new Entity();
                b1.AddComponent(new Bullet(1, true, g.controllableShoot))
                    .AddComponent(new Position(g.shootingPoint - new Vector2D(10, 0), 0))
                    .AddComponent(new Velocity(0, -300, 0))
                    .AddComponent(new Display(Properties.Resources.shoot1));
                e.AddEntity(b1);
                lst.Add(b1);

                b2.AddComponent(new Bullet(1, true, g.controllableShoot))
                    .AddComponent(new Position(g.shootingPoint + new Vector2D(10, 0), 0))
                    .AddComponent(new Velocity(0, -300, 0))
                    .AddComponent(new Display(Properties.Resources.shoot1));
                e.AddEntity(b2);
                lst.Add(b2);

            } else
            {
                Entity entity = new Entity();
                entity.AddComponent(new Bullet(1, true, g.controllableShoot))
                    .AddComponent(new Position(g.shootingPoint, 0))
                    .AddComponent(new Velocity(0, -300, 0))
                    .AddComponent(new Display(Properties.Resources.shoot1));
                e.AddEntity(entity);
                lst.Add(entity);
            }
            
            if (g.controllableShoot)
            {
                g.controllableShoot = false; // we reset bonus when shot
            }

            return lst;
        }

        public Entity CreateEnemyBullet(Gun g, Bitmap b, int damage)
        {

            Entity entity = new Entity();
            entity.AddComponent(new Bullet(damage, false))
                .AddComponent(new Position(g.shootingPoint, 0))
                .AddComponent(new Velocity(0, 300, 0))
                .AddComponent(new Display(b));
            e.AddEntity(entity);
            return entity;
        }


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
        public List<Entity> CreateEnemyEntities(Size s, GameState gs)
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

            int max_x = 0;
            int max_y = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Entity ent = new Entity();
                    Position p = new Position((s.Width / 20) * (2*j), (s.Height / 17) * i);
                    // We check the furthest x and y pos for the enemy block
                    if (max_x < p.point.x + arr_bitmap[i].Width)
                    {
                        max_x = (int)p.point.x + arr_bitmap[i].Width;
                    }

                    if (max_y < p.point.y + arr_bitmap[i].Height)
                    {
                        max_y = (int)p.point.y + arr_bitmap[i].Height;
                    }

                    ent.AddComponent(new Display(arr_bitmap[i]))
                        .AddComponent(p)
                        .AddComponent(new Gun(p.point))
                        .AddComponent(new Enemy(1, arr_type[i], s.Width / 25, 1000));
                    entities.Add(ent);
                    e.AddEntity(ent);
                    gs.enemiesCount++;
                }
            }

            Entity eb = new Entity();
            eb.AddComponent(
                new EnemyBlock(
                    0,
                    0,
                    max_x,
                    max_y
                    )
            );
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
