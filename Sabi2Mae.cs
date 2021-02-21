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
        string circlePath = "sb/circle.png";
        public override void Generate()
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)70716).BeatDuration;

            var layer = GetLayer("");
            Tsukuruzoi(layer, 70716, 87859);
        }

        void Tsukuruzoi(StoryboardLayer layer, int startTime, int endTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            bg(GetLayer("bg"), startTime, endTime, colorRGB(240, 240, 240));

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
            // t1 start
            var t1start = startTime + beatDuration * 5f;

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
            var third2BallPosses = placeInRing(balls.Length, 140);
            var thirdBallPosses = placeInRing(balls.Length, 110);

            var lastBalls = placeInRing(balls.Length, 80);
            var last1Balls = placeInRing(balls.Length, 50);
            var last2Balls = placeInRing(balls.Length, 20);

            for (int i = 0; i < balls.Length; i += 2)
            {
                double ballStart = newStart + (beatDuration * ((i / 2f) / 8f));
                double ballEnd = newStart + (beatDuration * ((i / 2f) / 8f)) + beatDuration / 2f;
                balls[i].Move(OsbEasing.OutBack, ballStart, ballEnd, new Vector2(320, 240), firstBallPosses[i]);
                balls[i].Color(ballStart, colorRGB(189, 222, 118));
                balls[i].Fade(ballStart, ballStart + beatDuration * 4f, 1, 1);
                balls[i].Scale(ballStart, 0.02f);

                balls[i].Move(OsbEasing.OutQuad, zoomStart + beatDuration * 0.75f, zoomStart + beatDuration * 1.25f, balls[i].PositionAt(zoomStart), thirdBallPosses[i]);
                balls[i].Color(startTime + beatDuration * 3.5f, colorRGB(214, 158, 109));
                balls[i].Scale(OsbEasing.OutBack, startTime + beatDuration * 3.5f, startTime + beatDuration * 4f, balls[i].ScaleAt(startTime + beatDuration * 3.5f).X, balls[i].ScaleAt(startTime + beatDuration * 3.5f).X * 1.5f);

                // t1 shit
                balls[i].Move(OsbEasing.OutBack, t1start, t1start + beatDuration * (1 / 3f), balls[i].PositionAt(t1start), lastBalls[i]);
                balls[i].Move(OsbEasing.OutBack, t1start + beatDuration * (1 / 3f), t1start + beatDuration * (2 / 3f), lastBalls[i], last1Balls[i]);
                balls[i].Move(OsbEasing.OutBack, t1start + beatDuration * (2 / 3f), t1start + beatDuration * (3 / 3f), last1Balls[i], last2Balls[i]);
            }

            // zoom in
            middleBall.Scale(OsbEasing.OutQuad, zoomStart, zoomStart + beatDuration * 0.5f, middleBall.ScaleAt(zoomStart).X, middleBall.ScaleAt(zoomStart).X * 1.3f);
            middleBall.Scale(OsbEasing.OutExpo, zoomStart + beatDuration * 0.75f, zoomStart + beatDuration * 1.25f, middleBall.ScaleAt(zoomStart + beatDuration * 0.75f).X, middleBall.ScaleAt(zoomStart + beatDuration * 0.75f).X * 1.4f);

            for (int i = 1; i < balls.Length; i += 2)
            {
                double ballStart = zoomStart;
                double ballEnd = zoomStart + beatDuration * (3 / 4f);
                balls[i].Move(OsbEasing.OutBack, ballStart, ballEnd, new Vector2(320, 240), firstBallPosses[i]);
                balls[i].Color(ballStart, colorRGB(189, 222, 118));
                balls[i].Fade(ballStart, ballStart + beatDuration * 4f, 1, 1);
                balls[i].Scale(ballStart, 0.02f);

                balls[i].Move(OsbEasing.OutQuad, zoomStart + beatDuration * 0.75f, zoomStart + beatDuration * 1.25f, balls[i].PositionAt(zoomStart + beatDuration * 0.75f), third2BallPosses[i]);
                balls[i].Color(startTime + beatDuration * 4f, colorRGB(247, 168, 168));
                balls[i].Scale(OsbEasing.OutBack, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, balls[i].ScaleAt(startTime + beatDuration * 3.5f).X, balls[i].ScaleAt(startTime + beatDuration * 3.5f).X * 0.7f);

                // t1 things
                balls[i].Move(OsbEasing.OutBack, t1start, t1start + beatDuration * (1 / 3f), balls[i].PositionAt(t1start), lastBalls[i]);
                balls[i].Move(OsbEasing.OutBack, t1start + beatDuration * (1 / 3f), t1start + beatDuration * (2 / 3f), lastBalls[i], last1Balls[i]);
                balls[i].Move(OsbEasing.OutBack, t1start + beatDuration * (2 / 3f), t1start + beatDuration * (3 / 3f), last1Balls[i], last2Balls[i]);
            }

            var sq = layer.CreateSprite(dotPath, OsbOrigin.Centre);

            sq.Rotate(startTime + beatDuration * 4f, MathHelper.DegreesToRadians(45));
            sq.Color(startTime + beatDuration * 4f, colorRGB(0, 0, 0));
            sq.Fade(startTime + beatDuration * 4f, t1start + beatDuration * (3 / 3f), 0.8f, 0.8f);
            sq.Scale(startTime + beatDuration * 4f, 40);

            // t1 start
            sq.Scale(OsbEasing.OutBack, t1start, t1start + beatDuration * (1 / 3f), 40, 32);
            sq.Scale(OsbEasing.OutBack, t1start + beatDuration * (1 / 3f), t1start + beatDuration * (2 / 3f), 32, 27);
            sq.Scale(OsbEasing.OutBack, t1start + beatDuration * (2 / 3f), t1start + beatDuration * (3 / 3f), 27, 23);

            // new ok NEEEEEEEEEEEEEEEEEEEEEEEEW
            double secondStart = t1start + beatDuration;
            bg(layer, secondStart, secondStart + beatDuration * 6f, colorRGB(168, 176, 247));

            var ballhaha = layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, -30));

            ballhaha.MoveY(OsbEasing.OutBack, secondStart, secondStart + beatDuration * 1.5f, -30, 240);
            ballhaha.Scale(secondStart, 0.1f);
            ballhaha.Scale(OsbEasing.InQuad, secondStart + beatDuration * 1f, secondStart + beatDuration * 2f, 0.1f, 0);

            OsbSprite[] splats = Splatter1(new Vector2(320, 240), secondStart + beatDuration * 2f, 10, beatDuration);

            for (int i = 0; i < splats.Length; i++)
            {
                splats[i].Move(OsbEasing.InQuart, secondStart + beatDuration * 2.5f, secondStart + beatDuration * 3.5f, splats[i].PositionAt(secondStart + beatDuration * 2.5f), new Vector2(splats[i].PositionAt(secondStart + beatDuration * 2.5f).X, 430));
            }

            var sq1 = new OsbSprite[]
            {
                layer.CreateSprite(dotPath, OsbOrigin.Centre),
                layer.CreateSprite(dotPath, OsbOrigin.Centre)
            };

            var circle1s = new OsbSprite[]
            {
                layer.CreateSprite(ballPath, OsbOrigin.Centre),
                layer.CreateSprite(ballPath, OsbOrigin.Centre)
            };

            sq1[0].Scale(secondStart + beatDuration * 3.5f, 40);
            sq1[1].Scale(secondStart + beatDuration * 3.5f, 40 * 0.96f);
            ColorInnerOuter(sq1, secondStart + beatDuration * 3.5f, Color4.White, colorRGB(168, 176, 247));
            RotateMulti(sq1, secondStart + beatDuration * (3 + (3 / 6f)), 0);
            FadeMultiOne(sq1, secondStart + beatDuration * (3 + (3 / 6f)), 1);
            FadeMultiOne(sq1, secondStart + beatDuration * (3 + (4 / 6f)), 0);

            RotateMulti(sq1, secondStart + beatDuration * (3 + (5 / 6f)), 45);
            FadeMultiOne(sq1, secondStart + beatDuration * (3 + (5 / 6f)), 1);
            FadeMultiOne(sq1, secondStart + beatDuration * (3 + (6 / 6f)), 0);

            circle1s[0].Scale(secondStart + beatDuration * (3 + (4 / 6f)), 0.07f);
            circle1s[1].Scale(secondStart + beatDuration * (3 + (4 / 6f)), 0.07f * 0.96f);
            ColorInnerOuter(circle1s, secondStart + beatDuration * (3 + (4 / 6f)), Color4.White, colorRGB(168, 176, 247));
            FadeMultiOne(circle1s, secondStart + beatDuration * (3 + (4 / 6f)), 1);
            FadeMultiOne(circle1s, secondStart + beatDuration * (3 + (5 / 6f)), 0);

            var allHaouds = CreateHadou(layer, secondStart + beatDuration * 4, 3, beatDuration * 3 / 4, colorRGB(168, 176, 247), Color4.White);
            FadeMultiOne(allHaouds, secondStart + beatDuration * 6, 0);

            // new before next transition
            double timeBytrans2 = secondStart + beatDuration * 6;
            bg(layer, secondStart + beatDuration * 6, secondStart + beatDuration * 8f, colorRGB(239, 240, 187));

            var bounceCir = layer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(320, 225));
            bounceCir.MoveY(OsbEasing.OutBounce, timeBytrans2, timeBytrans2 + beatDuration * 1, 225, 240);
            bounceCir.Scale(timeBytrans2, 0.07f);
            bounceCir.Fade(timeBytrans2 + beatDuration * 2, 1);

            // after second transition
            double secTransTime = timeBytrans2 + beatDuration * 2;
            bg(layer, secTransTime, secTransTime + beatDuration * 8f, colorRGB(166, 161, 154));

            var notLastBalls = new OsbSprite[3];

            var TrafficLightsLayer = GetLayer("trafic");

            // from left
            notLastBalls[0] = TrafficLightsLayer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(100, 240));
            notLastBalls[0].Scale(secTransTime, 0.075f);
            notLastBalls[0].Fade(OsbEasing.In, secTransTime, secTransTime + beatDuration / 2f, 0, 1);
            notLastBalls[0].Fade(secTransTime + beatDuration / 2f, secTransTime + beatDuration * 4f, 1, 1);
            notLastBalls[0].MoveX(OsbEasing.InExpo, secTransTime, secTransTime + beatDuration / 2f, 100, 290);

            // from right
            notLastBalls[1] = TrafficLightsLayer.CreateSprite(ballPath, OsbOrigin.Centre, new Vector2(540, 240));
            notLastBalls[1].Scale(secTransTime + beatDuration, 0.075f);
            notLastBalls[1].Fade(OsbEasing.In, secTransTime + beatDuration, secTransTime + beatDuration + beatDuration / 2f, 0, 1);
            notLastBalls[1].Fade(secTransTime + beatDuration + beatDuration / 2f, secTransTime + beatDuration * 4f, 1, 1);
            notLastBalls[1].MoveX(OsbEasing.InExpo, secTransTime + beatDuration, secTransTime + beatDuration + beatDuration / 2f, 540, 350);

            // in middle
            notLastBalls[2] = TrafficLightsLayer.CreateSprite(ballPath, OsbOrigin.Centre);
            notLastBalls[2].ScaleVec(OsbEasing.OutBack, secTransTime + beatDuration * 2f, secTransTime + beatDuration * 2.5f, 0, 0.075f, 0.075f, 0.075f);
            notLastBalls[2].Fade(secTransTime + beatDuration * 2f, secTransTime + beatDuration * 7f, 1, 1);

            notLastBalls[0].MoveX(OsbEasing.OutBack, secTransTime + beatDuration * 2f, secTransTime + beatDuration * 2.5f, 290, 260);
            notLastBalls[1].MoveX(OsbEasing.OutBack, secTransTime + beatDuration * 2f, secTransTime + beatDuration * 2.5f, 350, 380);

            notLastBalls[0].Color(secTransTime + beatDuration * 3f, colorRGB(242, 150, 97));
            notLastBalls[1].Color(secTransTime + beatDuration * (3f + (1f/3f)), colorRGB(65, 182, 250));
            notLastBalls[2].Color(secTransTime + beatDuration * (3f + (2f/3f)), colorRGB(70, 71, 70));

            for (int i = 0; i < notLastBalls.Length; i++)
            {
                notLastBalls[i].Color(secTransTime + beatDuration * i, Color4.White);
            }

            var splatterLeft = SplatterOneWay(notLastBalls[0].PositionAt(secTransTime + beatDuration * 4f), secTransTime + beatDuration * 4f, 6, beatDuration, new Vector2(-80, -40), new Vector2(-50, 50));
            var splatterRight = SplatterOneWay(notLastBalls[1].PositionAt(secTransTime + beatDuration * 4f), secTransTime + beatDuration * 4f, 6, beatDuration, new Vector2(40, 80), new Vector2(-50, 50));
            
            List<OsbSprite> allSplatters = new List<OsbSprite>();

            allSplatters.AddRange(splatterLeft);
            allSplatters.AddRange(splatterRight);

            for (int i = 0; i < allSplatters.Count; i++)
            {
                allSplatters[i].Move(OsbEasing.InQuad, secTransTime + beatDuration * 4.5f, secTransTime + beatDuration * 5.25f, allSplatters[i].PositionAt(secTransTime + beatDuration * 4.5f), new Vector2(320, 240));
            }
            notLastBalls[2].ScaleVec(OsbEasing.InQuart, secTransTime + beatDuration * 4.5f, secTransTime + beatDuration * 5.75f, notLastBalls[2].ScaleAt(secTransTime + beatDuration * 5.25f), new Vector2(0,0));
            var okok = SplatterOneWay(new Vector2(320, 240), secTransTime + beatDuration * 6f, 12, beatDuration, new Vector2(-80, 80), new Vector2(-80, 80));

            for (int i = 0; i < okok.Length; i++)
            {
                okok[i].Fade(secTransTime + beatDuration * 6f, secTransTime + beatDuration * 8f, 1, 0);
            }

            // the one before last
            bg(layer, 82501, 83037, colorRGB(227, 152, 173));

            // last thing
            CreateLastThing(83573);
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
            for (int i = 0; i < 10 / (e + 1); i++)
            {
                // 最初の円
                var cir = layer.CreateSprite(ballPath, OsbOrigin.Centre);
                // 終わりの場所
                endYepPos[i] = new Vector2(center.X + Random(-40, 40), center.Y + Random(-40, 40));

                cir.Move(OsbEasing.Out, startTime + beatDuration * (4 + e * 0.25f), startTime + beatDuration * (4.25f + e * 0.25f), center, endYepPos[i]);
                cir.Color(startTime + beatDuration * (4 + e * 0.25f), Color4.FromHsv(new Vector4(0, 0, 0f, 1)));
                cir.Scale(startTime + beatDuration * (4 + e * 0.25f), Random(0.003f, 0.012f) * Math.Pow(0.9, e));
                cir.Fade(startTime + beatDuration * (4 + e * 0.25f), 1 * Math.Pow(0.85, e));

                if (e == 7)
                {
                    cir.Move(OsbEasing.InQuint, startTime + beatDuration * (4.25f + e * 0.25f), startTime + beatDuration * (4.25f + e * 0.25f + 1f), endYepPos[i], new Vector2(320, 240));
                }
                Splatter(endYepPos[i], e + 1, startTime, beatDuration, lastPosses);
            }
        }

        void CreateLastThing(int startTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            var layer = GetLayer("LastBruh");

            var circle = new OsbSprite[3];

            for (int i = 0; i < circle.Length; i++)
            {
                float scalee = 0.32f - (0.1f * i);
                circle[i] = layer.CreateSprite(circlePath, OsbOrigin.Centre);

                circle[i].Color(startTime, 45001, colorRGB(41, 41, 41), colorRGB(41, 41, 41));
                circle[i].Scale(OsbEasing.OutBack, startTime, startTime + beatDuration, 0, scalee);

                circle[i].Scale(OsbEasing.OutBack, startTime + beatDuration * (4 + 0.25f * i), startTime + beatDuration * (4.5f + 0.25f * i), scalee, scalee * 1.6f);
                circle[i].Scale(OsbEasing.In, startTime + beatDuration * (4.25f + 7 * 0.25f), startTime + beatDuration * (4.25f + 7 * 0.25f + 1f), scalee * 1.6f, 0);

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

        OsbSprite[] CreateHadou(StoryboardLayer layer, double start, int amount, double timebetween, Color4 innerColor, Color4 outerColor, float x = 320, float y = 240)
        {
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

        void RotateMulti(OsbSprite[] sprites, double start, float degrees)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].Rotate(start, MathHelper.DegreesToRadians(degrees));
            }
        }

        void ScaleTwo(OsbSprite[] sprites, OsbEasing easing, double start, double end, float startScaleX, float startScaleY, float endScaleX, float endScaleY, float scaleDiff)
        {
            sprites[0].ScaleVec(easing, start, end, startScaleX, startScaleY, endScaleX, endScaleY);
            sprites[1].ScaleVec(easing, start, end, startScaleX * scaleDiff, startScaleY * scaleDiff, endScaleX * scaleDiff, endScaleY * scaleDiff);
        }

        void FadeMultiOne(OsbSprite[] sprites, double start, float startFade)
        {
            foreach (var item in sprites)
            {
                item.Fade(start, startFade);
            }
        }

        void ColorInnerOuter(OsbSprite[] sprites, double start, Color4 outerColor, Color4 innerColor)
        {
            sprites[0].Color(start, outerColor);
            sprites[1].Color(start, innerColor);
        }

        OsbSprite[] Splatter1(Vector2 center, double startTime, int amountSplatters, double beatDuration)
        {
            Vector2[] endYepPos = new Vector2[amountSplatters];
            OsbSprite[] cir = new OsbSprite[amountSplatters];
            var layer = GetLayer("");
            for (int i = 0; i < amountSplatters; i++)
            {
                // 最初の円
                cir[i] = layer.CreateSprite(ballPath, OsbOrigin.Centre);
                // 終わりの場所
                endYepPos[i] = new Vector2(center.X + Random(-60, 60), center.Y + Random(-60, 60));

                cir[i].Move(OsbEasing.OutQuad, startTime, startTime + beatDuration * (0.5f), center, endYepPos[i]);
                cir[i].Color(startTime, colorRGB(255, 255, 255));
                cir[i].Scale(startTime, Random(0.004f, 0.010f));
                cir[i].Fade(startTime, 1);
            }

            return cir;
        }

        OsbSprite[] SplatterOneWay(Vector2 center, double startTime, int amountSplatters, double beatDuration, Vector2 xRange, Vector2 yRange)
        {
            Vector2[] endYepPos = new Vector2[amountSplatters];
            OsbSprite[] cir = new OsbSprite[amountSplatters];
            var layer = GetLayer("");
            for (int i = 0; i < amountSplatters; i++)
            {
                // 最初の円
                cir[i] = layer.CreateSprite(ballPath, OsbOrigin.Centre);
                // 終わりの場所
                endYepPos[i] = new Vector2(center.X + Random(xRange.X, xRange.Y), center.Y + Random(yRange.X, yRange.Y));

                cir[i].Move(OsbEasing.OutQuad, startTime, startTime + beatDuration * (0.5f), center, endYepPos[i]);
                cir[i].Color(startTime, colorRGB(255, 255, 255));
                cir[i].Scale(startTime, Random(0.004f, 0.010f));
                cir[i].Fade(startTime, 1);
            }

            return cir;
        }

        Vector2[] placeInRing(int AmountPlaces, int LengthFromCentre)
        {
            Vector2[] positions = new Vector2[AmountPlaces];
            for (int i = 0; i < AmountPlaces; i++)
            {
                float degToChng = (360 / AmountPlaces);
                float y = LengthFromCentre * (float)Math.Sin(MathHelper.DegreesToRadians((degToChng * i) + 30));
                float x = LengthFromCentre * (float)Math.Cos(MathHelper.DegreesToRadians((degToChng * i) + 30));
                float realY = 240 - y;
                float realX = 320 - x;

                positions[i] = new Vector2(realX, realY);
            }

            return positions;
        }

        void bg(StoryboardLayer layer, double startTime, double endTime, Color4 color)
        {
            var bg = layer.CreateSprite(dotPath, OsbOrigin.Centre);

            bg.Color(startTime, color);
            bg.ScaleVec(startTime, endTime, 854, 480, 854, 480);
        }

        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
