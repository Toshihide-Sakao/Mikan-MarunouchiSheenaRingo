using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Storyboarding3d;
using StorybrewCommon.Animations;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class CoolTHIngs : StoryboardObjectGenerator
    {
        [Configurable] public int startTime;
        [Configurable] public int endTime;
        public string ballPath = "sb/ball.png";

        public override void Generate()
        {
            var layer = GetLayer("");
            var beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            var ball = layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, 550));
            ball.MoveY(OsbEasing.OutBack, startTime, 27859, 550, 240);
            ball.Scale(startTime, 0.6f);
            ball.Scale(OsbEasing.OutCirc, 27859, 27993, 0.6f, 1.1f);
            ball.Scale(OsbEasing.InCirc, 27993, 28261, 1.1f, 0.9f);
            ball.Scale(OsbEasing.OutCirc, 28663, 28663 + beatDuration / 4, 0.9f, 1.0f);
            ball.Scale(OsbEasing.InCirc, 28663 + beatDuration / 4, 28663 + beatDuration / 2, 1.0f, 0.9f);
            ball.Scale(OsbEasing.OutCirc, 28931, 29132, 0.9f, 1.2f);
            ball.Scale(OsbEasing.InCirc, 29132, 29332, 1.2f, 0.9f);
            CreateBounce(ball, 29332, 29734, 0.9f, 1.3f, 0.95f);
            // ball.Scale(OsbEasing.OutBack, 29734, )
            ball.Fade(startTime, endTime, 0.8f, 0.8f);
        }

        void CreateBounce(OsbSprite sprite, int start, int end, float startscale, float maxscale, float endscale)
        {
            int middle = start + ((end - start) / 2);
            // Log(end - start / 2);
            sprite.Scale(OsbEasing.OutCirc, start, middle, startscale, maxscale);
            sprite.Scale(OsbEasing.InCirc, middle, end, maxscale, endscale);
        }
    }
}
