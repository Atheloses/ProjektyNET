using DesktopAppVIS.DTOApp;
using DesktopAppVIS.Zalozky;
using DomainLogic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAppVIS
{
    public partial class Form1 : Form
    {

        private UzivatelApp _SelectedUzivatel;
        public UzivatelApp SelectedUzivatel
        {
            get { return _SelectedUzivatel; }
            set
            {
                _SelectedUzivatel = value;
                if (detailUzivatele != null && detailUzivatele.Visible)
                    detailUzivatele.SelectedUzivatel = value;
                if (novyUkol != null && novyUkol.Visible)
                    novyUkol.SelectedUzivatel = value;
                if (novaSkupina != null && novaSkupina.Visible)
                    novaSkupina.SelectedUzivatel = value;
            }
        }

        private DetailUzivatele detailUzivatele;
        private NovyUkol novyUkol;
        //private CiselnikPriorita ciselnikPriorita;
        private NovaSkupina novaSkupina;

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = UzivatelApp.GetAppFromDTO(new UzivatelDL().ZiskejUzivatele().Result);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is UzivatelApp)
                SelectedUzivatel = (UzivatelApp)comboBox1.SelectedItem;
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < this.Zalozky.TabPages.Count; i++)
            {
                Rectangle r = Zalozky.GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 3, 12, r.Height - 6);
                if (closeButton.Contains(e.Location))
                {
                    if (detailUzivatele != null && this.Zalozky.TabPages[i] == detailUzivatele.Zalozka)
                        detailUzivatele.Visible = false;

                    if (novyUkol != null && this.Zalozky.TabPages[i] == novyUkol.Zalozka)
                        novyUkol.Visible = false;

                    //if (ciselnikPriorita != null && this.Zalozky.TabPages[i] == ciselnikPriorita.Zalozka)
                    //    ciselnikPriorita.Visible = false;

                    this.Zalozky.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        private void DetailUzivateleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Selected = false;
            if (detailUzivatele != null)
            {
                for (int i = 0; i < this.Zalozky.TabPages.Count; i++)
                {
                    if (this.Zalozky.TabPages[i] == detailUzivatele.Zalozka)
                    {
                        Zalozky.SelectedTab = detailUzivatele.Zalozka;
                        Selected = true;
                    }
                }
            }
            if (!Selected)
                detailUzivatele = new DetailUzivatele(Zalozky) { SelectedUzivatel = SelectedUzivatel };
        }

        private void NovyUkolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Selected = false;
            if (novyUkol != null)
            {
                for (int i = 0; i < this.Zalozky.TabPages.Count; i++)
                {
                    if (this.Zalozky.TabPages[i] == novyUkol.Zalozka)
                    {
                        Zalozky.SelectedTab = novyUkol.Zalozka;
                        Selected = true;
                    }
                }
            }
            if (!Selected)
                novyUkol = new NovyUkol(Zalozky) { SelectedUzivatel = SelectedUzivatel };
        }

        //private void PrioritaToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    bool Selected = false;
        //    if (ciselnikPriorita != null)
        //    {
        //        for (int i = 0; i < this.Zalozky.TabPages.Count; i++)
        //        {
        //            if (this.Zalozky.TabPages[i] == ciselnikPriorita.Zalozka)
        //            {
        //                Zalozky.SelectedTab = ciselnikPriorita.Zalozka;
        //                Selected = true;
        //            }
        //        }
        //    }
        //    if (!Selected)
        //        ciselnikPriorita = new CiselnikPriorita(Connection, Zalozky);
        //}

        private void NovaSkupinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Selected = false;
            if (novaSkupina != null)
            {
                for (int i = 0; i < this.Zalozky.TabPages.Count; i++)
                {
                    if (this.Zalozky.TabPages[i] == novaSkupina.Zalozka)
                    {
                        Zalozky.SelectedTab = novaSkupina.Zalozka;
                        Selected = true;
                    }
                }
            }
            if (!Selected)
                novaSkupina = new NovaSkupina(Zalozky) { SelectedUzivatel = SelectedUzivatel };
        }

    }
}
