using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Storyboarding.CommandValues;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Trying : StoryboardObjectGenerator
    {
        public string dotPath = "sb/dot.png";
        public override void Generate()
        {
		    particlesUpwards(67502, 76535);
            
        }

        public void particlesUpwards(int startTime, int endTime)
        {
            var beatDuration = Beatmap.GetTimingPointAt(startTime).BeatDuration;

            var scene = new Scene3d();
            var camera = new PerspectiveCamera();

            camera.PositionX.Add(0, 0);
            camera.PositionY.Add(0, 0);
            camera.PositionZ.Add(0, 500);
            camera.NearClip.Add(0, 10);
            camera.NearFade.Add(0, 20);
            camera.FarClip.Add(0, 150);
            camera.FarFade.Add(0, 170);
            var parent = scene.Root;

            var particle = new Sprite3d()
            {
                SpritePath = dotPath,
                UseDistanceFade = false,
                RotationMode = RotationMode.Fixed,
            };

            particle.Opacity
                .Add(startTime, 0)
                .Add(startTime + beatDuration * 4, 1);
            particle.PositionX
                .Add(startTime, 200)
                .Add(startTime + (endTime - startTime) / 2, -500, EasingFunctions.BackInOut)
                .Add(endTime, 100, EasingFunctions.BackInOut);
            particle.PositionY
                .Add(startTime, -300, EasingFunctions.BackInOut)
                .Add(endTime, 0, EasingFunctions.BackInOut);
            particle.PositionZ
                .Add(startTime, 90)
                .Add(startTime + (endTime - startTime) / 2, 20, EasingFunctions.BackInOut)
                .Add(endTime, 50, EasingFunctions.BackInOut);
            particle.SpriteScale
                .Add(startTime, 50);
            // particle.Rotation
            //     .Add()

            parent.Add(particle);


            scene.Generate(camera, GetLayer(""), startTime, endTime, beatDuration / 8);
        }
    }
}
