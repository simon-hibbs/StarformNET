namespace DLS.StarformNET
{

    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.IO;
    using Display;
    using Data;

    public partial class MainGenerator : Form
    {
        private static string ArtFolder = "Art";
        private static string PlanetsFile = "PixelPlanets.png";

        private ChemTable[] _gases;
        private PlanetSpriteSheet _planetSprites;

        public MainGenerator()
        {
            InitializeComponent();
            var spriteFile = Path.Combine(Directory.GetCurrentDirectory(), ArtFolder, PlanetsFile);
            _planetSprites = new PlanetSpriteSheet(Image.FromFile(spriteFile), new Point(77, 71), new Size(32, 32), 5,
                5, 6);
            _systemMap.SpriteSheet = _planetSprites;
            _systemMap.PlanetPadding = 10;
            _gases = ChemTable.LoadFromFile(Path.Combine(Directory.GetCurrentDirectory(), "elements.dat"));

            GenerateSystem();
        }

        private void _regenButton_Click(object sender, EventArgs e)
        {
            GenerateSystem();
        }

        private void GenerateSystem()
        {
            var generator = new Generator(_gases);
            var star = new Star()
            {
                Mass = 1.0f,
                Luminosity = 1.0f
            };
            generator.GenerateStellarSystem(ref star, null, "p", 0, "whatever", true, true);
            _systemMap.SetNewSystem(generator.Planets);

            var text = PlanetText.GetSystemText(generator.Planets, _gases);
            _descriptionBox.Text = text;
        }
    }
}
