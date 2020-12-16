using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace StorybrewScripts
{
    public class Kiai3d : StoryboardObjectGenerator
    {
        public override void Generate()
        {

            PerspectiveCamera camera = new PerspectiveCamera();

            camera.NearClip.Add(0, 0.3f);
            camera.FarClip.Add(0, 22500);
            camera.NearFade.Add(0, 0);
            camera.FarFade.Add(0, 0);
            camera.HorizontalFov.Add(0, 60);
            camera.VerticalFov.Add(0, 60);

            camera.Up.Add(0, Vector3.UnitY);
            camera.PositionX.Add(0, 0);
            camera.PositionX.Add(200, -50);
            camera.PositionY.Add(0, 0);
            camera.PositionY.Add(200, 250);
            camera.PositionZ.Add(0, 0);
            camera.PositionZ.Add(1000, 100);

            generateSqBack();
        }

        public void generateSqBack() 
        {
            Vector2 xyposition = new Vector2(Random(3000), Random(3000));
            var zposition = Random(-45000, -500);
            var scale = new Vector2(20, 100);
            var color = Color.White;

            var sprite = new Sprite3d();
            sprite.sprite = GetLayer("").CreateSprite("sb/triangle.png", OsbOrigin.Centre);

            sprite.PositionX.Add(0, xyposition.X);
            sprite.PositionY.Add(0, xyposition.Y);
            sprite.PositionZ.Add(0, zposition);
            sprite.SpriteScale.Add(0, scale);
            sprite.Coloring.Add(0, color);

        }
    }
}
