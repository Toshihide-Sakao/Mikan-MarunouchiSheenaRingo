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
            float startScale = 0.1f;

            var ball = layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, 550));
            ball.MoveY(OsbEasing.OutBack, startTime, 27859, 550, 240);
            ball.Scale(startTime, 0.1f);
            // ball.Scale(OsbEasing.OutCirc, 27859, 27993, startScale, startScale * 1.2f);
            // ball.Scale(OsbEasing.InCirc, 27993, 28261, startScale * 1.2f, startScale * 1.05f);
            // ball.Scale(OsbEasing.OutCirc, 28663, 28663 + beatDuration / 4, startScale * 1.05f, 1.0f);
            // ball.Scale(OsbEasing.InCirc, 28663 + beatDuration / 4, 28663 + beatDuration / 2, 1.0f, 0.9f);
            // ball.Scale(OsbEasing.OutCirc, 28931, 29132, 0.9f, 1.2f);
            // ball.Scale(OsbEasing.InCirc, 29132, 29332, 1.2f, 0.9f);
            CreateBounce(ball, 27859, 1.5f, startScale * 1.4f, startScale * 1.1f);
            CreateBounce(ball, 28663, 0.25f, startScale * 1.2f, startScale);
            CreateBounce(ball, 28796, 0.25f, startScale * 1.2f, startScale);
            CreateBounce(ball, 28930, 0.75f, startScale * 1.4f, startScale * 1.1f);
            CreateBounce(ball, 28931, 0.5f, startScale * 1.4f, startScale * 1.1f);
            CreateBounce(ball, 29332, 0.75f, startScale * 1.5f, startScale * 1.3f);
            CreateBounce(ball, 29734, 0.5f, startScale * 1.5f, startScale * 1.3f);
            CreateBounce(ball, 30002, 1f, startScale * 1.5f, startScale * 1.3f);
            
            ball.Scale(OsbEasing.OutBack, 30537, 30537 + beatDuration / 4, ball.ScaleAt(30537).X, 0.1f);
            ball.Scale(OsbEasing.OutBack, 30537 + beatDuration / 4, 30537 + beatDuration / 2, ball.ScaleAt(30537 + beatDuration / 4).X, 0.08f);
            ball.Scale(OsbEasing.OutBack, 30537 + beatDuration / 2, 30537 + beatDuration * 3/ 4, ball.ScaleAt(30537 + beatDuration / 2).X, 0.06f);
            ball.Fade(startTime, endTime, 0.8f, 0.8f);


        }

        void CreateBounce(OsbSprite sprite, int start, float beats, float maxscale, float endscale)
        {
            var beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            double middle = start + (beatDuration * beats) / 2;
            double end = start + beatDuration * beats;
            // Log(end - start / 2);
            sprite.Scale(OsbEasing.OutCirc, start, middle, sprite.ScaleAt(start).X, maxscale);
            sprite.Scale(OsbEasing.InCirc, middle, end, maxscale, endscale);
        }
    }
}
