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

namespace StorybrewScripts
{
    public class DScript : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    PerspectiveCamera camera = new PerspectiveCamera();

            camera.NearClip.Add(0, 0.3f);
            // camera.FarClip.Add();
            camera.NearFade.Add(0,0);
            camera.FarFade.Add(0, 0);
            camera.HorizontalFov.Add(0, 60);
            camera.VerticalFov.Add(0, 60);

            camera.Up.Add(0, Vector3.UnitY);
            camera.PositionX.Add(0, 0);
            camera.PositionX.Add(1000, -300);
            camera.PositionY.Add(0, 0);
            camera.PositionY.Add(1000, 300);
            camera.PositionZ.Add(0, 0);
            camera.PositionZ.Add(3000, 100);

            var testSprite = new Sprite3d();
            testSprite.sprite = GetLayer("").CreateSprite("sb/dot.png", OsbOrigin.Centre);

            testSprite.PositionX.Add(0, 500);
            testSprite.PositionY.Add(0, 800);
            testSprite.PositionZ.Add(0, -10000);
            testSprite.ScaleX.Add(200);
            testSprite.ScaleY.Add(200);
            testSprite.ScaleZ.Add(200);
            testSprite.Opacity.Add(0, 0.8f);

            for (var time = 0; time < 3000; time += 100)
            {
                testSprite.GenerateStates(0, camera.StateAt(time), new Object3dState(testSprite.WorldTransformAt(time), Color4.White, 0.8f));
            }
            
            testSprite.GenerateCommands(null, 0, 3000, 0, false);
        }
    }
}
