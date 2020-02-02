﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Actors
{
    public class Breaker : PlayerActor
    {
        public Breaker()
        {

        }

        protected override void UpdateCheering()
        {
        }

        protected override void UpdateDefeat()
        {
        }

        protected override void UpdateIdle()
        {
            if (Input.GetAxis("Vertical") > sensitivity)
            {
                TryMove(Position.X, Position.Y - 1);
            }
            else if (Input.GetAxis("Vertical") < -sensitivity)
            {
                TryMove(Position.X, Position.Y + 1);
            }
            else if (Input.GetAxis("Horizontal") > sensitivity)
            {
                TryMove(Position.X - 1, Position.Y);
            }
            else if (Input.GetAxis("Horizontal") < -sensitivity)
            {
                TryMove(Position.X + 1, Position.Y);
            }

            else if (Input.GetButtonDown("DoStuff"))
                this.playerState = PlayerState.INTERACTING;
        }

        protected override void UpdateInteracting()
        {
            Position interactPosition = Position;
            switch (facing)
            {
                case Facing.UP:
                    interactPosition.Y--;
                    break;
                case Facing.DOWN:
                    interactPosition.Y++;
                    break;
                case Facing.LEFT:
                    interactPosition.X--;
                    break;
                case Facing.RIGHT:
                    interactPosition.X++;
                    break;
            }

            mapManager.BreakTile(Position.X, Position.Y);

            //If animation finished:
            playerState = PlayerState.IDLE;
        }
    }
}
