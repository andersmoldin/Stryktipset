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

        public static Omg�ng[] L�sInTipsxtraOmg�ngar()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "TipsXtra (*.csv)|*.csv",
                Multiselect = false,
                Title = "V�lj summering"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var summering = openFileDialog.FileName;

                openFileDialog.Title = "V�lj detaljer";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return Read.Omg�ngarMedMatcher(summering, openFileDialog.FileName);
                }
            }

            return Array.Empty<Omg�ng>();
        }
    }
}