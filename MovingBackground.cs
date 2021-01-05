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
            bg.Scale(StartTime, 550.0f / bitmap.Height);
            bg.Fade(StartTime - 500, StartTime, 0, Opacity);
            bg.Fade(EndTime, EndTime + 500, Opacity, 0);

            Vector2 position = new Vector2(320, 240);
            Vector2 newPosition = new Vector2(320, 240);
            float startDegree = Random(-5, -2);
            
            for (int time = StartTime; time < EndTime; time += (int)(beatDuration * 2))
            {
                newPosition = new Vector2(position.X + Random(-6, 6), position.Y + Random(-6, 6));
                bg.Move(OsbEasing.Out, time, time + (int)(beatDuration), position, newPosition);
                bg.Move(OsbEasing.Out, time + (int)(beatDuration), time + (int)(beatDuration * 2), newPosition, position);

                float nextStartDegree = Random(-3, -1);
                float endDegreee = Random(1, 3);
                bg.Rotate(OsbEasing.Out, time, time + (int)(beatDuration), MathHelper.DegreesToRadians(startDegree), MathHelper.DegreesToRadians(endDegreee));
                bg.Rotate(OsbEasing.Out, time + (int)(beatDuration), time + (int)(beatDuration * 2), MathHelper.DegreesToRadians(endDegreee), MathHelper.DegreesToRadians(nextStartDegree));
                
                startDegree = nextStartDegree;
                // position = newPosition;
            }
        }
    }
}
