﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WizardsNeverDie.Physics;
using WizardsNeverDie.Animation;
using WizardsNeverDie.Entities;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using WizardsNeverDie.Trigger;
using Microsoft.Xna.Framework.Audio;

namespace WizardsNeverDie.Level
{
    public class Level1_2 : BaseLevel
    {
        List<IAction> _actions1 = new List<IAction>();
        List<IAction> _actions2 = new List<IAction>();
        private List<Key> _blueKey = new List<Key>();
        private List<Key> _greenKey = new List<Key>();
        private List<Key> _redKey = new List<Key>();
        private Body oracleCutSceneBlock;
        private Wizard _player;
        private int _greenKeyCount = 0;
        private int _blueKeyCount = 0;
        private int _redKeyCount = 0;
        private WizardAnimation _wizard;
        private OracleAnimation _oracleAnimation;
        private List<Oracle> _oracle = new List<Oracle>();
        private HealthAnimation _healthSprite;
        private Health _health;
        private List<Brick> _bricks = new List<Brick>();
        private List<MeleeRedIfrit> _creatures = new List<MeleeRedIfrit>();
        private List<Odin> _odin = new List<Odin>();
        private List<RangedPurpleIfrit> _purpleCreatures = new List<RangedPurpleIfrit>();
        private List<Explosion> _explosions = new List<Explosion>();
        private List<SpriteAnimation> _wallSprites;
        private List<WizardPlasma> _plasma = new List<WizardPlasma>();
        private List<RangedPurplePlasma> _purplePlasma = new List<RangedPurplePlasma>();
        private List<Spawner> _spawners = new List<Spawner>();
        private List<TriggerBody> _triggers = new List<TriggerBody>();
        private List<TriggerBody> _triggers2 = new List<TriggerBody>();
        private List<Potions> _potion = new List<Potions>();
        private List<PotionExplosion> _potionExplosion = new List<PotionExplosion>();
        bool firstTime = true;
        GateAnimation greenGateAnimation;
        GateAnimation blueGateAnimation;
        GateAnimation redGateAnimation;
        Stopwatch timer = new Stopwatch();
        TimeSpan initialTime;
        TimeSpan animationTimeSpan;
        int forcePower = 500;
        Stopwatch animationTimer = new Stopwatch();
        Boolean animationFinished = true;
        private bool isGameOver = false;
        private int _creaturesKilled = 0;
        private TriggerBody _trigger;
        private List<Gate> _greenGates = new List<Gate>();
        private List<Gate> _blueGates = new List<Gate>();
        private List<Gate> _redGates = new List<Gate>();
        private static SoundEffect _soundEffect;
        private static SoundEffect _futureSound;
        PlasmaWall chatBody;
        public Level1_2()
        {
            int test = initialTime.Seconds;
            levelDetails = "Level 1";
            levelName = "Start Game: 2";
            this.backgroundTextureStr = "Materials/Level1_2";
        }
        public override void LoadContent()
        {
            base.LoadContent();
            
            _wizard = new WizardAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Wizard\\wizard"), new StreamReader(@"Content/Sprites/Wizard/wizard.txt"));
            _wizard.AnimationName = "wizard_d_walk";
            _player = new Wizard(_wizard, ConvertUnits.ToSimUnits(-(2048 / 2) + 1040, -(2048 / 2) + 70));
            _healthSprite = new HealthAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Health\\health"), new StreamReader(@"Content/Sprites/Health/health.txt"));
            _healthSprite.AnimationName = "health_n_health25";
            _health = new Health(_healthSprite, _player, _player.Position);
            GenerateWalls();
            GenereateCreatures();
            GenereateSpawners();
            GeneratePotions();
            GenerateGates();
            GenerateKeys();
            this.Camera.EnableTracking = true;
            this.Camera.TrackingBody = _player.getBody().Bodies[0];
            _soundEffect = ScreenManager.Content.Load<SoundEffect>("SoundEffects\\YEEAAAAH");
            
        }
        public void GenerateWalls()
        {
            World world = Farseer.Instance.World;
            PlasmaWall leftWall = new PlasmaWall(ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 985 + (10 / 2), -(2048 / 2) + (1079 / 2) + 0)), ConvertUnits.ToSimUnits(10), ConvertUnits.ToSimUnits(1079));
            PlasmaWall rightWall = new PlasmaWall(ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1085 + (10 / 2), -(2048 / 2) + (1079 / 2) + 0)), ConvertUnits.ToSimUnits(10), ConvertUnits.ToSimUnits(1079));
            Body circleArena = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 871 + (1), -(2048 / 2) + (1) + 1099)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 987 + (10 / 2), -(2048 / 2) + (1) + 1074))) ;
            Body circleArena2 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 871 + (1), -(2048 / 2) + (1) + 1099)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 766 + (10 / 2), -(2048 / 2) + (1) + 1145)));
            Body circleArena3 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 766 + (10 / 2), -(2048 / 2) + (1) + 1148)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 700 + (10 / 2), -(2048 / 2) + (1) + 1205)));
            Body circleArena4 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 700 + (10 / 2), -(2048 / 2) + (1) + 1205)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 670 + (10 / 2), -(2048 / 2) + (1) + 1270)));
            Body circleArena5 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 670 + (10 / 2), -(2048 / 2) + (1) + 1270)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 675 + (10 / 2), -(2048 / 2) + (1) + 1354)));
            Body circleArena6 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 675 + (10 / 2), -(2048 / 2) + (1) + 1354)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 725 + (10 / 2), -(2048 / 2) + (1) + 1417)));
            Body circleArena7 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 725 + (10 / 2), -(2048 / 2) + (1) + 1417)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 800 + (10 / 2), -(2048 / 2) + (1) + 1475)));
            Body circleArena8 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 800 + (10 / 2), -(2048 / 2) + (1) + 1475)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 922 + (10 / 2), -(2048 / 2) + (1) + 1516)));
            Body circleArena9 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 922 + (10 / 2), -(2048 / 2) + (1) + 1516)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1140 + (10 / 2), -(2048 / 2) + (1) + 1516)));
            Body circleArena10 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 922 + (10 / 2), -(2048 / 2) + (1) + 1516)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1140 + (10 / 2), -(2048 / 2) + (1) + 1516)));
            Body circleArena11 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1140 + (10 / 2), -(2048 / 2) + (1) + 1516)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1248 + (10 / 2), -(2048 / 2) + (1) + 1488)));
            Body circleArena12 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1248 + (10 / 2), -(2048 / 2) + (1) + 1488)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1337 + (10 / 2), -(2048 / 2) + (1) + 1420)));
            Body circleArena13 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1335 + (10 / 2), -(2048 / 2) + (1) + 1420)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1400 + (10 / 2), -(2048 / 2) + (1) + 1337)));
            Body circleArena14 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1400 + (10 / 2), -(2048 / 2) + (1) + 1337)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1391 + (10 / 2), -(2048 / 2) + (1) + 1245)));
            Body circleArena15 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1391 + (10 / 2), -(2048 / 2) + (1) + 1245)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1337 + (10 / 2), -(2048 / 2) + (1) + 1170)));
            Body circleArena16 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1337 + (10 / 2), -(2048 / 2) + (1) + 1170)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1237 + (10 / 2), -(2048 / 2) + (1) + 1106)));
            Body circleArena17 = BodyFactory.CreateEdge(world, ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1237 + (10 / 2), -(2048 / 2) + (1) + 1106)),
                ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1095 + (10 / 2), -(2048 / 2) + (1) + 1070)));

            MessageAction wAction = new MessageAction(ScreenManager,ScreenManager.Content.Load<Texture2D>(@"Avatar\WizardAvatarSunglasses"), "conversation5.xml");
            _actions1.Add(wAction);
            _triggers.Add(new TriggerBody(ConvertUnits.ToSimUnits(86), ConvertUnits.ToSimUnits(17), ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1030 + (24 / 2), -(2048 / 2) + (86 / 2) + 80)), 1f, _actions1));
            MessageAction oAction = new MessageAction(ScreenManager, ScreenManager.Content.Load<Texture2D>(@"Avatar\OdinAvatar"), "conversation6.xml");
            _actions2.Add(oAction);
            _triggers2.Add(new TriggerBody(ConvertUnits.ToSimUnits(86), ConvertUnits.ToSimUnits(17), ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1030 + (24 / 2), -(2048 / 2) + (86 / 2) + 1050)), 1f, _actions2));
            chatBody = new PlasmaWall(ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 1030 + (24 / 2), -(2048 / 2) + (86 / 2) + 1050)), ConvertUnits.ToSimUnits(86), ConvertUnits.ToSimUnits(17));
        }


        public void GenereateCreatures()
        {
            //RangedPurpleIfritAnimation _purpleCreatureAnimation = new RangedPurpleIfritAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\PurpleIfrit\\ifrit"),
            //    new StreamReader(@"Content/Sprites/PurpleIfrit/ifrit.txt"));
            //_purpleCreatureAnimation.AnimationName = "ifrit_d_walk";
            //_purpleCreatures.Add(new RangedPurpleIfrit(_purpleCreatureAnimation, _player, ConvertUnits.ToSimUnits(-(2048 / 2) + 1240, -(2048 / 2) + 587), 1.5f, 1.5f, 11F, 10f));

            //RangedPurpleIfritAnimation _purpleCreatureAnimation2 = new RangedPurpleIfritAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\PurpleIfrit\\ifrit"),
            //    new StreamReader(@"Content/Sprites/PurpleIfrit/ifrit.txt"));
            //_purpleCreatureAnimation2.AnimationName = "ifrit_d_walk";
            //_purpleCreatures.Add(new RangedPurpleIfrit(_purpleCreatureAnimation2, _player, ConvertUnits.ToSimUnits(-(2048 / 2) + 872, -(2048 / 2) + 362), 1.5f, 1.5f, 11F, 10f));
            
            OdinAnimation odinAnimation = new OdinAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Odin\\odin"), new StreamReader(@"Content/Sprites/Odin/odin.txt"));
            odinAnimation.AnimationName = "odin_u_walk";
            _odin.Add(new Odin(odinAnimation, _player, _player.Position + new Vector2(0, 55), 3f, 3f, 12f));
        }
        public void GenereateSpawners()
        {
            ;
        }
        public void GeneratePotions()
        {
            ;
        }
        public void GenerateKeys()
        {

        }
        public void GenerateGates()
        {//ConvertUnits.ToSimUnits(new Vector2(-(2048 / 2) + 968 + (16 / 2), -(2048 / 2) + (227 / 2) + 86)))

        }
        public void Spell(Wizard player, int forcePower)
        {
            Vector2 plasmaPosition = plasmaPosition = new Vector2(0, _player.Position.Y + 2);
            WizardPlasmaAnimation plasmaSprite = new WizardPlasmaAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Plasma\\plasma"), new StreamReader(@"Content/Sprites/Plasma/plasma.txt"));
            plasmaSprite.AnimationName = "plasma_d_attack";
            SpriteAnimation animation = (SpriteAnimation)player.SpriteManager;
            Vector2 force = new Vector2();
            if (animation.GetOrientation() == Orientation.Down)
            {
                force = new Vector2(0, forcePower);
                plasmaPosition = new Vector2(_player.Position.X, _player.Position.Y + 2);
            }
            else if (animation.GetOrientation() == Orientation.DownLeft)
            {
                force = new Vector2(-forcePower, forcePower);
                plasmaPosition = new Vector2(_player.Position.X - 1, _player.Position.Y + 1);
            }
            else if (animation.GetOrientation() == Orientation.DownRight)
            {
                force = new Vector2(forcePower, forcePower);
                plasmaPosition = new Vector2(_player.Position.X + 1, _player.Position.Y + 1);
            }
            else if (animation.GetOrientation() == Orientation.Left)
            {
                force = new Vector2(-forcePower, 0);
                plasmaPosition = new Vector2(_player.Position.X - 2, _player.Position.Y);
            }
            else if (animation.GetOrientation() == Orientation.Right)
            {
                force = new Vector2(forcePower, 0);
                plasmaPosition = new Vector2(_player.Position.X + 2, _player.Position.Y);
            }
            else if (animation.GetOrientation() == Orientation.Up)
            {
                plasmaPosition = new Vector2(_player.Position.X, _player.Position.Y - 2);
                force = new Vector2(0, -forcePower);
            }
            else if (animation.GetOrientation() == Orientation.UpLeft)
            {
                force = new Vector2(-forcePower, -forcePower);
                plasmaPosition = new Vector2(_player.Position.X - 1, _player.Position.Y - 1);
            }
            else if (animation.GetOrientation() == Orientation.UpRight)
            {
                force = new Vector2(forcePower, -forcePower);
                plasmaPosition = new Vector2(_player.Position.X + 1, _player.Position.Y - 1);
            }
            _plasma.Add(new WizardPlasma(plasmaSprite, plasmaPosition, force));
        }

        public void PurpleSpell(RangedPurpleIfrit enemy, int forcePower)
        {
            forcePower = 500;
            Vector2 plasmaPosition = plasmaPosition = new Vector2(0, _player.Position.Y + 2);
            WizardPlasmaAnimation plasmaSprite = new WizardPlasmaAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Plasma\\plasma"), new StreamReader(@"Content/Sprites/Plasma/plasma.txt"));
            plasmaSprite.AnimationName = "plasma_d_attack";
            SpriteAnimation animation = (SpriteAnimation)enemy.SpriteManager;
            Vector2 force = new Vector2();
            //for (int i = 0; i < _purpleCreatures.Count; i++)
            //{
            if (animation.GetOrientation() == Orientation.Down)
            {
                force = new Vector2(0, forcePower);
                plasmaPosition = new Vector2(enemy.Position.X, enemy.Position.Y + 2);
            }
            else if (animation.GetOrientation() == Orientation.DownLeft)
            {
                force = new Vector2(-forcePower, forcePower);
                plasmaPosition = new Vector2(enemy.Position.X - 1, enemy.Position.Y + 1);
            }
            else if (animation.GetOrientation() == Orientation.DownRight)
            {
                force = new Vector2(forcePower, forcePower);
                plasmaPosition = new Vector2(enemy.Position.X + 1, enemy.Position.Y + 1);
            }
            else if (animation.GetOrientation() == Orientation.Left)
            {
                force = new Vector2(-forcePower, 0);
                plasmaPosition = new Vector2(enemy.Position.X - 2, enemy.Position.Y);
            }
            else if (animation.GetOrientation() == Orientation.Right)
            {
                force = new Vector2(forcePower, 0);
                plasmaPosition = new Vector2(enemy.Position.X + 2, enemy.Position.Y);
            }
            else if (animation.GetOrientation() == Orientation.Up)
            {
                plasmaPosition = new Vector2(enemy.Position.X, enemy.Position.Y - 2);
                force = new Vector2(0, -forcePower);
            }
            else if (animation.GetOrientation() == Orientation.UpLeft)
            {
                force = new Vector2(-forcePower, -forcePower);
                plasmaPosition = new Vector2(enemy.Position.X - 1, enemy.Position.Y - 1);
            }
            else if (animation.GetOrientation() == Orientation.UpRight)
            {
                force = new Vector2(forcePower, -forcePower);
                plasmaPosition = new Vector2(enemy.Position.X + 1, enemy.Position.Y - 1);
            }
            _purplePlasma.Add(new RangedPurplePlasma(plasmaSprite, plasmaPosition, force));
        }


        //}

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            WizardAnimation wizard = (WizardAnimation)_player.SpriteManager;
            lastGamepadState = gamepadState;
            gamepadState = GamePad.GetState(PlayerIndex.One);
            ExplosionAnimation explosionSprite = new ExplosionAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Explosion\\explosion"), new StreamReader(@"Content/Sprites/Explosion/explosion.txt"), 12f);
            ExplosionAnimation metalSlugExplosionSprite =
                new ExplosionAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Explosion\\metalslugexplosion"), new StreamReader(@"Content/Sprites/Explosion/metalslugexplosion.txt"), 12f);

            if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || gamepadState.Buttons.X == ButtonState.Pressed)
            {

                if (!timer.IsRunning)
                {
                    timer.Start();
                    initialTime = timer.Elapsed;
                }
            }
            if (!isGameOver)
            {
                _player.Update(gameTime);
                _health.Update(gameTime);
            }
            if (_player.Health == HealthAnimation.HealthState.Health0 && !isGameOver && !wizard.CheckEndRevive)
            {
                wizard.SetAnimationState(AnimationState.Revived);
                _player.getBody().Bodies[0].Position = ConvertUnits.ToSimUnits(-(2048 / 2) + 1040, -(2048 / 2) + 70);
                _player.Update(gameTime);
                
            }
            if (wizard.CheckEndRevive)
            {
                _player.Health = HealthAnimation.HealthState.Health100;
                _health.Update(gameTime);
                wizard.SetAnimationState(AnimationState.Walk);
                _player.Update(gameTime);  
            }
            for (int i = 0; i < _triggers.Count; i++)
            {
                if (_triggers[i].IsDead == true)
                {
                    for (int j = 0; j < _triggers[i].ActionList.Count; j++)
                    {
                        if (_triggers[i].ActionList[j].IsDead == false)
                            _triggers[i].ActionList[j].Update(gameTime);
                    }
                }
            }

            for (int i = 0; i < _triggers2.Count; i++)
            {
                if (_triggers2[i].IsDead == true)
                {
                    for (int j = 0; j < _triggers2[i].ActionList.Count; j++)
                    {
                        if (_triggers2[i].ActionList[j].IsDead == false)
                            _triggers2[i].ActionList[j].Update(gameTime);
                    }
                }
            }
            for (int i = 0; i < _greenGates.Count; i++)
            {
                if (_greenGates[i].Unlock && _greenKeyCount > 0)
                {
                    greenGateAnimation.SetAnimationState(AnimationState.Open);
                    greenGateAnimation.Update(gameTime);
                    if (_greenGates[i].GateCollideFirstTime)
                    {
                        Farseer.Instance.World.RemoveBody(_greenGates[i].getBody().Bodies[0]);
                        _greenGates[i].GateCollideFirstTime = false;
                        _greenKeyCount--;
                    }
                }
                else
                {
                    _greenGates[i].Update(gameTime);
                }

            }

            for (int i = 0; i < _blueGates.Count; i++)
            {
                if (_blueGates[i].Unlock && _blueKeyCount > 0)
                {
                    blueGateAnimation.SetAnimationState(AnimationState.Open);
                    blueGateAnimation.Update(gameTime);
                    if (_blueGates[i].GateCollideFirstTime)
                    {
                        Farseer.Instance.World.RemoveBody(_blueGates[i].getBody().Bodies[0]);
                        _blueGates[i].GateCollideFirstTime = false;
                        _blueKeyCount--;
                    }
                }
                else
                {
                    _blueGates[i].Update(gameTime);
                }
            }

            for (int i = 0; i < _redGates.Count; i++)
            {
                if (_redGates[i].Unlock && _redKeyCount > 0)
                {

                    redGateAnimation.SetAnimationState(AnimationState.Open);
                    redGateAnimation.Update(gameTime);
                    if (_redGates[i].GateCollideFirstTime)
                    {
                        Farseer.Instance.World.RemoveBody(_redGates[i].getBody().Bodies[0]);
                        _redGates[i].GateCollideFirstTime = false;
                        _redKeyCount--;
                    }
                }
                else
                {
                    _redGates[i].Update(gameTime);
                }
            }

            for (int i = 0; i < _creatures.Count; i++)//(Enemy e in _creatures)
            {
                MeleeRedIfritAnimation enemy = (MeleeRedIfritAnimation)_creatures[i].SpriteManager;
                AnimationState s = enemy.GetAnimationState();
                if (s == AnimationState.Disentegrated && enemy.PreviousAnimationState == AnimationState.Death)
                {
                    explosionSprite.AnimationName = "ifrit_d_explosion";
                    _explosions.Add(new Explosion(explosionSprite, _creatures[i].Position + new Vector2(0, -1), .01f, .01f));
                    Farseer.Instance.World.RemoveBody(_creatures[i].getBody().Bodies[0]);
                    _creatures.Remove(_creatures[i]);
                    _creaturesKilled = _creaturesKilled++;
                }
                else
                {
                    _creatures[i].Update(gameTime);
                }
            }

            for (int i = 0; i < _odin.Count; i++)//(Enemy e in _creatures)
            {
                
                    _odin[i].Update(gameTime);
                
            }

            for (int i = 0; i < _purpleCreatures.Count; i++)//(Enemy e in _creatures)
            {
                RangedPurpleIfritAnimation enemy = (RangedPurpleIfritAnimation)_purpleCreatures[i].SpriteManager;
                AnimationState s = enemy.GetAnimationState();
                if (s == AnimationState.Disentegrated && enemy.PreviousAnimationState == AnimationState.Death)
                {
                    explosionSprite.AnimationName = "ifrit_d_explosion";
                    _explosions.Add(new Explosion(explosionSprite, _purpleCreatures[i].Position + new Vector2(0, -1), .01f, .01f));
                    Farseer.Instance.World.RemoveBody(_purpleCreatures[i].getBody().Bodies[0]);
                    _purpleCreatures.Remove(_purpleCreatures[i]);
                    _creaturesKilled = _creaturesKilled++;

                }
                else if (enemy.CheckEndSpell == true)
                {
                    PurpleSpell(_purpleCreatures[i], 300);
                    _purpleCreatures[i].Update(gameTime);
                }
                else
                {
                    _purpleCreatures[i].Update(gameTime);
                }

            }
            for (int i = 0; i < _explosions.Count; i++)
            {
                ExplosionAnimation explosion = (ExplosionAnimation)_explosions[i].SpriteManager;
                if (explosion.IsDead)
                {
                    _explosions.Remove(_explosions[i]);
                }
                else
                {
                    _explosions[i].Update(gameTime);
                }
            }



            //SpawnerAnimation _spawnerAnimation = new SpawnerAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Spawner\\portal"), new StreamReader(@"Content/Sprites/Spawner/portal.txt"));
            //_spawnerAnimation.AnimationName = "spawner_d_spawning";
            //_spawner.Add(new Spawner(_spawnerAnimation, new Vector2(0,0)));
            //if (keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            if (keyboardState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.X) && lastKeyBoardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X))
            {
                _player.Health = HealthAnimation.HealthState.Health0;
            }

            if (wizard.CheckEndSpell == true)
            {

                timer.Stop();
                TimeSpan endTime = timer.Elapsed;
                int totalTime = endTime.Seconds - initialTime.Seconds;
                timer.Reset();

                initialTime = timer.Elapsed;
                if (totalTime < 1)
                {
                    forcePower = 500;
                }
                else if (totalTime >= 1 && totalTime < 2)
                {
                    forcePower = 1000;
                }
                else if (totalTime >= 2 && totalTime < 4)
                {
                    forcePower = 2000;
                }
                else if (totalTime >= 4)
                {
                    forcePower = 3000;
                }
                AnimationState s = wizard.GetAnimationState();
                if (!isGameOver)
                {
                    Spell(_player, forcePower);
                }
            }


            if (keyboardState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Q) && lastKeyBoardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q) && potionCount > 0)
            {
                //PotionExplosion(_player);
                //_potionExplosion.Add(new PotionExplosion(potionExplosionAnimation, _player, _creatures, _player.Position));
                //potionCount--;
            }
            for (int i = 0; i < _potionExplosion.Count; i++)
            {
                PotionExplosionAnimation potionExplosionAnim = (PotionExplosionAnimation)_potionExplosion[i].SpriteManager;
                if (potionExplosionAnim.IsDead)
                {
                    _potionExplosion.Remove(_potionExplosion[i]);
                }
                else
                {
                    _potionExplosion[i].Update(gameTime);
                }
            }

            for (int i = 0; i < _plasma.Count; i++)//(Plasma p in _plasma)
            {
                if (_plasma[i].IsDead)
                {
                    explosionSprite.AnimationName = "ifrit_d_explosion";
                    _explosions.Add(new Explosion(explosionSprite, _plasma[i].Position + new Vector2(0, -1), .01f, .01f));
                    Farseer.Instance.World.RemoveBody(_plasma[i].getBody().Bodies[0]);
                    _plasma.Remove(_plasma[i]);
                }
                else if (_plasma[i].IsDeadOnEnemy)
                {
                    Farseer.Instance.World.RemoveBody(_plasma[i].getBody().Bodies[0]);
                    _plasma.Remove(_plasma[i]);
                }
                else
                    _plasma[i].Update(gameTime);
            }
            for (int i = 0; i < _purplePlasma.Count; i++)//(Plasma p in _plasma)
            {
                if (_purplePlasma[i].IsDead)
                {
                    explosionSprite.AnimationName = "ifrit_d_explosion";
                    _explosions.Add(new Explosion(explosionSprite, _purplePlasma[i].Position + new Vector2(0, -1), .01f, .01f));
                    Farseer.Instance.World.RemoveBody(_purplePlasma[i].getBody().Bodies[0]);
                    _purplePlasma.Remove(_purplePlasma[i]);
                }
                else if (_purplePlasma[i].IsDeadOnEnemy)
                {
                    Farseer.Instance.World.RemoveBody(_purplePlasma[i].getBody().Bodies[0]);
                    _purplePlasma.Remove(_purplePlasma[i]);
                }
                else
                    _purplePlasma[i].Update(gameTime);
            }
            for (int i = 0; i < _spawners.Count; i++)
            {
                if (_spawners[i].IsDead)
                {
                    metalSlugExplosionSprite.ExplodeOnSpawner = true;
                    metalSlugExplosionSprite.AnimationName = "metalslug_d_explosion";
                    _explosions.Add(new Explosion(metalSlugExplosionSprite, _spawners[i].Position + new Vector2(-2, -5), .01f, .01f));
                    _explosions[_explosions.Count - 1].SpriteManager.Animations[metalSlugExplosionSprite.AnimationName].Scale = 2f;
                    _explosions[_explosions.Count - 1].Update(gameTime);
                    Farseer.Instance.World.RemoveBody(_spawners[i].getBody().Bodies[0]);
                    _spawners.Remove(_spawners[i]);
                }
                else
                {
                    _spawners[i].Update(gameTime);
                }
            }

            for (int i = 0; i < _triggers.Count; i++)
            {
                bool removeTrigger = true;
                for (int j = 0; j < _triggers[i].ActionList.Count; j++)
                {
                    if (_triggers[i].ActionList[j].IsDead == false)
                    {
                        removeTrigger = false;
                    }
                }
                if (removeTrigger)
                {
                    Farseer.Instance.World.RemoveBody(_triggers[i].Bodies[0]);
                    _triggers.Remove(_triggers[i]);

                    _soundEffect.Play();
                    removeTrigger = false;
                }
            }

            for (int i = 0; i < _triggers2.Count; i++)
            {
                bool removeTrigger = true;
                for (int j = 0; j < _triggers2[i].ActionList.Count; j++)
                {
                    if (_triggers2[i].ActionList[j].IsDead == false)
                    {
                        removeTrigger = false;
                    }
                }
                if (removeTrigger)
                {
                    Farseer.Instance.World.RemoveBody(_triggers2[i].Bodies[0]);
                    _triggers2.Remove(_triggers2[i]);
                    Farseer.Instance.World.RemoveBody(chatBody.getBody().Bodies[0]);
                    
                }
            }

            for (int i = 0; i < _potion.Count; i++)
            {
                if (_potion[i].IsCollected == true)
                {
                    potionCount++;
                    Farseer.Instance.World.RemoveBody(_potion[i].getBody().Bodies[0]);
                    _potion.Remove(_potion[i]);
                }
                else
                {
                    _potion[i].Update(gameTime);
                }
            }

            for (int i = 0; i < _blueKey.Count; i++)
            {
                if (_blueKey[i].IsCollected == true)
                {
                    _blueKeyCount++;
                    Farseer.Instance.World.RemoveBody(_blueKey[i].getBody().Bodies[0]);
                    _blueKey.Remove(_blueKey[i]);
                }
                else
                {
                    _blueKey[i].Update(gameTime);
                }
            }
            for (int i = 0; i < _greenKey.Count; i++)
            {
                if (_greenKey[i].IsCollected == true)
                {
                    _greenKeyCount++;
                    Farseer.Instance.World.RemoveBody(_greenKey[i].getBody().Bodies[0]);
                    _greenKey.Remove(_greenKey[i]);
                }
                else
                {
                    _greenKey[i].Update(gameTime);
                }
            }
            for (int i = 0; i < _redKey.Count; i++)
            {
                if (_redKey[i].IsCollected == true)
                {
                    _redKeyCount++;
                    Farseer.Instance.World.RemoveBody(_redKey[i].getBody().Bodies[0]);
                    _redKey.Remove(_redKey[i]);
                }
                else
                {
                    _redKey[i].Update(gameTime);
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null, Camera.View);
            //Draw stuff in here
            foreach (Gate g in _greenGates)
            {
                g.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Gate b in _blueGates)
            {
                b.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Gate r in _redGates)
            {
                r.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Brick b in _bricks)
            {
                b.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Potions s in _potion)
            {
                s.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Spawner s in _spawners)
            {
                s.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (MeleeRedIfrit e in _creatures)
            {
                e.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (RangedPurpleIfrit e in _purpleCreatures)
            {
                e.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (WizardPlasma p in _plasma)
            {
                p.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (RangedPurplePlasma p in _purplePlasma)
            {
                p.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (PotionExplosion pE in _potionExplosion)
            {
                pE.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Oracle o in _oracle)
            {
                o.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            if (!isGameOver)
            {
                _player.SpriteManager.Draw(ScreenManager.SpriteBatch);
                _health.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Key k in _blueKey)
            {
                k.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Key k in _greenKey)
            {
                k.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Key k in _redKey)
            {
                k.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Explosion e in _explosions)
            {
                e.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }
            foreach (Odin o in _odin)
            {
                o.SpriteManager.Draw(ScreenManager.SpriteBatch);
            }


            //if (isGameOver)
            //{
            //    ScreenManager.SpriteBatch.Draw(_gameover, _gameOverVector, Color.White);
            //}


            //ScreenManager.SpriteBatch.Draw(_gameover, new Rectangle((int)_player.Position.X, (int)_player.Position.X, 400, 266), Color.White);
            ScreenManager.SpriteBatch.End();
            ScreenManager.SpriteBatch.Begin();

            for (int i = 0; i < _triggers.Count; i++)
            {
                if (_triggers[i].IsDead == true)
                {
                    for (int j = 0; j < _triggers[i].ActionList.Count; j++)
                    {
                        _triggers[i].ActionList[j].Draw();
                    }
                }
            }
            for (int i = 0; i < _triggers2.Count; i++)
            {
                if (_triggers2[i].IsDead == true)
                {
                    for (int j = 0; j < _triggers2[i].ActionList.Count; j++)
                    {
                        _triggers2[i].ActionList[j].Draw();
                    }
                }
            }
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}