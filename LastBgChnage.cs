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

namespace StorybrewScripts
{
    public class LastBgChnage : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("");
            var bg = layer.CreateSprite("sb/dot.png", OsbOrigin.Centre);
            bg.ScaleVec(175448, 854, 480);
            bg.Fade(OsbEasing.InQuad, 175448, 175716, 0, 1);
            bg.Color(175448, 178796, Color4.Black, Color4.Black);

            var layer2 = GetLayer("bruh");

            var lastBg = layer2.CreateSprite("sb/dot.png", OsbOrigin.Centre);
            lastBg.ScaleVec(185359, 854, 480);
            lastBg.Fade(OsbEasing.InQuad, 185359, 186430, 0, 1);
            lastBg.Color(185359, 186430, Color4.Black, Color4.Black);
            
        }
    }
}
