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
    public class DistortedBGToBeat : StoryboardObjectGenerator
    {
        public string backPath = "main_visual_momoco-ex-pc.jpg";
        public string dotPat = "sb/dot.png";
        public int StartTime = 113573;
        public int EndTime = 130716;
        public override void Generate()
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration;
            int duration = EndTime - StartTime;


            var bitmap = GetMapsetBitmap(backPath);
            // int[] MaxXs = new int[] {316, 320, } 

            var backLayer = GetLayer("glitchedBack");

            var backSprites = new OsbSprite[]
            {
                backLayer.CreateSprite(backPath, OsbOrigin.Centre, new Vector2(320, 240)),
                backLayer.CreateSprite(backPath, OsbOrigin.Centre, new Vector2(320, 240)),
                backLayer.CreateSprite(backPath, OsbOrigin.Centre, new Vector2(320, 240))
            };

            // var overEyes = CreateOverEye();

            for (int i = 0; i < backSprites.Length; i++)
            {
                backSprites[i].Additive(StartTime);
                backSprites[i].Scale(StartTime, EndTime + 500, 0.36f, 0.36f);
                backSprites[i].Fade(StartTime, 1);
            }

            backSprites[0].Color(StartTime, colorRGB(255, 0, 0));
            backSprites[1].Color(StartTime, colorRGB(0, 255, 0));
            backSprites[2].Color(StartTime, colorRGB(0, 0, 255));

            int amountTime = (int)beatDuration;
            Vector2 position = new Vector2(320, 240);
            Vector2 newPosition = new Vector2(320, 240);

            for (int time = StartTime; time < EndTime; time += amountTime)
            {
                newPosition = new Vector2(position.X + Random(-2, 2), position.Y + Random(-2, 2));

                int rnd = Random(3, 5);
                int durationOk = 150;
                int durationNext = amountTime - durationOk;

                backSprites[0].Move(OsbEasing.OutQuad, time, time + (int)(durationOk), position.X + 1, position.Y, newPosition.X + rnd, newPosition.Y);
                backSprites[0].Move(OsbEasing.In, time + (int)(durationOk), time + (int)(durationNext), newPosition.X + rnd, newPosition.Y, position.X + 1, position.Y);

                backSprites[1].Move(OsbEasing.Out, time, time + (int)(durationOk), position, newPosition);
                backSprites[1].Move(OsbEasing.In, time + (int)(durationOk), time + (int)(durationNext), newPosition, position);

                backSprites[2].Move(OsbEasing.OutQuad, time, time + (int)(durationOk), position.X - 1, position.Y, newPosition.X - rnd, newPosition.Y);
                backSprites[2].Move(OsbEasing.In, time + (int)(durationOk), time + (int)(durationNext), newPosition.X - rnd, newPosition.Y, position.X - 1, position.Y);

                // if (time < 129644)
                // {
                //     for (int i = 0; i < overEyes.Length; i++)
                //     {
                //         overEyes[i].Fade(OsbEasing.InCubic, time, time + (int)(durationOk), 1, 0.75f);
                //         overEyes[i].Fade(OsbEasing.In, time + (int)(durationOk), time + (int)(durationNext), 0.75f, 1f);
                //     }
                // }


            }
            var flLayer = GetLayer("sdsd");
            var white = flLayer.CreateSprite(dotPat, OsbOrigin.Centre);
            white.ScaleVec(129644, 130716, 854, 480, 854, 480);
            white.Fade(OsbEasing.InQuad, 129644, 130716, 0, 1);
            white.Fade(131251, 131519, 1, 0);
            white.Color(OsbEasing.Out, 130716, 131251, Color4.White, Color4.Black);
        }

        OsbSprite[] CreateOverEye()
        {
            var layer = GetLayer("Eyes");
            OsbSprite[] overEyes = new OsbSprite[3];

            for (int i = 0; i < overEyes.Length; i++)
            {
                overEyes[i] = layer.CreateSprite(dotPat, OsbOrigin.Centre, new Vector2(320, 117 + i * 3));
                overEyes[i].ScaleVec(StartTime, EndTime + 500, 854, 90, 854, 90);
                overEyes[i].Additive(StartTime);
                overEyes[i].Fade(EndTime + 500, 0.6f);
            }

            overEyes[0].Color(StartTime, colorRGB(255, 0, 0));
            overEyes[1].Color(StartTime, colorRGB(0, 255, 0));
            overEyes[2].Color(StartTime, colorRGB(0, 0, 255));


            return overEyes;
        }


        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
