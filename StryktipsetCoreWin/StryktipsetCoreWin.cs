using StryktipsetCore;
using StryktipsetCore.Contract;

namespace StryktipsetCoreWin
{
    public partial class Tipsxtra : Form
    {
        public Tipsxtra()
        {
            InitializeComponent();
        }

        public static Omgång[] LäsInTipsxtraOmgångar()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "TipsXtra (*.csv)|*.csv",
                Multiselect = false,
                Title = "Välj summering"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var summering = openFileDialog.FileName;

                openFileDialog.Title = "Välj detaljer";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return Read.OmgångarMedMatcher(summering, openFileDialog.FileName);
                }
            }

            return Array.Empty<Omgång>();
        }
    }
}