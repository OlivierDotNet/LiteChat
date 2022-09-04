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
        protected string szConsoleTitle = string.Empty;

        public string ConsoleTitle { get { return szConsoleTitle; } set { SetConsoleTitle(value); } }

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

        /// <summary>
        /// Creates a <see cref="LayoutConsole"/> child (<see cref="ScreenObject.Children"/>) and positions it as a block element in html
        /// </summary>
        /// <param name="width">width of the new <see cref="LayoutConsole"/></param>
        /// <param name="height">height of the new <see cref="LayoutConsole"/></param>
        /// <param name="foreground">Text &amp; Border color of the new <see cref="LayoutConsole"/></param>
        /// <param name="background">Background color for the new <see cref="LayoutConsole"/></param>
        /// <param name="glyph">The border glyph to be used in the new <see cref="LayoutConsole"/></param>
        /// <param name="title">Whether to show a title for the new <see cref="LayoutConsole"/></param>
        /// <returns>The created <see cref="LayoutConsole"/></returns>
        public LayoutConsole AddChildConsole(int width, int height, Color foreground, Color background, int glyph, string title = "")
        {
            LayoutConsole child = new LayoutConsole(width, height, foreground, background, glyph, title);

            if(child.Width > Width - 1)
            {
                child.Resize(Width - 2, child.Height, true);
            }
            if(child.Height > Height - 1)
            {
                child.Resize(child.Width, Height - 2, true);
            }

            child.Position = GetFirstAvailablePosition();
            Children.Add(child);
       
            return child;
        }

        /// <summary>
        /// Retrieves the position right below the most recent element, including borders.
        /// </summary>
        /// <returns>The offset from the starting position of the <see cref="LayoutConsole"/> to where the last element and written text is</returns>
        public Point GetFirstAvailablePosition()
        {
            // Default position is (1, 1) as it's within the border box.
            Point offset = new Point(1, 1);
            if (Children.Count > 0)
            {
                foreach (ScreenSurface screen in Children)
                {
                    // Increase offset for each element
                    offset += new Point(0, screen.Surface.Height);
                }

                // Also increase offset from the Cursor's last printed text position
                return offset += new Point(0, Cursor.Position.Y);
            }
            else if (Cursor.Position.Y > 1) // If the cursor has printed multiple rows
            {
                return offset + new Point(0, Cursor.Position.Y);
            }
            else // return base offset
            {
                return offset;
            }
        }

        /// <summary>
        /// Prints <paramref name="text"/> at the cursor position
        /// </summary>
        /// <param name="text">Text to print</param>
        /// <param name="newLine">Whether to make a new line</param>
        public void Print(string text, bool newLine = true)
        {
            Cursor.Position = GetFirstAvailablePosition();
            Cursor.Print(text);
            if(newLine) Cursor.NewLine();
        }

        /// <inheritdoc cref="Print(string, bool)"/>
        public void Print(ColoredString text, bool newLine = true)
        {
            Cursor.Position = GetFirstAvailablePosition();
            Cursor.Print(text);
            if (newLine) Cursor.NewLine();
        }


        /// <summary>
        /// Initializes the <see cref="LayoutConsole"/> with values specified for space, border and colors
        /// </summary>
        /// <param name="width">Width of this <see cref="LayoutConsole"/></param>
        /// <param name="height">Height of this <see cref="LayoutConsole"/></param>
        /// <param name="foreground">Foreground color to be used for this <see cref="LayoutConsole"/></param>
        /// <param name="background">Background color to be used for this <see cref="LayoutConsole"/></param>
        /// <param name="glyph">The border glyph that surrounds this <see cref="LayoutConsole"/></param>
        /// <param name="title">The Title of this <see cref="LayoutConsole"/>, comparable to an h1 element</param>
        void Initialize(int width, int height, Color foreground, Color background, int glyph, string title)
        {
            gBorderGlyph = glyph;
            SetDefaultColors(foreground, background);
            GenerateBox(width, height);
            ConsoleTitle = title;

            // Offset the cursor so it's within the box bounds
            Cursor.Move(GetFirstAvailablePosition());
        }

        /// <summary>
        /// Sets all default colors for this <see cref="LayoutConsole"/>
        /// </summary>
        /// <param name="foreground">The foreground color (text color, border color) to be used</param>
        /// <param name="background">The background color to be used</param>
        void SetDefaultColors(Color foreground, Color background)
        {
            DefaultForeground = foreground;
            DefaultBackground = background;
        }

        /// <summary>
        /// Draws the border surrounding this Layout Console
        /// </summary>
        /// <param name="width">width of the <see cref="LayoutConsole"/></param>
        /// <param name="height">height of the <see cref="LayoutConsole"/></param>
        void GenerateBox(int width, int height)
        {
            rConsoleRectangle = new Rectangle(0, 0, width, height);
            this.DrawBox(rConsoleRectangle, ShapeParameters.CreateFilled(new ColoredGlyph(DefaultForeground, DefaultBackground, gBorderGlyph), new ColoredGlyph(DefaultForeground, DefaultBackground)));
        }

        /// <summary>
        /// Sets the Console Title, overwriting the previous one and replacing empty space with the border
        /// </summary>
        /// <param name="title">new title to be set</param>
        /// <param name="upperCase">whether to uppercase the title</param>
        void SetConsoleTitle(string title, bool upperCase = false)
        {
            if (String.IsNullOrEmpty(title)) return;
            if (upperCase) title = title.ToUpper();

            // clean up previous title and replace with the border.
            for (int i = 0; i < Width; i++)
            {
                this.Print(i, 0, $"", new ColoredGlyph(DefaultForeground, DefaultBackground, gBorderGlyph));
            }

            szConsoleTitle = title;
            this.Print(2, 0, $"{title}"); // 2, 0 is the offset used for every LayoutConsole's title. 
        }

        #endregion

    }
}
