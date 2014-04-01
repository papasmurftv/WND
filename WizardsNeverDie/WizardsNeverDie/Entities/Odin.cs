﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.DebugViews;
using WizardsNeverDie.Physics;
using WizardsNeverDie.Level;
using WizardsNeverDie.Animation;
using WizardsNeverDie.Intelligence;
using WizardsNeverDie.Entities;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace WizardsNeverDie.Entities
{
    class Odin : AbstractCreature
    {
        private bool _isDead = false;
        
        public Odin(OdinAnimation spriteManager, AbstractCreature target, Vector2 position, float width, float height, float targetDistance)
        {
            this.spriteManager = spriteManager;
            this.body = new BasicBody(this, position, 1f);
            this.intelligence = new OdinIntelligence(this, target, .25f, targetDistance);
        }
        public void Update(GameTime gameTime)
        {
            intelligence.Update(gameTime);
            spriteManager.Position = body.Position;
            spriteManager.Update(gameTime);
        }
        public AbstractCreature Target
        {
            get
            {
                return ((OdinIntelligence)intelligence).target;
            }
            set
            {
                ((OdinIntelligence)intelligence).target = value;
            }
        }
        public override bool WillCollide(AbstractEntity collidedWith)
        {

            OdinAnimation animation = (OdinAnimation)this.SpriteManager;
            if (collidedWith is Wizard && animation.GetAnimationState() != AnimationState.Death)
            {
                Wizard player = (Wizard)collidedWith;
                animation.SetAnimationState(AnimationState.Attack);
                //if (animation.PreviousAnimationState = AnimationState.Walk && animation.GetAnimationState() == AnimationState.Walk)
                //{
                    if (player.Health == HealthAnimation.HealthState.Health100)
                        player.Health = HealthAnimation.HealthState.Health0;
                //}
            }
            if(collidedWith is WizardPlasma)
            {
                WizardPlasma plasma = (WizardPlasma)collidedWith;
                animation.SetAnimationState(AnimationState.Death);
                return false;
            }
            if (collidedWith is MeleeRedIfrit)
            {
                return true;
            }
            return true;     
        }
        
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            set
            {
                _isDead = value;
            }
        }
    }
}
