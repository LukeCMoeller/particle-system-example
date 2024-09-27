﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
namespace ParticleSystemExample
{
    public class FireworkParticleSystem : ParticleSystem
    {
        Color[] colors = new Color[]{
        Color.Fuchsia,
        Color.Red,
        Color.Green,
        Color.HotPink,
        Color.Gainsboro,
        Color.LimeGreen
        };
        Color color;
        public FireworkParticleSystem(Game game, int maxExplosions) : base(game, maxExplosions * 25) { }
        protected override void InitializeConstants()
        {
            textureFilename = "circle";
            minNumParticles = 10;
            maxNumParticles = 25;
            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }
        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDirection() * RandomHelper.NextFloat(40, 200);
            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);
            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            var acceleration = -velocity / lifetime;
            var scale = RandomHelper.NextFloat(4, 6);
            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }
        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);
            float normalizelifetime = particle.TimeSinceStart / particle.Lifetime;
            

            particle.Scale = 0.1f + 0.25f * normalizelifetime;
        }
        public void PlaceFirework(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }
    }
}
