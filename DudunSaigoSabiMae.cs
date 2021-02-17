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
    public class DudunSaigoSabiMae : StoryboardObjectGenerator
    {
        public string dotPath = "sb/dot.png";
        public override void Generate()
        {
            var beatDuration = Beatmap.GetTimingPointAt((int)137144).BeatDuration;
		    var layer = GetLayer("");

            var b1 = layer.CreateSprite(dotPath, OsbOrigin.Centre);
            var b2 = layer.CreateSprite(dotPath, OsbOrigin.Centre);


            b1.ScaleVec(137144, 141698, 854, 480, 854, 480);
            b1.Fade(141430, 141698, 1, 0);

            b2.ScaleVec(137144, 141698, 854, 480, 854, 480);
            b2.Color(137144, colorRGB(173, 105, 209));
            b2.Color(137144 + beatDuration / 4, colorRGB(237, 231, 109));
            b2.Color(137948, colorRGB(104, 222, 216));
            b2.Color(137948 + beatDuration /4, colorRGB(196, 112, 168));

            b2.Color(139287, colorRGB(173, 105, 209));
            b2.Color(139287 + beatDuration / 4, colorRGB(237, 231, 109));
            b2.Color(140091, colorRGB(104, 222, 216));
            b2.Color(140091 + beatDuration /4, colorRGB(196, 112, 168));
            b2.Fade(OsbEasing.InBack, 137144, 141698, 0.9f, 0);
            
        }

        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
