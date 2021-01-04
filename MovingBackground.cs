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
    public class MovingBackground : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;
        public double Opacity = 1;

        public override void Generate()
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)StartTime).BeatDuration;
            int duration = EndTime - StartTime;


            var bitmap = GetMapsetBitmap(BackgroundPath);
            var bg = GetLayer("").CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(StartTime, 500.0f / bitmap.Height);
            bg.Fade(StartTime - 500, StartTime, 0, Opacity);
            bg.Fade(EndTime, EndTime + 500, Opacity, 0);

            Vector2 position = new Vector2(320, 240);
            Vector2 newPosition = new Vector2(320, 240);
            
            for (int time = StartTime; time < EndTime; time += (int)(beatDuration * 2))
            {
                newPosition = new Vector2(position.X + Random(-3, 3), position.Y + Random(-3, 3));
                bg.Move(OsbEasing.None, time, time + (int)(beatDuration), position, newPosition);
                bg.Move(OsbEasing.None, time + (int)(beatDuration), time + (int)(beatDuration * 2), newPosition, position);

                float randomdegree = Random(-2, 2);
                bg.Rotate(OsbEasing.None, time, time + (int)(beatDuration), MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(randomdegree));
                bg.Rotate(OsbEasing.None, time + (int)(beatDuration), time + (int)(beatDuration * 2), MathHelper.DegreesToRadians(randomdegree), MathHelper.DegreesToRadians(0));
                
                position = newPosition;
            }
        }
    }
}
