﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using WizardsNeverDie.Entities;
using WizardsNeverDie.Level;

namespace WizardsNeverDie.Physics
{
    public class BasicBody : PhysicsBody
    {
        AbstractEntity entity;
        Vector2 position;
        float size;
        public BasicBody(AbstractEntity entity, Vector2 position, float size)
            : base()
        {
            this.entity = entity;
            this.position = position;
            this.size = size;

            World world = Farseer.Instance.World;
            Body body;
            if(entity is Gotfraggon)
            {
                body = BodyFactory.CreateRectangle(world, 10f, 18f, 1f);
            }
            else
            {
                body = BodyFactory.CreateCircle(world, size, 1f);
            }
            Bodies.Add(body);
            foreach (Fixture fixture in body.FixtureList)
                fixture.UserData = entity;
            body.UserData = entity;
            body.Position = position;
            body.Friction = float.MaxValue;
            body.Restitution = 0.3f;
            body.BodyType = BodyType.Dynamic;
            body.SleepingAllowed = false;
            body.CollisionCategories = Category.Cat1;
            body.CollidesWith = Category.Cat1;
            body.Awake = true;
            body.OnCollision += new OnCollisionEventHandler(onCollision);
        }
        bool onCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            AbstractEntity collided = fixtureB.UserData as AbstractEntity;
            return entity.WillCollide(collided);
        }
    }
}
