using SadConsole;
using SadRogue.Primitives;

namespace LiteChat.Core.Console
{
    /// <summary>
    /// LayoutConsole used to create simple layouts similarly to HTML layouts with div elements
    /// </summary>
    public class LayoutConsole : SadConsole.Console
    {
        #region Fields
        protected Rectangle rConsoleRectangle = new Rectangle();
        protected int gBorderGlyph = 0;
        protected string szWindowTitle = string.Empty;

        public string WindowTitle { get { return szWindowTitle; } set { SetWindowTitle(value); } }

        #endregion

        #region Constructors
        public LayoutConsole(int width, int height, Color foreground, Color background, int glyph, string title = "") : base(width, height)
        {
            Initialize(width, height, foreground, background, glyph, title);
        }

        public LayoutConsole(int width, int height, Color foreground, Color background, int glyph, ColoredGlyph[] initialCells, string title = "") : base(width, height, initialCells)
        {
            Initialize(width, height, foreground, background, glyph, title);
        }

        public LayoutConsole(ICellSurface surface, Color foreground, Color background, int glyph, string title = "", IFont font = null, Point? fontSize = null) : base(surface, font, fontSize)
        {
            Initialize(surface.Width, surface.Height, foreground, background, glyph, title);
        }

        public LayoutConsole(int width, int height, Color foreground, Color background, int glyph, int bufferWidth, int bufferHeight, string title = "") : base(width, height, bufferWidth, bufferHeight)
        {
            Initialize(width, height, foreground, background, glyph, title);
        }

        public LayoutConsole(int width, int height, Color foreground, Color background, int glyph, int bufferWidth, int bufferHeight, ColoredGlyph[] initialCells, string title = "") : base(width, height, bufferWidth, bufferHeight, initialCells)
        {
            Initialize(width, height, foreground, background, glyph, title);
        }
        #endregion

        #region Methods

        void Initialize(int width, int height, Color foreground, Color background, int glyph, string title)
        {
            gBorderGlyph = glyph;
            SetDefaultColors(foreground, background);
            GenerateBox(width, height);
            WindowTitle = title;
        }

        void SetDefaultColors(Color foreground, Color background)
        {
            DefaultForeground = foreground;
            DefaultBackground = background;
        }

        void GenerateBox(int width, int height)
        {
            rConsoleRectangle = new Rectangle(0, 0, width, height);
            this.DrawBox(rConsoleRectangle, ShapeParameters.CreateFilled(new ColoredGlyph(DefaultForeground, DefaultBackground, gBorderGlyph), new ColoredGlyph(DefaultForeground, DefaultBackground)));
        }

        void SetWindowTitle(string title, bool upperCase = false)
        {
            if (String.IsNullOrEmpty(title)) return;
            if (upperCase) title = title.ToUpper();

            // clean up previous title and replace with the border.
            for (int i = 0; i < Width; i++)
            {
                this.Print(i, 0, $"", new ColoredGlyph(DefaultForeground, DefaultBackground, gBorderGlyph));
            }

            this.Print(2, 0, $"{title}"); // 2, 0 is the offset used for every LayoutConsole's title. 
        }

        #endregion

    }
}
