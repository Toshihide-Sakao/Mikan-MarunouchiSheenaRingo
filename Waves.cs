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
    public class Waves : StoryboardObjectGenerator
    {
        string wavePath = "sb/wave.png";
        string dotPath = "sb/dot.png";

        public override void Generate()
        {
            // GenerateWave(32144, 36430);
        }
        void GenerateWave(int start, int last)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)start).BeatDuration;
            double end = start + beatDuration * 2;
            var layer = GetLayer("");
            
            var waveBitmap = GetMapsetBitmap(wavePath);
            float bruh = waveBitmap.Width / 854f;
            float x = -107f;
            float lastX = Random(854f, 1000);

            for (int i = 0; i < 12f; i++)
            {
                
                var Wave = layer.CreateSprite(wavePath, OsbOrigin.BottomRight, new Vector2(x, 320));
                var waveUnder = layer.CreateSprite(wavePath, OsbOrigin.BottomLeft, new Vector2(x, 320));

                
                Wave.MoveX(OsbEasing.OutExpo, start, end, x, lastX);
                waveUnder.MoveX(OsbEasing.OutExpo, start, end, x, lastX);
                Wave.Color(start, last, Color4.Blue, Color4.Blue);
                waveUnder.Color(start, last, Color4.Blue, Color4.Blue);
                Wave.Fade(start, 0.6f);
                waveUnder.Fade(start, 0.6f);
                waveUnder.Rotate(start, MathHelper.DegreesToRadians(180));

                lastX -= waveBitmap.Width;
                x -= waveBitmap.Width;
            }


        }
    }
}
