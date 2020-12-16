using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.CommandValues;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace StorybrewScripts
{
    public class Seed : StoryboardObjectGenerator
    {
        private CommandColor seedColor = CommandColor.FromHtml("#000309");

        private CommandColor lightBrown = CommandColor.FromHtml("#B8A58D");
        private CommandColor darkBrown = CommandColor.FromHtml("#525252");
        private CommandColor lightBlue = CommandColor.FromHtml("#4E707D");
        private CommandColor darkBlue = CommandColor.FromHtml("#0B131F");

        public override void Generate()
        {
            seedDrop(4286, 12986);

            // var darkGlow = GetLayer("").CreateSprite("sb/triangle.png", OsbOrigin.Centre, new Vector2(445, 405));
            // darkGlow.Scale(OsbEasing.OutExpo, 14186, 15986, 4, 8);
            // darkGlow.Fade(OsbEasing.InExpo, 14186, 15986, 1, 0);
            // darkGlow.Color(14186, 15986, Color4.Black, darkBlue);

            // var glow = GetLayer("").CreateSprite("sb/triangle.png", OsbOrigin.Centre, new Vector2(445, 405));
            // glow.Scale(OsbEasing.InExpo, 14186, 14261, 6, 2);
            // glow.Fade(OsbEasing.InExpo, 14186, 14261, 1, 0);
            // glow.Color(14186, lightBlue);
            // glow.Additive(14186, 14261);
        }

        public void seedDrop(double startTime, double endTime)
        {           
            var b0 = Beatmap.Bookmarks.ElementAt(0);
            var beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            var scene = new Scene3d();
            var camera = new PerspectiveCamera();
            camera.PositionX.Add(0, 0);
            camera.PositionY.Add(0, 0);
            camera.PositionZ.Add(0, 100);
            camera.NearClip.Add(0, 80);
            camera.NearFade.Add(0, 90);
            camera.FarFade.Add(0, 110);
            camera.FarClip.Add(0, 160);
            var parent = scene.Root;

            var seed = new Sprite3d()
            {
                SpritePath = "sb/triangle.png",
                UseDistanceFade = false,
                RotationMode = RotationMode.Fixed,
            };
            seed.ConfigureGenerators(g =>
            {
                g.PositionDecimals = 0;
                g.ColorTolerance = 10;
            });
            seed.Opacity
                .Add(startTime - beatDuration * 6, 0)
                .Add(startTime, 1);
            seed.Coloring
                .Add(startTime - beatDuration, darkBrown)
                .Add(startTime, lightBrown)
                .Add(endTime, seedColor);
            seed.PositionX
                .Add(startTime, 240)
                .Add((startTime + endTime) / 2, -300, EasingFunctions.BackInOut)
                .Add(endTime, 200, EasingFunctions.BackInOut);
            seed.PositionY
                .Add(startTime, -340)
                .Add(endTime, 310);
            seed.Rotation
                .Add(startTime, -(float)Math.PI)
                .Add((startTime + endTime) / 2, 0, EasingFunctions.BackInOut)
                .Add(endTime, (float)Math.PI, EasingFunctions.BackInOut);
            seed.SpriteScale
                .Add(startTime, 0.5f, 0.5f)
                .Until(endTime - beatDuration)
                .Add(endTime, 0, 0);
            parent.Add(seed);

            var glow = new Sprite3d()
            {
                SpritePath = "sb/dot.png",
                UseDistanceFade = false,
                RotationMode = RotationMode.Fixed,
                InheritsColor = false,
                Additive = true,
            };
            glow.ConfigureGenerators(g =>
            {
                g.PositionDecimals = 0;
                g.ColorTolerance = 10;
            });
            glow.Opacity
                .Add(endTime - beatDuration * 6, 0)
                .Add(endTime, 1);
            glow.Coloring
                .Add(endTime, lightBlue);
            glow.SpriteScale
                .Add(endTime - beatDuration * 6, 1f, 1f)
                .Add(endTime + beatDuration * 6, 0, 0);
            seed.Add(glow);

            var amount = 100;
            for (var i = 0; i < amount; i++)
            {
                var t0 = startTime + beatDuration * i;
                var t1 = startTime + beatDuration * (i + 1);
                var t2 = startTime + beatDuration * (i + 5);
                var t3 = startTime + beatDuration * (i + 6);

                if (endTime < t3)
                    break;

                var extraSeed = new Sprite3d()
                {
                    SpritePath = "sb/dot.png",
                    UseDistanceFade = false,
                    InheritsColor = false,
                    RotationMode = RotationMode.Fixed,
                };
                extraSeed.ConfigureGenerators(g =>
                {
                    g.PositionDecimals = 0;
                });
                var scale = 1.3f - (i * (0.25f / amount));
                extraSeed.PositionX
                    .Add(startTime, 0)
                    .Add(endTime, Random(-100, 100));
                extraSeed.PositionY
                    .Add(startTime, Random(-40, 40))
                    .Add(endTime, Random(-50, 50));
                extraSeed.PositionZ
                    .Add(startTime, Random(-10, 10))
                    .Add(endTime, Random(-40, 40));
                extraSeed.Opacity
                    .Add(t0, 0)
                    .Add(t1, 1)
                    .Until(t2)
                    .Add(t3, 0);
                extraSeed.SpriteScale.Add(startTime, scale, scale);
                extraSeed.Coloring.Add(startTime, i % 2 == 0 ? lightBrown : darkBrown);
                seed.Add(extraSeed);
            }

            scene.Generate(camera, GetLayer(""), 0, b0, beatDuration / 8);
        }
    }
}
