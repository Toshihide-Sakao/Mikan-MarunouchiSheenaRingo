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
    public class ParticleToBeat : StoryboardObjectGenerator
    {
        [Configurable] public int startTime = 45001;
        [Configurable] public int endTime = 61073;
        [Configurable] public string ballPath = "sb/fadeBall.png";
        public override void Generate()
        {
            int particleAmount = 40;
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            var layer = GetLayer("");
            float time = startTime;

            int loopCount = (int)((endTime - startTime) / beatDuration);
            for (double l = startTime; l < endTime; l += beatDuration)
            {
                for (int i = 0; i < particleAmount; i++)
                {
                    float opacity = Random(0.3f, 0.5f);
                    float randomX = Random(-107, 747);
                    float randomScale = Random(0.01f * 10, 0.04f * 10);
                    float yPos = 510f;
                    var particle = layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(randomX, yPos));

                    particle.Scale(l, randomScale);
                    particle.Fade(l, opacity);
                    particle.Color(l, new Color4(175, 236, 250, 1));

                    float uniqueTime = (float)l;
                    for (int j = 0; j < 5; j++)
                    {

                        float nextY = yPos - Random(100, 200);
                        particle.MoveY(OsbEasing.OutExpo, uniqueTime, uniqueTime + beatDuration, yPos, nextY);
                        uniqueTime += (float)beatDuration;
                        yPos = nextY;

                        if (particle.PositionAt(uniqueTime).Y <= -50.0f)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
