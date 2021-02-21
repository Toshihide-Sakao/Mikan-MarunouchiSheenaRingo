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
    public class Transitions : StoryboardObjectGenerator
    {
        string dotPath = "sb/dot.png";
        public override void Generate()
        {
            var layer = GetLayer("");
            SqTransition(layer, 30537, 32144, 3, 320, 3, Color4.White, 1 / 4f);
            SqTransition(layer, 30537, 32144, 3, 320, 1, Color4.White, 1 / 4f);

            var layer2 = GetLayer("aTrans");
            SqTransition(layer2, 34823, 35894, 3, 320, 3, Color4.White, 1 / 4f);
            SqTransition(layer2, 34823, 35894, 3, 320, 1, Color4.White, 1 / 4f);

            var layer3 = GetLayer("bruhbruh");
            SqTransition(layer3, 39109, 40716, 3, 320, 3, colorRGB(122, 230, 185), 1 / 3f);
            SqTransition(layer3, 39109, 40716, 3, 320, 1, colorRGB(122, 230, 185), 1 / 3f);
            SqTransition(layer3, 39644, 40716, 3, 320, 3, colorRGB(235, 117, 117), 1 / 3f);
            SqTransition(layer3, 39644, 40716, 3, 320, 1, colorRGB(235, 117, 117), 1 / 3f);

            // bruh lolololol
            var layer4 = GetLayer("haha burh shit hate u");
            SqTransition(layer4, 73394, 73930, 3, 320, 3, colorRGB(168, 176, 247), 1 / 3f);
            SqTransition(layer4, 73394, 73930, 3, 320, 1, colorRGB(168, 176, 247), 1 / 3f);

            var layer5 = GetLayer("after Hahabruh");
            SqTransition(layer5, 77680, 78216, 3, 320, 3, colorRGB(166, 161, 154), 1 / 3f);
            SqTransition(layer5, 77680, 78216, 3, 320, 1, colorRGB(166, 161, 154), 1 / 3f);

            var layer6 = GetLayer("ok after the after hahabruh");
            SqTransition(layer5, 81966, 82501, 3, 320, 3, colorRGB(227, 152, 173), 1 / 3f);
            SqTransition(layer5, 81966, 82501, 3, 320, 1, colorRGB(227, 152, 173), 1 / 3f);
            SqTransition(layer5, 82501, 83037, 3, 320, 3, colorRGB(255, 255, 255), 1 / 3f);
            SqTransition(layer5, 82501, 83037, 3, 320, 1, colorRGB(255, 255, 255), 1 / 3f);
        }

        void SqTransition(StoryboardLayer layer, int start, int end, int amount, float endX, int fromWhere, Color4 color, double beats)
        {
            var beatDuration = Beatmap.GetTimingPointAt(start).BeatDuration;
            var sqsTop = new OsbSprite[amount];
            var sqsBot = new OsbSprite[amount];



            float width = (endX + 107f) / amount;
            float time = start;

            if (fromWhere == 3)
            {
                for (int i = 0; i < sqsTop.Length; i++)
                {
                    float x = width * i - 107;
                    var thingTop = layer.CreateSprite(dotPath, OsbOrigin.TopLeft, new Vector2(x, 0));
                    thingTop.ScaleVec(OsbEasing.OutQuint, time, time + beatDuration * beats, width, 0, width, 240);

                    var thingBot = layer.CreateSprite(dotPath, OsbOrigin.BottomLeft, new Vector2(x, 480));
                    thingBot.ScaleVec(OsbEasing.OutQuint, time, time + beatDuration * beats, width, 0, width, 240);

                    thingTop.Fade(start, end, 1, 1);
                    thingBot.Fade(start, end, 1, 1);
                    thingTop.Color(start, color);
                    thingBot.Color(start, color);

                    time += (float)(beatDuration * beats);
                }
            }
            else if (fromWhere == 1)
            {
                for (int i = sqsTop.Length - 1; i >= 0; i--)
                {
                    float x = 427 + width * i - 107;
                    var thingTop = layer.CreateSprite(dotPath, OsbOrigin.TopLeft, new Vector2(x, 0));
                    thingTop.ScaleVec(OsbEasing.OutQuint, time, time + beatDuration * beats, width, 0, width, 240);

                    var thingBot = layer.CreateSprite(dotPath, OsbOrigin.BottomLeft, new Vector2(x, 480));
                    thingBot.ScaleVec(OsbEasing.OutQuint, time, time + beatDuration * beats, width, 0, width, 240);

                    thingTop.Fade(start, end, 1, 1);
                    thingBot.Fade(start, end, 1, 1);
                    thingTop.Color(start, color);
                    thingBot.Color(start, color);

                    time += (float)(beatDuration * beats);
                }
            }


            // OsbSprite[] wholeArr = new OsbSprite[sqsTop.Length * 2];
            // sqsTop.CopyTo(wholeArr, 0);
            // sqsBot.CopyTo(wholeArr, sqsTop.Length);
            // return wholeArr;
        }

        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
