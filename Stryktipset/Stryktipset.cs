﻿using StryktipsetCore.Contract;
using StryktipsetCoreWin;
using System;
using System.Linq;
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
            Omgångar = Tipsxtra.LäsInTipsxtraOmgångar();

            antalOmgångar.Text = $"Omgångar: {Omgångar.Count()}";

            MessageBox.Show($"Snitt 1: {Omgångar.Average(o => o.Ettor)}{Environment.NewLine}Snitt X: {Omgångar.Average(o => o.Kryss)}{Environment.NewLine}Snitt 2: {Omgångar.Average(o => o.Tvåor)}");

            omgångar.Nodes.AddRange(Omgångar
                .Where(o => o.Matcher.Any(m => m.SvenskaFolketEttProcent == 38 && m.SvenskaFolketKryssProcent == 27 && m.SvenskaFolketTvåProcent == 35) &&
                            o.Matcher.Any(m => m.SvenskaFolketEttProcent >= 21))
                .Select(o => new TreeNode($"Omgång {o.Vecka}. {o.Ettor} ettor, {o.Kryss} kryss, {o.Tvåor} tvåor.", o.Matcher
                .Select(m => new TreeNode($"{m.HemmaLag} {m.HemmaMål} - {m.BortaMål} {m.BortaLag}. Svenska folket gissade: 1: {m.SvenskaFolketEttProcent}%, X: {m.SvenskaFolketKryssProcent}%, 2: {m.SvenskaFolketTvåProcent}%")).ToArray())).ToArray());
        }
    }
}
