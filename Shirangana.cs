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
    public class Shirangana : StoryboardObjectGenerator
    {
        [Configurable] public int startTime = 62145;
        [Configurable] public int endTime;
        public string roundRectPath = "sb/roundRect.png";
        public string dotPath = "sb/dot.png";
        public string ballPath = "sb/ball.png";
        public string blendBallPath = "sb/fadeBall.png";
        public override void Generate()
        {
            var layer = GetLayer("");


            OsbSprite[] outerCircle = new OsbSprite[]
            {
                layer.CreateSprite(blendBallPath, OsbOrigin.Centre),
                layer.CreateSprite(ballPath, OsbOrigin.Centre)
            };
            OsbSprite[] sq = new OsbSprite[]
            {
                layer.CreateSprite(dotPath, OsbOrigin.Centre),
                layer.CreateSprite(dotPath, OsbOrigin.Centre)
            };
            OsbSprite[] sq2 = new OsbSprite[]
            {
                layer.CreateSprite(dotPath, OsbOrigin.Centre),
                layer.CreateSprite(dotPath, OsbOrigin.Centre)
            };
            OsbSprite[] circle = new OsbSprite[]
            {
                layer.CreateSprite(blendBallPath, OsbOrigin.Centre),
                layer.CreateSprite(ballPath, OsbOrigin.Centre)
            };
            OsbSprite[] taiyoKou = new OsbSprite[6];
            for (int i = 0; i < taiyoKou.Length; i++)
            {
                taiyoKou[i] = layer.CreateSprite(dotPath, OsbOrigin.BottomCentre);
            }

            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            var roundRect = layer.CreateSprite(roundRectPath, OsbOrigin.Centre, new Vector2(320, 500));

            roundRect.Color(startTime, colorRGB(167, 122, 230));
            roundRect.Scale(startTime, 1.3f / 6);
            roundRect.MoveY(OsbEasing.OutQuint, startTime, startTime + beatDuration, 500, 230);

            roundRect.Scale(OsbEasing.OutCubic, startTime + beatDuration, startTime + beatDuration * 5 / 4, 1.3f / 6, 0.7f / 6);
            roundRect.Rotate(OsbEasing.OutCubic, startTime + beatDuration, startTime + beatDuration * 5 / 4, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-90));
            roundRect.MoveY(OsbEasing.OutQuart, startTime + beatDuration, startTime + beatDuration * 5 / 4, 230, 240);

            roundRect.Fade(startTime, endTime, 1, 1);

            ScaleTwo(sq, OsbEasing.OutSine, startTime + beatDuration, startTime + beatDuration * 5 / 4, 10, 10, 250, 250, 0.96f);
            ColorInnerOuter(sq, startTime + beatDuration, colorRGB(230, 142, 160), colorRGB(255, 255, 255));
            RotateMulti(sq, OsbEasing.OutCubic, startTime + beatDuration, startTime + beatDuration * 5 / 4, -45, 45);

            ScaleTwo(circle, OsbEasing.OutQuad, startTime + beatDuration, startTime + beatDuration * 5 / 4, 0.6f, 0.6f, 0.3f, 0.3f, 0.6f);
            ColorInnerOuter(circle, startTime + beatDuration, colorRGB(100, 145, 217), colorRGB(255, 255, 255));

            ScaleTwo(outerCircle, OsbEasing.OutQuad, startTime + beatDuration, startTime + beatDuration * 5 / 4, 1.2f, 1.2f, 0.8f, 0.8f, 0.93f);
            ColorInnerOuter(outerCircle, startTime + beatDuration, colorRGB(141, 180, 242), colorRGB(255, 255, 255));

            TaiyouKou(taiyoKou, startTime + beatDuration, startTime + beatDuration * 2, 0, 0);

            ScaleTwo(sq2, OsbEasing.OutExpo, startTime + beatDuration * 2, startTime + beatDuration * 3, 10, 10, 220, 220, 0.96f);
            ColorInnerOuter(sq2, startTime + beatDuration * 2, colorRGB(252, 151, 151), colorRGB(255, 255, 255));
            RotateMulti(sq2, OsbEasing.OutBack, startTime + beatDuration * 2, startTime + beatDuration * 3, -45, 45);

            ScaleTwo(outerCircle, OsbEasing.Out, startTime + beatDuration * 2, startTime + beatDuration * 2.5f, 0.8f, 0.8f, 1.3f, 1.3f, 0.96f);
            ScaleTwo(sq, OsbEasing.Out, startTime + beatDuration * 2, startTime + beatDuration * 2.5f, 250, 250, 380, 380, 0.97f);
            RotateMulti(sq, OsbEasing.OutExpo, startTime + beatDuration * 2, startTime + beatDuration * 2.5f, 45, 135);

            roundRect.Scale(OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, roundRect.ScaleAt(startTime + beatDuration * 3).X, roundRect.ScaleAt(startTime + beatDuration * 3).X * 1.3f);
            roundRect.Rotate(OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(45));
            ScaleTwo(sq2, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 220, 220, 280, 280, 0.97f);
            RotateMulti(sq2, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 45, 45 + 50);
            ScaleTwo(sq, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 380, 380, 450, 450, 0.97f);
            RotateMulti(sq, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 45, 45 + 40);
            ScaleTwo(outerCircle, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 1.3f, 1.3f, 1.4f, 1.4f, 0.97f);
            RotateMulti(outerCircle, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 0, 45);
            ScaleTwo(circle, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 0.3f, 0.3f, 0.36f, 0.36f, 0.7f);
            RotateMulti(circle, OsbEasing.OutExpo, startTime + beatDuration * 3, startTime + beatDuration * 3.5f, 0, 45);

            // startTime + beatDuration * 4f
            roundRect.Scale(OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, roundRect.ScaleAt(startTime + beatDuration * 4f).X, roundRect.ScaleAt(startTime + beatDuration * 4f).X * 1.3f);
            roundRect.Rotate(OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, MathHelper.DegreesToRadians(45), MathHelper.DegreesToRadians(45 + 45));
            ScaleTwo(sq2, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 280, 280, 320, 320, 0.97f);
            RotateMulti(sq2, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 95, 95 + 50);
            ScaleTwo(sq, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 450, 450, 520, 520, 0.97f);
            RotateMulti(sq, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 85, 85 + 40);
            ScaleTwo(outerCircle, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 1.4f, 1.4f, 1.6f, 1.6f, 0.97f);
            RotateMulti(outerCircle, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 45, 45 + 45);
            ScaleTwo(circle, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 0.36f, 0.36f, 0.45f, 0.45f, 0.71f);
            RotateMulti(circle, OsbEasing.OutExpo, startTime + beatDuration * 4f, startTime + beatDuration * 4.5f, 45, 45 + 45);

            TheScaleThing(startTime + beatDuration * 5f, startTime + beatDuration * 5.5f, roundRect, sq, sq2, circle, outerCircle);
            TheScaleThing(startTime + beatDuration * 6f, startTime + beatDuration * 6.5f, roundRect, sq, sq2, circle, outerCircle);
            TheScaleThing(startTime + beatDuration * 7f, startTime + beatDuration * 7.5f, roundRect, sq, sq2, circle, outerCircle);
            TheScaleThing(startTime + beatDuration * 8f, startTime + beatDuration * 8.25f, roundRect, sq, sq2, circle, outerCircle);
            TheScaleThing(startTime + beatDuration * 8.5f, startTime + beatDuration * 9f, roundRect, sq, sq2, circle, outerCircle);
            // TheScaleThing(startTime + beatDuration * 9f, startTime + beatDuration * 9.25f, roundRect, sq, sq2, circle, outerCircle);
            // TheScaleThing(startTime + beatDuration * 9.5f, startTime + beatDuration * 9.75f, roundRect, sq, sq2, circle, outerCircle);
            // TheScaleThing(startTime + beatDuration * 10f, startTime + beatDuration * 10.25f, roundRect, sq, sq2, circle, outerCircle);
            // TheScaleThing(startTime + beatDuration * 10.5f, startTime + beatDuration * 10.75f, roundRect, sq, sq2, circle, outerCircle);

            roundRect.Scale(OsbEasing.OutExpo, startTime + beatDuration * 9f, startTime + beatDuration * 9.5f, roundRect.ScaleAt(startTime + beatDuration * 9.0f).X, roundRect.ScaleAt(startTime + beatDuration * 9f).X * 2.5f);
            roundRect.Rotate(OsbEasing.OutExpo, startTime + beatDuration * 9f, startTime + beatDuration * 9.5f, roundRect.RotationAt(startTime + beatDuration * 9f), roundRect.RotationAt(startTime + beatDuration * 9f) + MathHelper.DegreesToRadians(45));

            // FadeMulti(sq, OsbEasing.None, startTime + beatDuration, endTime, 1, 1);
            // FadeMulti(sq2, OsbEasing.None, startTime + beatDuration * 2, endTime, 1, 1);
            // FadeMulti(circle, OsbEasing.None, startTime + beatDuration, endTime, 1, 1);
            // FadeMulti(outerCircle, OsbEasing.None, startTime + beatDuration, endTime, 1, 1);
        }

        void TheScaleThing(double start, double end, OsbSprite roundRect, OsbSprite[] sq, OsbSprite[] sq2, OsbSprite[] circle, OsbSprite[] outerCircle)
        {
            if (roundRect.ScaleAt(start).X <= 10f)
            {
                roundRect.Scale(OsbEasing.OutExpo, start, end, roundRect.ScaleAt(start).X, roundRect.ScaleAt(start).X * 1.3f);
                roundRect.Rotate(OsbEasing.OutExpo, start, end, roundRect.RotationAt(start), roundRect.RotationAt(start) + MathHelper.DegreesToRadians(45));
            }
            if (sq2[0].ScaleAt(start).X <= 900)
            {
                ScaleTwo(sq2, OsbEasing.OutExpo, start, end, sq2[0].ScaleAt(start).X, sq2[0].ScaleAt(start).X, sq2[0].ScaleAt(start).X * 1.3f, sq2[0].ScaleAt(start).X * 1.3f, 0.96f);
                RotateMulti(sq2, OsbEasing.OutExpo, start, end, MathHelper.RadiansToDegrees(sq2[0].RotationAt(start)), MathHelper.RadiansToDegrees(sq2[0].RotationAt(start)) + 50);
            }
            if (sq[0].ScaleAt(start).X <= 900)
            {
                ScaleTwo(sq, OsbEasing.OutExpo, start, end, sq[0].ScaleAt(start).X, sq[0].ScaleAt(start).X, sq[0].ScaleAt(start).X * 1.3f, sq[0].ScaleAt(start).X * 1.3f, 0.97f);
                RotateMulti(sq, OsbEasing.OutExpo, start, end, MathHelper.RadiansToDegrees(sq[0].RotationAt(start)), MathHelper.RadiansToDegrees(sq[0].RotationAt(start)) + 40);
            }
            if (circle[0].ScaleAt(start).X <= 2.0f)
            {
                ScaleTwo(circle, OsbEasing.OutExpo, start, end, circle[0].ScaleAt(start).X, circle[0].ScaleAt(start).X, circle[0].ScaleAt(start).X * 1.3f, circle[0].ScaleAt(start).X * 1.3f, 0.77f);
                RotateMulti(circle, OsbEasing.OutExpo, start, end, MathHelper.RadiansToDegrees(circle[0].RotationAt(start)), MathHelper.RadiansToDegrees(circle[0].RotationAt(start)) + 45);
            }
            if (outerCircle[0].ScaleAt(start).X <= 1.6f)
            {
                ScaleTwo(outerCircle, OsbEasing.OutExpo, start, end, outerCircle[0].ScaleAt(start).X, outerCircle[0].ScaleAt(start).X, outerCircle[0].ScaleAt(start).X * 1.3f, outerCircle[0].ScaleAt(start).X * 1.3f, 0.97f);
                RotateMulti(outerCircle, OsbEasing.OutExpo, start, end, MathHelper.RadiansToDegrees(outerCircle[0].RotationAt(start)), MathHelper.RadiansToDegrees(outerCircle[0].RotationAt(start)) + 45);
            }

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

        void TaiyouKou(OsbSprite[] sprites, double start, double end, float startscale, float endscale)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            var degree = 360 / sprites.Length;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].Color(start, colorRGB(123, 100, 163));

                sprites[i].ScaleVec(OsbEasing.None, start, start + 50, 5, 0, 5, 60);

                float y = 160 * (float)Math.Sin(MathHelper.DegreesToRadians(degree * i));
                float x = 160 * (float)Math.Cos(MathHelper.DegreesToRadians(degree * i));
                float y2 = 1000 * (float)Math.Sin(MathHelper.DegreesToRadians(degree * i));
                float x2 = 1000 * (float)Math.Cos(MathHelper.DegreesToRadians(degree * i));
                float realY = 240 - y;
                float realX = 320 - x;
                float lastY = 240 - y2;
                float lastX = 320 - x2;
                sprites[i].Move(OsbEasing.OutBack, start, end, 320, 240, realX, realY);
                sprites[i].Move(OsbEasing.Out, end, startTime + beatDuration * 2.5f, realX, realY, lastX, lastY);

                sprites[i].Rotate(start, MathHelper.DegreesToRadians((degree * i) + 90));


            }
        }

        public Color4 colorRGB(float r, float g, float b)
        {
            return new Color4(r / 255, g / 255, b / 255, 1);
        }
    }
}
