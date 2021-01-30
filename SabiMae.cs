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
    public class SabiMae : StoryboardObjectGenerator
    {
        string dotPath = "sb/dot.png";
        string ballPath = "sb/ball.png";
        string roundRectPath = "sb/roundRect.png";
        string borderPath = "sb/border.png";
        string circlePath = "sb/circle.png";
        int lastTime = 35359;
        int realStartTime = 31073;
        public override void Generate()
        {

            ThingBruh(31073, lastTime);

        }

        void ThingBruh(int startTime, int endTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            var layer = GetLayer("");

            var Back = layer.CreateSprite(dotPath, OsbOrigin.Centre);
            Back.Scale(startTime, endTime, 900, 900);
            Back.Color(startTime, Color4.White);

            var centerBall = layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, -50));
            centerBall.MoveY(OsbEasing.OutBack, startTime, startTime + beatDuration * 1.5f, -50, 240);
            centerBall.Scale(startTime, 0.1f);
            centerBall.Color(startTime, colorRGB(247, 199, 67));

            double scaleDownTime = startTime + beatDuration;
            centerBall.Scale(OsbEasing.InQuint, scaleDownTime, scaleDownTime + beatDuration, 0.1f, 0.03f);

            centerBall.Fade(startTime, scaleDownTime + beatDuration, 1, 1);

            double ChnageYellow = scaleDownTime + beatDuration;
            Back.Color(ChnageYellow, colorRGB(247, 199, 67));

            OsbSprite[] splitBalls = new OsbSprite[4];
            for (int i = 0; i < splitBalls.Length; i++)
            {
                splitBalls[i] = layer.CreateSprite(ballPath, OsbOrigin.Centre);

                float y = 90 * (float)Math.Sin(MathHelper.DegreesToRadians(90 * i));
                float x = 90 * (float)Math.Cos(MathHelper.DegreesToRadians(90 * i));
                float realY = 240 - y;
                float realX = 320 - x;

                splitBalls[i].Move(OsbEasing.OutQuint, ChnageYellow, ChnageYellow + beatDuration, 320, 240, realX, realY);
                splitBalls[i].Scale(ChnageYellow, 0.015f);
                splitBalls[i].Fade(ChnageYellow, 1);
                splitBalls[i].Fade(33216, 0);

                float nRadius = 90.0f;
                double goingInTime = ChnageYellow + beatDuration * 1f;
                double timePerMove = beatDuration * 1f / 10;
                float degreeChangePerMove = 20;
                float degree = 0;
                for (int j = 0; j < 15; j++)
                {
                    float y2 = nRadius * (float)Math.Sin(MathHelper.DegreesToRadians((90 * i) + degree));
                    float x2 = nRadius * (float)Math.Cos(MathHelper.DegreesToRadians((90 * i) + degree));
                    float nextY = 240 - y2;
                    float nextX = 320 - x2;

                    splitBalls[i].Move(OsbEasing.None, goingInTime, goingInTime + timePerMove, splitBalls[i].PositionAt(goingInTime).X, splitBalls[i].PositionAt(goingInTime).Y, nextX, nextY);

                    goingInTime += timePerMove;
                    degree -= degreeChangePerMove;
                    if (j >= 1)
                    {
                        nRadius *= 0.7f;
                    }
                }
            }

            Back.Color(33216, colorRGB(82, 200, 247));

            var allHaouds = CreateHadou(33216, 3, beatDuration * 3 / 4, colorRGB(82, 200, 247), Color4.White);
            FadeMultiOne(allHaouds, 34287, 0);

            Back.Color(34287, colorRGB(247, 192, 82));

            var roundRect = layer.CreateSprite(roundRectPath, OsbOrigin.Centre);

            roundRect.Scale(OsbEasing.OutElastic, 34287, 34287 + beatDuration * 3 / 4, 0.1f, 0.5f);
            roundRect.Rotate(OsbEasing.InBack, 34287, 35359, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(360));


            //Next thing
            BruhNextThing(startTime);

            // last thign
            CreateLastThing(40716);
        }

        void BruhNextThing(int startTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            var layer = GetLayer("bruhNext");
            OsbSprite[] threeBalls = new OsbSprite[]
            {
                layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, 40)),
                layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, 440)),
                layer.CreateSprite(ballPath, OsbOrigin.Centre)
            };
            for (int i = 0; i < 3; i++)
            {
                threeBalls[i].Color(35225, 35225 + beatDuration * 1f, colorRGB(118, 214, 201), colorRGB(118, 214, 201));
                threeBalls[i].Fade(OsbEasing.OutQuart, 35225, 35225 + beatDuration, 0, 1);
                threeBalls[i].ScaleVec(35225, 0.02f, 0.02f);
            }
            var ballBitmap = GetMapsetBitmap(ballPath);

            for (int i = 0; i < 2; i++)
            {
                double degree = 90;
                double realDegree = 90;
                double timiii = 35225;
                double timePerMove = (beatDuration * 1f) / 10f;
                double bruhhingPer = 90 / 10f;
                double okY = 0;

                for (int j = 0; j < 10; j++)
                {
                    float y = 200 * (float)Math.Sin(MathHelper.DegreesToRadians((180 * i) + degree));
                    float x = 200 * (float)Math.Cos(MathHelper.DegreesToRadians((180 * i) + degree));
                    float realY = 240 - y;
                    float realX = 320 - x;

                    threeBalls[i].Move(timiii, timiii + timePerMove, threeBalls[i].PositionAt(timiii).X, threeBalls[i].PositionAt(timiii).Y, realX, realY);

                    okY = 1 - Math.Pow(1f - (j / 10f), 4f);
                    degree = 90 * okY + 90f;
                    timiii += timePerMove;
                }


                // next (same as below) 

                var sq = layer.CreateSprite(dotPath, OsbOrigin.Centre, threeBalls[i].PositionAt(35894 + beatDuration));

                if (i == 0)
                {
                    threeBalls[i].Move(OsbEasing.OutQuint, 35894, 35894 + beatDuration, threeBalls[i].PositionAt(35894), new Vector2(threeBalls[i].PositionAt(35894).X + 90, threeBalls[i].PositionAt(35894).Y));
                    sq.Move(OsbEasing.OutQuint, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, threeBalls[i].PositionAt(35894 + beatDuration * 3f), new Vector2(threeBalls[i].PositionAt(35894 + beatDuration * 3f).X + 40, threeBalls[i].PositionAt(35894 + beatDuration * 3f).Y));
                    sq.Move(OsbEasing.InQuart, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, sq.PositionAt(35894 + beatDuration * 3.5f), new Vector2(sq.PositionAt(35894 + beatDuration * 3.5f).X + 40, sq.PositionAt(35894 + beatDuration * 3.5f).Y));
                    sq.Move(OsbEasing.InQuart, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, sq.PositionAt(35894 + beatDuration * 4.5f), new Vector2(sq.PositionAt(35894 + beatDuration * 4.5f).X - 120, sq.PositionAt(35894 + beatDuration * 4.5f).Y));
                }
                else if (i == 1)
                {
                    threeBalls[i].Move(OsbEasing.OutQuint, 35894, 35894 + beatDuration, threeBalls[i].PositionAt(35894), new Vector2(threeBalls[i].PositionAt(35894).X - 90, threeBalls[i].PositionAt(35894).Y));
                    sq.Move(OsbEasing.OutQuint, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, threeBalls[i].PositionAt(35894 + beatDuration * 3f), new Vector2(threeBalls[i].PositionAt(35894 + beatDuration * 3f).X - 40, threeBalls[i].PositionAt(35894 + beatDuration * 3f).Y));
                    sq.Move(OsbEasing.InQuart, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, sq.PositionAt(35894 + beatDuration * 3.5f), new Vector2(sq.PositionAt(35894 + beatDuration * 3.5f).X - 40, sq.PositionAt(35894 + beatDuration * 3.5f).Y));
                    sq.Move(OsbEasing.InQuart, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, sq.PositionAt(35894 + beatDuration * 4.5f), new Vector2(sq.PositionAt(35894 + beatDuration * 4.5f).X + 120, sq.PositionAt(35894 + beatDuration * 4.5f).Y));

                }

                sq.ScaleVec(OsbEasing.OutExpo, 35894 + beatDuration, 35894 + beatDuration * 1.5f, 0, ballBitmap.Height * threeBalls[i].ScaleAt(35894).X, 140, ballBitmap.Height * threeBalls[i].ScaleAt(35894).X);
                sq.Color(35894 + beatDuration, 39644, colorRGB(118, 214, 201), colorRGB(118, 214, 201));
            }

            // next

            OsbSprite[] middleBall = new OsbSprite[2];
            middleBall[0] = threeBalls[2];
            middleBall[1] = layer.CreateSprite(ballPath, OsbOrigin.Centre);

            ScaleTwo(middleBall, OsbEasing.OutBack, 35894, 35894 + beatDuration / 2, threeBalls[2].ScaleAt(35894).X, threeBalls[2].ScaleAt(35894).X, 0.4f, 0.4f, 0.9f);

            middleBall[1].Color(35894, 39644, Color4.White, Color4.White);

            var middleInnerBall = layer.CreateSprite(ballPath, OsbOrigin.Centre);
            middleInnerBall.Color(35894, 35894 + beatDuration * 2f, colorRGB(118, 214, 201), colorRGB(118, 214, 201));
            middleInnerBall.Scale(OsbEasing.OutBack, 35894, 35894 + beatDuration / 4, 0.001f, 0.03f);

            var sq1 = layer.CreateSprite(dotPath, OsbOrigin.Centre, middleInnerBall.PositionAt(35894 + beatDuration));
            var sq2 = layer.CreateSprite(dotPath, OsbOrigin.Centre, middleInnerBall.PositionAt(35894 + beatDuration));

            sq1.ScaleVec(OsbEasing.OutBack, 35894 + beatDuration, 35894 + beatDuration * 1.5f, 0, ballBitmap.Height * middleInnerBall.ScaleAt(35894 + beatDuration).X, 120, ballBitmap.Height * middleInnerBall.ScaleAt(35894 + beatDuration).X);
            sq1.Color(35894 + beatDuration, 39644, colorRGB(118, 214, 201), colorRGB(118, 214, 201));
            sq1.Rotate(35894 + beatDuration, MathHelper.DegreesToRadians(45));
            sq2.ScaleVec(OsbEasing.OutBack, 35894 + beatDuration, 35894 + beatDuration * 1.5f, 0, ballBitmap.Height * middleInnerBall.ScaleAt(35894 + beatDuration).X, 120, ballBitmap.Height * middleInnerBall.ScaleAt(35894 + beatDuration).X);
            sq2.Color(35894 + beatDuration, 39644, colorRGB(118, 214, 201), colorRGB(118, 214, 201));
            sq2.Rotate(35894 + beatDuration, MathHelper.DegreesToRadians(45 + 90));

            ScaleTwo(middleBall, OsbEasing.OutQuart, 35894 + beatDuration, 35894 + beatDuration * 1.5f, middleBall[0].ScaleAt(35894 + beatDuration).X, middleBall[0].ScaleAt(35894 + beatDuration).X, middleBall[0].ScaleAt(35894 + beatDuration).X * 1.2f, middleBall[0].ScaleAt(35894 + beatDuration).X * 1.2f, 0.92f);
            ScaleTwo(middleBall, OsbEasing.InQuart, 35894 + beatDuration * 1.5f, 35894 + beatDuration * 2f, middleBall[0].ScaleAt(35894 + beatDuration * 1.5f).X, middleBall[0].ScaleAt(35894 + beatDuration * 1.5f).X, middleBall[0].ScaleAt(35894 + beatDuration * 1.5f).X * 0.8f, middleBall[0].ScaleAt(35894 + beatDuration * 1.5f).X * 0.8f, 0.92f);

            var border = layer.CreateSprite(borderPath, OsbOrigin.Centre);
            border.Scale(OsbEasing.OutQuart, 35894 + beatDuration, 35894 + beatDuration * 1.5f, 0.1f, (ballBitmap.Height * middleBall[0].ScaleAt(35894 + beatDuration * 1.5f).X) / 100f);
            border.Color(35894 + beatDuration, 39644, colorRGB(118, 214, 201), colorRGB(118, 214, 201));

            border.Scale(OsbEasing.InQuart, 35894 + beatDuration * 1.5f, 35894 + beatDuration * 2f, border.ScaleAt(35894 + beatDuration * 1.5f).X, (ballBitmap.Height * middleBall[0].ScaleAt(35894 + beatDuration * 2f).X) / 100f);

            sq1.Rotate(OsbEasing.InQuart, 35894 + beatDuration * 1.5f, 35894 + beatDuration * 2f, sq1.RotationAt(35894 + beatDuration * 1.5f), sq1.RotationAt(35894 + beatDuration * 1.5f) + MathHelper.DegreesToRadians(90));
            sq2.Rotate(OsbEasing.InQuart, 35894 + beatDuration * 1.5f, 35894 + beatDuration * 2f, sq2.RotationAt(35894 + beatDuration * 1.5f), sq2.RotationAt(35894 + beatDuration * 1.5f) + MathHelper.DegreesToRadians(90));

            border.Rotate(OsbEasing.InQuart, 35894 + beatDuration * 1.5f, 35894 + beatDuration * 2f, 0, MathHelper.DegreesToRadians(45));

            var border2 = layer.CreateSprite(borderPath, OsbOrigin.Centre);
            border2.Scale(OsbEasing.OutBack, 35894 + beatDuration * 2f, 35894 + beatDuration * 2.5f, border.ScaleAt(35894 + beatDuration * 2f).X, border.ScaleAt(35894 + beatDuration * 2f).X * 1.15f);
            border2.Color(35894 + beatDuration * 2f, 39644, colorRGB(118, 214, 201), colorRGB(118, 214, 201));
            border2.Rotate(35894 + beatDuration * 2f, MathHelper.DegreesToRadians(45));

            var border3 = layer.CreateSprite(borderPath, OsbOrigin.Centre);
            border3.Scale(OsbEasing.OutBack, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, border2.ScaleAt(35894 + beatDuration * 2.5f).X, border2.ScaleAt(35894 + beatDuration * 2.5f).X * 1.15f);
            border3.Color(35894 + beatDuration * 3f, 39644, colorRGB(118, 214, 201), colorRGB(118, 214, 201));

            border.Rotate(OsbEasing.OutQuint, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, border.RotationAt(35894 + beatDuration * 3f), border.RotationAt(35894 + beatDuration * 3f) + MathHelper.DegreesToRadians(90));
            border2.Rotate(OsbEasing.OutQuint, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, border.RotationAt(35894 + beatDuration * 3f), border.RotationAt(35894 + beatDuration * 3.5f));
            border3.Rotate(OsbEasing.OutQuint, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, border2.RotationAt(35894 + beatDuration * 3f), border2.RotationAt(35894 + beatDuration * 3.5f));

            sq1.Rotate(OsbEasing.OutQuint, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, sq1.RotationAt(35894 + beatDuration * 3f), sq1.RotationAt(35894 + beatDuration * 3f) + MathHelper.DegreesToRadians(90));
            sq2.Rotate(OsbEasing.OutQuint, 35894 + beatDuration * 3f, 35894 + beatDuration * 3.5f, sq2.RotationAt(35894 + beatDuration * 3f), sq2.RotationAt(35894 + beatDuration * 3f) + MathHelper.DegreesToRadians(90));

            border.Scale(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, border.ScaleAt(35894 + beatDuration * 3.5f).X, border.ScaleAt(35894 + beatDuration * 3.5f).X * 1.2f);
            border2.Scale(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, border2.ScaleAt(35894 + beatDuration * 3.5f).X, border2.ScaleAt(35894 + beatDuration * 3.5f).X * 1.2f);
            border3.Scale(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, border3.ScaleAt(35894 + beatDuration * 3.5f).X, border3.ScaleAt(35894 + beatDuration * 3.5f).X * 1.2f);

            border.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, border.RotationAt(35894 + beatDuration * 3.5f), border.RotationAt(35894 + beatDuration * 3.5f) + MathHelper.DegreesToRadians(140));
            border2.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, border.RotationAt(35894 + beatDuration * 3.5f), border.RotationAt(35894 + beatDuration * 3.5f) + MathHelper.DegreesToRadians(140));
            border3.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, border.RotationAt(35894 + beatDuration * 3.5f), border.RotationAt(35894 + beatDuration * 3.5f) + MathHelper.DegreesToRadians(140));
            sq1.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, sq1.RotationAt(35894 + beatDuration * 3.5f), sq1.RotationAt(35894 + beatDuration * 3.5f) + MathHelper.DegreesToRadians(70));
            sq2.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 3.5f, 35894 + beatDuration * 4.5f, sq2.RotationAt(35894 + beatDuration * 3.5f), sq2.RotationAt(35894 + beatDuration * 3.5f) + MathHelper.DegreesToRadians(70));

            border.Scale(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, border.ScaleAt(35894 + beatDuration * 4.5f).X, border.ScaleAt(35894 + beatDuration * 4.5f).X * 0.4f);
            border2.Scale(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, border2.ScaleAt(35894 + beatDuration * 4.5f).X, border2.ScaleAt(35894 + beatDuration * 4.5f).X * 0.4f);
            border3.Scale(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, border3.ScaleAt(35894 + beatDuration * 4.5f).X, border3.ScaleAt(35894 + beatDuration * 4.5f).X * 0.4f);
            ScaleTwo(middleBall, OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, middleBall[0].ScaleAt(35894 + beatDuration * 4.5f).X, middleBall[0].ScaleAt(35894 + beatDuration * 4.5f).X, middleBall[0].ScaleAt(35894 + beatDuration * 4.5f).X * 0.4f, middleBall[0].ScaleAt(35894 + beatDuration * 4.5f).X * 0.4f, 0.92f);
            sq1.ScaleVec(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, sq1.ScaleAt(35894 + beatDuration * 4.5f).X, sq1.ScaleAt(35894 + beatDuration * 4.5f).Y, sq1.ScaleAt(35894 + beatDuration * 4.5f).X * 0.5f, sq1.ScaleAt(35894 + beatDuration * 4.5f).Y * 0.5f);
            sq2.ScaleVec(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, sq2.ScaleAt(35894 + beatDuration * 4.5f).X, sq2.ScaleAt(35894 + beatDuration * 4.5f).Y, sq2.ScaleAt(35894 + beatDuration * 4.5f).X * 0.5f, sq2.ScaleAt(35894 + beatDuration * 4.5f).Y * 0.5f);



            border.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, border.RotationAt(35894 + beatDuration * 4.5f), border.RotationAt(35894 + beatDuration * 4.5f) - MathHelper.DegreesToRadians(70 + 180));
            border2.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, border.RotationAt(35894 + beatDuration * 4.5f), border.RotationAt(35894 + beatDuration * 4.5f) - MathHelper.DegreesToRadians(70 + 180));
            border3.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, border.RotationAt(35894 + beatDuration * 4.5f), border.RotationAt(35894 + beatDuration * 4.5f) - MathHelper.DegreesToRadians(70 + 180));
            sq1.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, sq1.RotationAt(35894 + beatDuration * 4.5f), sq1.RotationAt(35894 + beatDuration * 4.5f) - MathHelper.DegreesToRadians(70 + 180));
            sq2.Rotate(OsbEasing.InQuint, 35894 + beatDuration * 4.5f, 35894 + beatDuration * 6.75f, sq2.RotationAt(35894 + beatDuration * 4.5f), sq2.RotationAt(35894 + beatDuration * 4.5f) - MathHelper.DegreesToRadians(70 + 180));

        }

        void CreateLastThing(int startTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            var layer = GetLayer("LastBruh");

            var circle = new OsbSprite[3];

            for (int i = 0; i < circle.Length; i++)
            {
                circle[i] = layer.CreateSprite(circlePath, OsbOrigin.Centre);

                circle[i].Color(startTime, 45001, colorRGB(41, 41, 41), colorRGB(41, 41, 41));
                circle[i].Scale(OsbEasing.OutBack, startTime, startTime + beatDuration, 0, 0.8f - (0.1f * i));

                circle[i].Scale(OsbEasing.OutBack, startTime + beatDuration * (4 + 0.25f * i), startTime + beatDuration * (4.5f + 0.25f * i), 0.8f - (0.1f * i), (0.8f - (0.1f * i)) * 1.6f);
                circle[i].Scale(OsbEasing.In, startTime + beatDuration * (4.25f + 7 * 0.25f), startTime + beatDuration * (4.25f + 7 * 0.25f + 1f), (0.8f - (0.1f * i)) * 1.6f, 0);
                
            }

            for (int i = -1; i < 2; i++)
            {
                if (i != 0)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var sq = layer.CreateSprite(dotPath, OsbOrigin.CentreLeft);

                        sq.MoveX(OsbEasing.Out, startTime + beatDuration, startTime + beatDuration * 2, 320 + i * -7 + i * 50 + i * 20 * j, 320 + i * 50 + i * 20 * j);
                        sq.Color(startTime + beatDuration, startTime + beatDuration * 4, colorRGB(41, 41, 41), colorRGB(41, 41, 41));
                        sq.Scale(startTime + beatDuration, 8);
                        sq.Rotate(startTime + beatDuration, MathHelper.DegreesToRadians(45));
                        sq.Fade(startTime + beatDuration, 0.8f);

                        sq.MoveX(OsbEasing.InQuint, startTime + beatDuration * 2, startTime + beatDuration * 4, 320 + i * 50 + i * 20 * j, 320);
                    }
                }


            }

            for (int i = -1; i < 2; i++)
            {
                if (i != 0)
                {
                    var back = layer.CreateSprite(dotPath, OsbOrigin.CentreLeft, new Vector2(320 + 550 * i, 240 + 100 * i));
                    if (i == 1)
                    {
                        back = layer.CreateSprite(dotPath, OsbOrigin.CentreRight, new Vector2(320 + 550 * i, 240 + 100 * i));
                    }

                    back.ScaleVec(startTime + beatDuration * 2, 300, 600);
                    back.Color(startTime + beatDuration * 2, startTime + beatDuration * 8, colorRGB(41, 41, 41), colorRGB(41, 41, 41));

                    back.Rotate(startTime + beatDuration * 2, startTime + beatDuration * 4, MathHelper.DegreesToRadians(20), MathHelper.DegreesToRadians(15));
                    back.Fade(startTime + beatDuration * 2, startTime + beatDuration * 2.5f, 0, 0.8f);

                    back.ScaleVec(OsbEasing.InCubic, startTime + beatDuration * (4.25f + 7 * 0.25f), startTime + beatDuration * (4.25f + 7 * 0.25f + 1f), 300, 600, 557.15f, 600);
                }
            }

            List<Vector2> lastPosses = new List<Vector2>();
            Splatter(new Vector2(320, 240), 0, startTime, beatDuration, lastPosses);

            
        }

        void Splatter(Vector2 center, int e, int startTime, double beatDuration, List<Vector2> lastPosses)
        {
            if (e == 8)
            {
                lastPosses.Add(center);
                return;
            }
            Vector2[] endYepPos = new Vector2[10];
            var layer = GetLayer("");
            for (int i = 0; i < 10 / (e+1) ; i++)
            {
                // 最初の円
                var cir = layer.CreateSprite(ballPath, OsbOrigin.Centre);
                // 終わりの場所
                endYepPos[i] = new Vector2(center.X + Random(-40, 40), center.Y + Random(-40, 40));

                cir.Move(OsbEasing.Out, startTime + beatDuration * (4 + e * 0.25f), startTime + beatDuration * (4.25f + e * 0.25f), center, endYepPos[i]);
                cir.Color(startTime + beatDuration * (4 + e * 0.25f), Color4.FromHsv(new Vector4(0,0, 0f, 1)));
                cir.Scale(startTime + beatDuration * (4 + e * 0.25f), Random(0.003f, 0.012f) * Math.Pow(0.9, e));
                cir.Fade(startTime + beatDuration * (4 + e * 0.25f), 1 * Math.Pow(0.85, e));

                if (e == 7)
                {
                    cir.Move(OsbEasing.InQuint, startTime + beatDuration * (4.25f + e * 0.25f), startTime + beatDuration * (4.25f + e * 0.25f + 1f), endYepPos[i], new Vector2(320, 240));
                }
                Splatter(endYepPos[i], e+1, startTime, beatDuration, lastPosses);
            }
        }

        OsbSprite[] CreateHadou(double start, int amount, double timebetween, Color4 innerColor, Color4 outerColor, float x = 320, float y = 240)
        {
            var layer = GetLayer("");
            int wierdiiii = amount;
            OsbSprite[] wholeArray = new OsbSprite[amount * 2];
            for (int i = 0; i < amount; i++)
            {
                OsbSprite[] hadous = new OsbSprite[]
                {
                    layer.CreateSprite(ballPath, OsbOrigin.Centre),
                    layer.CreateSprite(ballPath,OsbOrigin.Centre)
                };
                double timii = start;
                float startScale = 0.1f;
                float scaleMultiplier = 2f;

                for (int j = 0; j < wierdiiii; j++)
                {
                    ScaleTwo(hadous, OsbEasing.OutExpo, timii, timii + timebetween, startScale, startScale, startScale * scaleMultiplier, startScale * scaleMultiplier, 0.98f);

                    startScale *= scaleMultiplier;
                    timii += timebetween;
                }

                FadeMultiOne(hadous, start, 1);
                ColorInnerOuter(hadous, start, outerColor, innerColor);

                start += timebetween;
                wierdiiii--;

                hadous.CopyTo(wholeArray, i * 2);
            }

            return wholeArray;
        }

        void ScaleTwo(OsbSprite[] sprites, OsbEasing easing, double start, double end, float startScaleX, float startScaleY, float endScaleX, float endScaleY, float scaleDiff)
        {
            sprites[0].ScaleVec(easing, start, end, startScaleX, startScaleY, endScaleX, endScaleY);
            sprites[1].ScaleVec(easing, start, end, startScaleX * scaleDiff, startScaleY * scaleDiff, endScaleX * scaleDiff, endScaleY * scaleDiff);
        }

        void ColorInnerOuter(OsbSprite[] sprites, double start, Color4 outerColor, Color4 innerColor)
        {
            sprites[0].Color(start, outerColor);
            sprites[1].Color(start, innerColor);
        }

        void RotateMulti(OsbSprite[] sprites, OsbEasing easing, double start, double end, float startdegree, float enddegree)
        {
            foreach (var item in sprites)
            {
                item.Rotate(easing, start, end, MathHelper.DegreesToRadians(startdegree), MathHelper.DegreesToRadians(enddegree));
            }
        }

        void FadeMulti(OsbSprite[] sprites, OsbEasing easing, double start, double end, float startFade, float endFade)
        {
            foreach (var item in sprites)
            {
                item.Fade(easing, start, end, startFade, endFade);
            }
        }

        void FadeMultiOne(OsbSprite[] sprites, double start, float startFade)
        {
            foreach (var item in sprites)
            {
                item.Fade(start, startFade);
            }
        }

        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
