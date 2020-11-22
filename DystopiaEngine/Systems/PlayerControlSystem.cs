// Original code dervied from:
// https://github.com/thelinuxlich/starwarrior_CSharp/blob/master/StarWarrior/StarWarrior/Systems/PlayerShipControlSystem.cs

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerShipControlSystem.cs" company="GAMADU.COM">
//     Copyright © 2013 GAMADU.COM. All rights reserved.
//
//     Redistribution and use in source and binary forms, with or without modification, are
//     permitted provided that the following conditions are met:
//
//        1. Redistributions of source code must retain the above copyright notice, this list of
//           conditions and the following disclaimer.
//
//        2. Redistributions in binary form must reproduce the above copyright notice, this list
//           of conditions and the following disclaimer in the documentation and/or other materials
//           provided with the distribution.
//
//     THIS SOFTWARE IS PROVIDED BY GAMADU.COM 'AS IS' AND ANY EXPRESS OR IMPLIED
//     WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL GAMADU.COM OR
//     CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
//     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
//     ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//     NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
//     ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//     The views and conclusions contained in the software and documentation are those of the
//     authors and should not be interpreted as representing official policies, either expressed
//     or implied, of GAMADU.COM.
// </copyright>
// <summary>
//   The player ship control system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using DystopiaEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace DystopiaEngine.Systems
{
    public class PlayerControlSystem : EntityProcessingSystem
    {
        private readonly EntityFactory _entityFactory;
        private KeyboardState _lastState;

        public PlayerControlSystem(EntityFactory entityFactory) : base(Aspect.All(typeof(PlayerComponent), typeof(Transform2)))
        {
            _entityFactory = entityFactory;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var entity = GetEntity(entityId);
            var transform = entity.Get<Transform2>();

            var keyboard = Keyboard.GetState();
            var direction = Vector2.Zero;

            if (keyboard.IsKeyDown(Keys.OemComma) || keyboard.IsKeyDown(Keys.Up))
                direction -= Vector2.UnitY;

            if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
                direction -= Vector2.UnitX;

            if (keyboard.IsKeyDown(Keys.O) || keyboard.IsKeyDown(Keys.Down))
                direction += Vector2.UnitY;

            if (keyboard.IsKeyDown(Keys.E) || keyboard.IsKeyDown(Keys.Right))
                direction += Vector2.UnitX;

            var isMoving = direction != Vector2.Zero;
            if (isMoving)
                direction.Normalize();

            var speed = 400;
            transform.Position += direction * 400 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _lastState = keyboard;
        }
    }
}
