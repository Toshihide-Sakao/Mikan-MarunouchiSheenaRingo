using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Animations;
using System.Drawing;

namespace StorybrewScripts
{
    public class Space : StoryboardObjectGenerator
    {
        [Configurable] public int startTime = 131251;
        [Configurable] public int endTime = 137144;
        [Configurable] public int easings = 0;
        public override void Generate()
        {
            var b0 = Beatmap.Bookmarks.ElementAt(0);
            var beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            PerspectiveCamera camera = new PerspectiveCamera();

            camera.NearClip.Add(startTime, 0.1f);
            camera.FarClip.Add(startTime, 22500);
            camera.NearFade.Add(startTime, 0);
            camera.FarFade.Add(startTime, 0);
            camera.HorizontalFov.Add(startTime, 60);
            camera.VerticalFov.Add(startTime, 60);

            camera.Up.Add(0, Vector3.UnitY);
            camera.PositionX.Add(startTime, 0);
            camera.PositionY.Add(startTime, 0);
            camera.PositionZ.Add(startTime, -4000);
            if (easings == 0)
            {
                camera.PositionZ.Add(endTime, -10000, EasingFunctions.CircIn);
            }
            else
            {
                camera.PositionZ.Add(endTime, -10000, EasingFunctions.CircOut);
            }
            

            for (int i = 0; i < 500; i++)
            {
                Vector2 xyposition = new Vector2(Random(-2000, 2000), Random(-2000, 2000));
                var zposition = Random(-11000, -500);
                var scale = new Vector2(5, 5);
                var color = Color.White;

                var sprite = new Sprite3d();
                sprite.sprite = GetLayer("").CreateSprite("sb/dot.png", OsbOrigin.Centre);

                sprite.PositionX.Add(0, xyposition.X);
                sprite.PositionY.Add(0, xyposition.Y);
                sprite.PositionZ.Add(0, zposition);
                sprite.SpriteScale.Add(0, scale);
                sprite.Coloring.Add(0, color);

                for (double time = startTime; time <= endTime; time += beatDuration / 8)
                {
                    sprite.GenerateStates(time, camera.StateAt(time), new Object3dState(sprite.WorldTransformAt(time), Color.White, 1) );
                }

                sprite.GenerateCommands(null, startTime, endTime, 0, false);
            }
        }
    }
}
