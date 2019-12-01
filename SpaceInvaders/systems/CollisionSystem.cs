using ECSharp.core;
using SpaceInvaders.components;
using SpaceInvaders.nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders.systems
{
    class CollisionSystem : Systeme
    {

        #region fields
        private readonly Node bunkernode = new BunkerCollisionNode();
        private readonly Node bulletnode = new BulletCollisionNode();
        private readonly Node enemynode = new EnemyNode();
        private readonly Node spaceshipnode = new SpaceShipCollisionNode();
        private readonly Node gunnode = new GunControlNode();
        private readonly Node gamenode = new GameStateNode();
        private readonly Node bonusnode = new BonusCollisionNode();
        private readonly Node blocknode = new EnemyBlockNode();
        //private readonly Node blocknode = new EnemyBlock();


        private LinkedList<Node> lst_bunker;
        private LinkedList<Node> lst_bullet;
        private LinkedList<Node> lst_gun;
        private LinkedList<Node> lst_spaceship;
        private LinkedList<Node> lst_enemy;
        private LinkedList<Node> lst_game;
        private LinkedList<Node> lst_bonus;
        private LinkedList<Node> lst_block;

        private EntityFactory ef;
        private Size size;
        private int pixel_destroyed = 0;
        string bonus_label = "";
        int compteur_label;

        private System.Media.SoundPlayer bunk_expl = new System.Media.SoundPlayer(Properties.Resources.bunker_col);
        private System.Media.SoundPlayer bonus_sound = new System.Media.SoundPlayer(Properties.Resources.bonus_sound);
        #endregion

        #region method and constructor
        public CollisionSystem(EntityFactory ef, Size s) : base()
        {
            bunkernode.SetUp();
            bulletnode.SetUp();
            spaceshipnode.SetUp();
            enemynode.SetUp();
            gunnode.SetUp();
            gamenode.SetUp();
            bonusnode.SetUp();
            blocknode.SetUp();
            size = s;
            this.ef = ef;
        }

        public override void AddIntoEngine(Engine e)
        {
            lst_bullet = e.GetNodeList(bulletnode);
            lst_bunker = e.GetNodeList(bunkernode);
            lst_gun = e.GetNodeList(gunnode);
            lst_spaceship = e.GetNodeList(spaceshipnode);
            lst_enemy = e.GetNodeList(enemynode);
            lst_game = e.GetNodeList(gamenode);
            lst_bonus = e.GetNodeList(bonusnode);
            lst_block = e.GetNodeList(blocknode);
        }

        public override void RemoveFromEngine(Engine e)
        {
            lst_bunker = null;
            lst_bullet = null;
            lst_gun = null;
            lst_spaceship = null;
            lst_enemy = null;
            lst_bonus = null;
            lst_game = null;
            lst_block = null;
        }

        #endregion

        public override void update(float time, Graphics g)
        {
            if (runnable)
            {
                // We set up the gunnode to allow it to shoot again
                GunControlNode gunctrlnode = null;
                GameStateNode gstatenode = null;
                SpaceShipCollisionNode sscn = null;
                EnemyBlockNode eblocknode = null;


                if (lst_gun.Count > 0) { gunctrlnode = (GunControlNode)lst_gun.First(); }
                if (lst_game.Count > 0) { gstatenode = (GameStateNode)lst_game.First(); }
                if (lst_block.Count > 0) { eblocknode = (EnemyBlockNode)lst_block.First(); }

                #region bullet bunker collision
                foreach (Node bullet in lst_bullet.ToList()) // to list to remove entity while looping
                {
                    foreach (Node bunker in lst_bunker.ToList())
                    {
                        BulletCollisionNode bullcn = (BulletCollisionNode)bullet;
                        BunkerCollisionNode bunkcn = (BunkerCollisionNode)bunker;



                        if (bunkcn.bunker.life > 0 &&
                            CheckRectangleCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos) &&
                            CheckPixelCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos)
                            )
                        {
                            CheckPixelCollision(bullcn.display, bullcn.pos, bunkcn.display, bunkcn.pos, bullcn.bullet.damage);
                            ef.removeEntity(bullcn.entity);
                            bunkcn.bunker.life -= pixel_destroyed;
                            pixel_destroyed = 0;
                            if (gunctrlnode != null) { gunctrlnode.gun.shoot = true; }
                        }
                    }
                }
                #endregion

                #region enemy bullet collision
                gstatenode.gs.enemiesCount = lst_enemy.Count;
                foreach (Node bullet in lst_bullet.ToList()) // to list to remove entity while looping
                {
                    foreach (Node enemy in lst_enemy.ToList())
                    {
                        BulletCollisionNode bullcn = (BulletCollisionNode)bullet;
                        EnemyNode en = (EnemyNode)enemy;



                        if (en.enemy.life > 0 &&
                            bullcn.bullet.ally &&
                            CheckRectangleCollision(bullcn.display, bullcn.pos, en.display, en.pos) &&
                            CheckPixelCollision(bullcn.display, bullcn.pos, en.display, en.pos)
                            )
                        {
                            ef.removeEntity(bullcn.entity);
                            en.enemy.life -= 1;
                            if (en.enemy.life <= 0)
                            {
                                if (gstatenode != null)
                                {
                                    switch (en.enemy.type)
                                    {
                                        case Enemy.Type.Big:
                                            gstatenode.gs.score += 500;
                                            break;
                                        case Enemy.Type.Medium:
                                            gstatenode.gs.score += 300;
                                            break;
                                        case Enemy.Type.Small:
                                            gstatenode.gs.score += 100;
                                            break;
                                    }
                                }
                                ef.removeEntity(en.entity);
                            }
                            pixel_destroyed = 0;
                            if (gunctrlnode != null) { gunctrlnode.gun.shoot = true; }

                            if (Game.rand.Next(20) == 1) // Drop a bonus
                            {
                                Array values = Enum.GetValues(typeof(Bonus.Type));
                                ef.CreateBonus(en.pos, (Bonus.Type)values.GetValue(Game.rand.Next(values.Length)));
                            }
                        }
                    }
                }
                #endregion

                #region spaceship bullet collision
                if (lst_spaceship.Count > 0)
                {
                    sscn = (SpaceShipCollisionNode)lst_spaceship.First();
                    foreach (Node bullet in lst_bullet.ToList())
                    {
                        BulletCollisionNode bullcn = (BulletCollisionNode)bullet;



                        if (sscn.spaceship.life > 0 &&
                            !bullcn.bullet.ally &&
                            CheckRectangleCollision(bullcn.display, bullcn.pos, sscn.display, sscn.pos) &&
                            CheckPixelCollision(bullcn.display, bullcn.pos, sscn.display, sscn.pos)
                            )
                        {
                            ef.removeEntity(bullcn.entity);
                            sscn.spaceship.life -= bullcn.bullet.damage;
                            gstatenode.gs.lives = sscn.spaceship.life;
                            pixel_destroyed = 0;
                        }

                    }
                }
                #endregion

                #region bullet bullet collision
                foreach (Node bullet in lst_bullet.ToList()) // to list to remove entity while looping
                {
                    foreach (Node bull in lst_bullet.ToList())
                    {
                        BulletCollisionNode bullcn = (BulletCollisionNode)bullet;
                        BulletCollisionNode en = (BulletCollisionNode)bull;
                        if (bullcn.entity.Name != en.entity.Name)
                        {


                            if (
                                CheckRectangleCollision(bullcn.display, bullcn.pos, en.display, en.pos) &&
                                CheckPixelCollision(bullcn.display, bullcn.pos, en.display, en.pos)
                                )
                            {
                                ef.removeEntity(bullcn.entity);
                                ef.removeEntity(en.entity);
                                if (gunctrlnode != null) { gunctrlnode.gun.shoot = true; }
                            }
                        }
                    }
                }
                #endregion

                #region spaceship bonus collision
                if (lst_spaceship.Count > 0)
                {

                    sscn = (SpaceShipCollisionNode)lst_spaceship.First();
                    foreach (Node bonus in lst_bonus.ToList())
                    {
                        BonusCollisionNode bonusn = (BonusCollisionNode)bonus;



                        if (sscn.spaceship.life > 0 &&
                            CheckRectangleCollision(bonusn.display, bonusn.pos, sscn.display, sscn.pos) &&
                            CheckPixelCollision(bonusn.display, bonusn.pos, sscn.display, sscn.pos)
                            )
                        {

                            switch (bonusn.bonus.type)
                            {

                                case Bonus.Type.PlusLife:
                                    sscn.spaceship.life++;
                                    bonus_label = "+ 1 life";
                                    break;
                                case Bonus.Type.ScoreBoost:
                                    gstatenode.gs.score *= 2;
                                    bonus_label = "Score doubled";
                                    break;
                                case Bonus.Type.RestoreBunker:
                                    foreach (Node bunker in lst_bunker.ToList())
                                    {
                                        ef.removeEntity(bunker.entity);
                                    }
                                    ef.CreateBunkerEntities(size);
                                    bonus_label = "Bunker restored";
                                    break;
                                case Bonus.Type.EnemySlow:
                                    foreach (Node enemy in lst_enemy.ToList())
                                    {
                                        EnemyNode enem = (EnemyNode)enemy;
                                        enem.enemy.vitesse.x /= 5;
                                    }
                                    bonus_label = "Enemies slowdown";
                                    break;
                                case Bonus.Type.doubleShoot:
                                    Gun gun = (Gun)sscn.entity.GetComponent("Gun");
                                    gun.doubleShoot = true;
                                    bonus_label = "Double bullet";
                                    break;
                                case Bonus.Type.controlledBullet:
                                    Gun pisto = (Gun)sscn.entity.GetComponent("Gun");
                                    pisto.controllableShoot = true;
                                    bonus_label = "Controlled Bullet";
                                    break;
                            }
                            Console.WriteLine(bonus_label);
                            ef.removeEntity(bonusn.entity);
                            bonus_sound.Play();
                            gstatenode.gs.lives = sscn.spaceship.life;
                            pixel_destroyed = 0;
                        }
                    }

                    if (g != null && compteur_label++ < 1000)
                    {
                        g.DrawString(
                            bonus_label,
                            Game.font,
                            Game.blackBrush,
                            size.Width - 130,
                            size.Height - 50
                        );
                    }
                    else if (g != null && compteur_label++ > 200)
                    {
                        compteur_label = 0;
                        bonus_label = "";
                    }
                }
                #endregion


                #region enemyblock collision
                if (eblocknode.block.direction == EnemyBlock.Direction.Left && eblocknode.block.upperLeft.x <= 0)
                {
                    eblocknode.block.collided = true;
                    eblocknode.block.direction = EnemyBlock.Direction.Right;
                    eblocknode.block.shootProbability -= 10;
                }
                else if (eblocknode.block.direction == EnemyBlock.Direction.Right && eblocknode.block.bottomRight.x >= size.Width)
                {
                    eblocknode.block.collided = true;
                    eblocknode.block.direction = EnemyBlock.Direction.Left;
                    eblocknode.block.shootProbability -= 10;
                }

                if (lst_spaceship.Count > 0)
                {
                    sscn = (SpaceShipCollisionNode)lst_spaceship.First();
                    // we check the collision we the spaceship and then set the game to lose if collision occured
                    if (
                        sscn.pos.point.x + sscn.display.bitmap.Width >= eblocknode.block.upperLeft.x &&
                        sscn.pos.point.x <= eblocknode.block.bottomRight.x &&
                        sscn.pos.point.y + sscn.display.bitmap.Height >= eblocknode.block.upperLeft.y &&
                        sscn.pos.point.y <= eblocknode.block.bottomRight.y
                        )
                    {
                        gstatenode.gs.state = Game.State.Lost;
                    }

                }
                #endregion
            }

        }


        #region collision methods
        private bool CheckRectangleCollision(Display d1, Position p1, Display d2, Position p2)
        {
            if (p1.point.x + d1.bitmap.Width >= p2.point.x &&
                p1.point.x <= p2.point.x + d2.bitmap.Width &&
                p1.point.y + d1.bitmap.Height >= p2.point.y &&
                p1.point.y <= p2.point.y + d2.bitmap.Height)
            {
                return true;
            }
            return false;
        }

        private bool CheckPixelCollision(Display d1, Position p1, Display d2, Position p2, int damage = 0)
        {
            float x1 = Math.Max(p1.point.x, p2.point.x);
            float x2 = Math.Min(p1.point.x + d1.bitmap.Width, p2.point.x + d2.bitmap.Width);

            float y1 = Math.Max(p1.point.y, p2.point.y);
            float y2 = Math.Min(p1.point.y + d1.bitmap.Height, p2.point.y + d2.bitmap.Height);

            // For each single pixel in the intersecting rectangle
            for (int y = (int)y1; y < y2; ++y)
            {
                for (int x = (int)x1; x < x2; ++x)
                {
                    if (d1.bitmap.GetPixel((int)(x - p1.point.x), (int)(y - p1.point.y)) == Color.FromArgb(255, 0, 0, 0) &&
                        d1.bitmap.GetPixel((int)(x - p1.point.x), (int)(y - p1.point.y)) ==
                        d2.bitmap.GetPixel((int)(x - p2.point.x), (int)(y - p2.point.y)))
                    {
                        if (damage > 0) { DestroyPixel(d2, (int)(x - p2.point.x), (int)(y - p2.point.y), damage); }
                        bunk_expl.Play();
                        return true;
                    }
                }
            }
            return false;
        }

        private void DestroyPixel(Display d, int x, int y, int damage)
        {
            d.bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
            for (int i = -damage * 4; i < damage * 4; i++)
            {
                for (int j = -damage * 4; j < damage * 4; j++)
                {
                    if (Math.Abs(i) + Math.Abs(j) < 4)
                    {
                        if (x + i >= 0 && x + i < d.bitmap.Width && y + j >= 0 && y + j < d.bitmap.Height)
                        {
                            d.bitmap.SetPixel(x + i, y + j, Color.FromArgb(0, 255, 255, 255));
                            pixel_destroyed++;
                        }

                    }

                }
            }

        }
        #endregion
    }
}
