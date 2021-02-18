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
using StorybrewCommon.Storyboarding3d;

namespace StorybrewScripts
{
    public class Sabi2Mae : StoryboardObjectGenerator
    {
        public string dotPath = "sb/dot.png";
        public string ballPath = "sb/ball.png";
        public override void Generate()
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)70716).BeatDuration;

            var layer = GetLayer("");
            Tsukuruzoi(layer, 70716, 87859);
        }

        void Tsukuruzoi(StoryboardLayer layer, int startTime, int endTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            bg(GetLayer("bg"), startTime, endTime);

            var middleBall = layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, 250));

            middleBall.Scale(startTime, 0.05f);
            middleBall.Move(OsbEasing.InOutBack, startTime, startTime + beatDuration, 320, 250, 320, 240);
            middleBall.Color(startTime, colorRGB(199, 123, 123));
            middleBall.Fade(OsbEasing.Out, startTime, startTime + beatDuration, 0, 1);
            middleBall.Fade(startTime + beatDuration, startTime + beatDuration * 4, 1, 1);

            //startnew 
            double newStart = startTime + beatDuration * 1.5f;
            // zoomTime start
            double zoomStart = startTime + beatDuration * 2f;

            middleBall.Color(newStart, colorRGB(109, 209, 204));
            var balls = new OsbSprite[]
            {
                // ここは小さい
                layer.CreateSprite(ballPath, OsbOrigin.Centre),
                layer.CreateSprite(ballPath, OsbOrigin.Centre),
                layer.CreateSprite(ballPath, OsbOrigin.Centre),

                // ここから大きい
                layer.CreateSprite(ballPath, OsbOrigin.Centre),
                layer.CreateSprite(ballPath, OsbOrigin.Centre),
                layer.CreateSprite(ballPath, OsbOrigin.Centre)
            };

            var firstBallPosses = placeInRing(balls.Length, 65);
            var secondBallPosses = placeInRing(balls.Length, 90);
            var thirdBallPosses = placeInRing(balls.Length, 130);

            for (int i = 0; i < balls.Length; i += 2)
            {
                double ballStart = newStart + (beatDuration * ((i / 2f) / 8f));
                double ballEnd = newStart + (beatDuration * ((i / 2f) / 8f)) + beatDuration / 2f;
                balls[i].Move(OsbEasing.OutBack, ballStart, ballEnd, new Vector2(320, 240), firstBallPosses[i]);
                balls[i].Color(ballStart, colorRGB(189, 222, 118));
                balls[i].Fade(ballStart, ballStart + beatDuration * 4f, 1, 1);
                balls[i].Scale(ballStart, 0.02f);

                balls[i].Move(OsbEasing.OutQuad, zoomStart, zoomStart + beatDuration * 0.5f, balls[i].PositionAt(zoomStart), thirdBallPosses[i]);
            }

            // zoom in
            middleBall.Scale(OsbEasing.OutQuad, zoomStart, zoomStart + beatDuration * 0.5f, middleBall.ScaleAt(zoomStart).X, middleBall.ScaleAt(zoomStart).X * 1.3f);
        }

        Vector2[] placeInRing(int AmountPlaces, int LengthFromCentre)
        {
            Vector2[] positions = new Vector2[AmountPlaces];
            for (int i = 0; i < AmountPlaces; i++)
            {
                float degToChng = (360 / AmountPlaces);
                float y = LengthFromCentre * (float)Math.Sin(MathHelper.DegreesToRadians(degToChng * i));
                float x = LengthFromCentre * (float)Math.Cos(MathHelper.DegreesToRadians(degToChng * i));
                float realY = 240 - y;
                float realX = 320 - x;

                positions[i] = new Vector2(realX, realY);
            }

            return positions;
        }

        void bg(StoryboardLayer layer, int startTime, int endTime)
        {
            var bg = layer.CreateSprite(dotPath, OsbOrigin.Centre);

            bg.Color(startTime, colorRGB(240, 240, 240));
            bg.ScaleVec(startTime, endTime, 854, 480, 854, 480);
        }

        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
