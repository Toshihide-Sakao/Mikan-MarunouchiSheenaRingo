using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System.Drawing;

namespace StorybrewScripts
{
    // Simple and clean Lyrics script by -Tochi
    // Text is generated as a whole sentence so you cannot adjust the parameters for each letter
    // Fore help, join our Discord server: https://discord.gg/QZjD3yb

    public class SimplifiedLyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public string fontName = "Verdana";

        [Configurable]
        public float opacity = 1f;
        
        [Configurable]
        public int fontScale = 30;

        [Configurable]
        public float spriteScale = 1f;

        [Configurable]
        public float fadeStartDelay = 1000;

        [Configurable]
        public float fadeEndDelay = 1000;

        [Configurable]
        public FontStyle fontStyle = FontStyle.Regular;

        [Configurable]
        public bool enableColor = false;

        [Configurable]
        public Color4 textColor = Color4.IndianRed;

        [Configurable]
        public bool enableGlow = false;

        [Configurable]
        public int glowRadius = 30;

        [Configurable]
        public Color4 glowColor = Color4.White;

        [Configurable]
        public int outlineThickness = 0;

        [Configurable]
        public Color4 outlineColor = Color4.Black;

        [Configurable]
        public int shadowThickness = 0;

        [Configurable]
        public Color4 shadowColor = Color4.Black;

        private FontGenerator font;

        public override void Generate()
        {
            font = FontGenerator("sb/lyrics");
            // You should write the lyrics in this function

            // The 'CreateText' function's parameters should be in this order (for reference):
            // CreateText(startTime, endTime, position, origin, text)

            CreateText(0, 3000, new Vector2(320, 240), OsbOrigin.Centre, "Hello world.");
            CreateText(4000, 7000, new Vector2(320, 240), OsbOrigin.Centre, "How are you?");
            CreateText(0, 3000, new Vector2(320, 340), OsbOrigin.Centre, "こんにちは世界。");
            CreateText(4000, 7000, new Vector2(320, 340), OsbOrigin.Centre, "お元気ですか？");

            // Any language is supported (as long as the fontName used supports the language)
        }

        FontGenerator FontGenerator(string output)
        {
            var font = LoadFont(output, new FontDescription()
            {
                FontPath = fontName,
                FontSize = fontScale,
                Color = Color4.White,
                Padding = Vector2.Zero,
                FontStyle = fontStyle,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontGlow()
            {
                Radius = !enableGlow ? 0 : glowRadius,
                Color = glowColor,
            },
            new FontOutline()
            {
                Thickness = outlineThickness,
                Color = outlineColor,
            },
            new FontShadow()
            {
                Thickness = shadowThickness,
                Color = shadowColor,
            });

            return font;
        }

        // lyrics code
        public void CreateText(int startTime, int endTime, Vector2 position, OsbOrigin origin, string text)
        {
            var texture = font.GetTexture(text);
            var sprite = GetLayer("").CreateSprite(texture.Path, origin, position);

            sprite.Scale(startTime, 0.5f * spriteScale);
            sprite.Fade(startTime, startTime + fadeStartDelay, 0, opacity);
            sprite.Fade(endTime, endTime + fadeEndDelay, opacity, 0);
            if (enableColor)
            sprite.Color(startTime, textColor);
        }
    }
}

