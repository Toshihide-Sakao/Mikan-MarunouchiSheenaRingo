using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System.Drawing;

namespace StorybrewScripts
{
    public class FirstLyrics : StoryboardObjectGenerator
    {
        public string fontName = "超極細ゴシック";
        private FontGenerator font;

        public override void Generate()
        {
            font = FontGenerator("sb/Lyrics");
            double beatDuration = Beatmap.GetTimingPointAt((int)2).BeatDuration;

            
            // "I'll rip into those robes and pursue the Dharma", 2, 6430

            // "A Buddhist monk of my own would feel fine", 6430, 10716

            // "Selflessness and cessation get you Nirvana", 10716, 15001

            // "It Kurt would beat my Gretsch, I think I'd fly", 15001, 19287

            CreateText("I'll rip into those robes and pursue the Dharma", 2144, 6430);
            CreateText("A Buddhist monk of my own would feel fine", 6430, 10716);
            CreateText("Selflessness and cessation get you Nirvana", 10716, 15001);
            CreateText("It Kurt would beat my Gretsch, I think I'd fly", 15001, 19287);
        }

        FontGenerator FontGenerator(string output)
        {
            var font = LoadFont(output, new FontDescription()
            {
                FontPath = fontName,
                FontSize = 100,
                Color = Color4.White,
                Padding = Vector2.Zero,
                FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontOutline()
            {
                Thickness = 2,
                Color = Color4.White,
            });

            return font;
        }

        public void CreateText(string text, int startTime, int endTime)
        {
            var texture = font.GetTexture(text);
            var bitmap = GetMapsetBitmap(texture.Path);
            var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre);

            sprite.Scale(startTime, 0.2f);
            sprite.Fade(startTime, startTime + 300, 0, 1);
            sprite.Fade(endTime - 400, endTime - 50, 1, 0);

            var underline = GetLayer("").CreateSprite("sb/dot.png", OsbOrigin.Centre, new Vector2(320, 260));
            underline.ScaleVec(OsbEasing.OutExpo, startTime, startTime + 1000, 0, 1, bitmap.Width * 0.2f - 10, 1);
            underline.Fade(startTime, 1);
            underline.Fade(endTime - 400, endTime - 50, 1, 0);
        }
    }
}
