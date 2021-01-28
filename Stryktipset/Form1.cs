using StryktipsetCore;
using StryktipsetCore.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stryktipset
{
    public partial class Stryktipset : Form
    {
        public Omgång[] Omgångar { get; set; }

        public Stryktipset()
        {
            InitializeComponent();
        }

        private void LäsInOmgångar_Click(object sender, EventArgs e)
        {
            Omgångar = Read.OmgångarMedMatcher();

            antalOmgångar.Text = $"Omgångar: {Omgångar.Count()}";

            omgångar.Nodes.AddRange(Omgångar
                .Where(o => o.Matcher.Any(m => m.SvenskaFolketEttProcent == 38 && m.SvenskaFolketKryssProcent == 27 && m.SvenskaFolketTvåProcent == 35) &&
                            o.Matcher.Any(m => m.SvenskaFolketEttProcent >= 21))
                .Select(o => new TreeNode($"Omgång {o.Vecka}. {o.Ettor} ettor, {o.Kryss} kryss, {o.Tvåor} tvåor.", o.Matcher
                .Select(m => new TreeNode($"{m.HemmaLag} {m.HemmaMål} - {m.BortaMål} {m.BortaLag}. Svenska folket gissade: 1: {m.SvenskaFolketEttProcent}%, X: {m.SvenskaFolketKryssProcent}%, 2: {m.SvenskaFolketTvåProcent}%")).ToArray())).ToArray());
        }
    }
}
