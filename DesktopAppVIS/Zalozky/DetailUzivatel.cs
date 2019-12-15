using DesktopAppVIS.DTOApp;
using DomainLogic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesktopAppVIS.Zalozky
{
    public class DetailUzivatele
    {
        public bool Visible { get; set; }
        private TabControl Zalozky { get; set; }
        public TabPage Zalozka { get; set; }

        private UzivatelApp _SelectedUzivatel;
        public UzivatelApp SelectedUzivatel
        {
            get { return _SelectedUzivatel; }
            set
            {
                _SelectedUzivatel = value;
                FillData();
            }
        }

        private Label NazevForm { get; set; }
        private TextBox Jmeno { get; set; }
        private Label JmenoLabel { get; set; }
        private TextBox Prijmeni { get; set; }
        private Label PrijmeniLabel { get; set; }
        private TextBox Email { get; set; }
        private Label EmailLabel { get; set; }
        private Label UkolyLabel { get; set; }
        private DataGridView Ukoly { get; set; }
        private Label SkupinyLabel { get; set; }
        private DataGridView Skupiny { get; set; }
        private Button Ulozit { get; set; }
        private Button Splnit { get; set; }

        public DetailUzivatele( TabControl p_Zalozky)
        {
            Zalozky = p_Zalozky;
            string title = "Detail uživatele     x";
            Zalozka = new TabPage(title)
            {
                BackColor = Color.White
            };

            InitContent();
            Visible = true;
            Zalozky.SelectedTab = Zalozka;
        }

        private void InitContent()
        {
            var TableLayout = new TableLayoutPanel
            {
                RowCount = 9,
                ColumnCount = 2
            };
            TableLayout.AutoSize = true;
            TableLayout.AutoScroll = true;
            TableLayout.Dock = DockStyle.Fill;
            for (int i = 0; i < TableLayout.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100 / TableLayout.ColumnCount);
                TableLayout.ColumnStyles.Add(cs);
            }

            //TableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            NazevForm = new Label() { Text = "Uživatel", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };

            JmenoLabel = new Label() { Text = "Jméno:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Jmeno = new TextBox();

            PrijmeniLabel = new Label() { Text = "Příjmení:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Prijmeni = new TextBox();

            EmailLabel = new Label() { Text = "Email:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Email = new TextBox();
            Email.Validating += new CancelEventHandler(IsValidEmail);

            UkolyLabel = new Label() { Text = "Mé úkoly", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Bottom };
            Ukoly = new DataGridView() { Dock = DockStyle.Fill };
            Ukoly.MaximumSize = new Size(1920, 150);
            Ukoly.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            SkupinyLabel = new Label() { Text = "Mé skupiny", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Bottom };
            Skupiny = new DataGridView() { Dock = DockStyle.Fill };
            Skupiny.MaximumSize = new Size(1920, 150);
            Skupiny.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            Ulozit = new Button { Text = "Uložit informace", AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            Ulozit.Click += new System.EventHandler(this.UlozitUzivatele);

            Splnit = new Button { Text = "Splnit označené", AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            Splnit.Click += new System.EventHandler(this.SplnitUkoly);

            TableLayout.Controls.Add(NazevForm, 0, 0);
            TableLayout.SetColumnSpan(NazevForm, 2);
            TableLayout.Controls.Add(JmenoLabel, 0, 1);
            TableLayout.Controls.Add(Jmeno, 1, 1);
            TableLayout.Controls.Add(PrijmeniLabel, 0, 2);
            TableLayout.Controls.Add(Prijmeni, 1, 2);
            TableLayout.Controls.Add(EmailLabel, 0, 3);
            TableLayout.Controls.Add(Email, 1, 3);
            TableLayout.Controls.Add(UkolyLabel, 0, 4);
            TableLayout.SetColumnSpan(UkolyLabel, 2);
            TableLayout.Controls.Add(Ukoly, 0, 5);
            TableLayout.SetColumnSpan(Ukoly, 2);
            TableLayout.Controls.Add(SkupinyLabel, 0, 6);
            TableLayout.SetColumnSpan(SkupinyLabel, 2);
            TableLayout.Controls.Add(Skupiny, 0, 7);
            TableLayout.SetColumnSpan(Skupiny, 2);
            TableLayout.Controls.Add(Ulozit, 0, 8);
            TableLayout.Controls.Add(Splnit, 1, 8);
            Zalozka.Controls.Add(TableLayout);
            Zalozky.TabPages.Add(Zalozka);
        }

        private async void FillData()
        {
            NazevForm.Text = SelectedUzivatel.Prezdivka;
            Jmeno.Text = SelectedUzivatel.Jmeno;
            Prijmeni.Text = SelectedUzivatel.Prijmeni;

            List<DetailUkolApp> ukoly = new List<DetailUkolApp>();
            using var ukolDL = new UkolDL();
            foreach (var row in await ukolDL.ZiskejUkolyUzivatele(SelectedUzivatel.ID))
                foreach (var ukol in row.Value)
                {
                    ukoly.Add(DetailUkolApp.GetAppFromDTO(row.Key, ukol));
                }


            IEnumerable<SkupinaApp> skupiny = new List<SkupinaApp>();
            using var skupinaDL = new SkupinaDL();
            skupiny = SkupinaApp.GetAppFromDTO(await skupinaDL.ZiskejSkupinyUzivatele(SelectedUzivatel.ID));

            //using var uzivatelUkolDL = new UzivatelUkolDL();
            //var ukoly = UzivatelUkolApp.GetAppFromDTO(await uzivatelUkolDL.ZiskejUzivatelUkoly());
            //ukoly = ukoly.Where(i => i.IDUzivatel == SelectedUzivatel.ID).ToList();
            //ukoly = ukoly.OrderBy(o => o.TerminPodukolu).ToList();
            Ukoly.DataSource = ukoly; //.Where(i => !i.SplnenUkol && !i.SplnenPodukol).ToList()

            //using var uzivatelSkupinaDL = new UzivatelSkupinaDL();
            //var meSkupiny = uzivatelSkupinaDL.ZiskejUzivateloveSkupiny(SelectedUzivatel.ID);
            //var ukolySkupin = new SkupinaUkolDA(Connection).SelectAll();
            //var ukolyMychSkupin = ukolySkupin.Where(i => meSkupiny.Exists(b => b.SkupinaFK.ID == i.SkupinaFK.ID)).ToList();
            //ukolyMychSkupin = ukolyMychSkupin.OrderBy(o => o.Termin).ToList();
            Skupiny.DataSource = skupiny.OrderBy(x=>x.DatumVytvoreni).ToList(); //UkolySkupinApp.GetAppFromDTO(ukolyMychSkupin).Where(i => !i.SplnenUkol && !i.SplnenPodukol).ToList();

            if (Ukoly.RowCount > 0)
            {
                Ukoly.Columns["IDUkol"].Visible = false;
                Ukoly.Columns["IDUzivatelUkol"].Visible = false;
                Ukoly.Columns["IDUzivatel"].Visible = false;
                //Ukoly.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (Skupiny.RowCount > 0)
            {
                Skupiny.Columns["ID"].Visible = false;
            }
        }

        private async void UlozitUzivatele(object sender, EventArgs e)
        {
            if (!IsValidEmail(Email.Text))
            {
                Email.BackColor = Color.IndianRed;
                return;
            }

            SelectedUzivatel.Jmeno = Jmeno.Text;
            SelectedUzivatel.Prijmeni = Prijmeni.Text;
            SelectedUzivatel.Email = Email.Text;

            await new UzivatelDL().UlozUzivatel(UzivatelApp.GetDTOFromApp(SelectedUzivatel));
        }

        private async void SplnitUkoly(object sender, EventArgs e)
        {

            var _Ukoly = new List<int>();
            var _PodukolyUzivatele = new List<int>();
            for (int i = 0; i < Ukoly.RowCount; i++)
            {
                if (Convert.ToBoolean(Ukoly.Rows[i].Cells["OznacenUkol"].Value.ToString()))
                    _Ukoly.Add(Convert.ToInt32(Ukoly.Rows[i].Cells["IDUkol"].Value.ToString()));

                if (Convert.ToBoolean(Ukoly.Rows[i].Cells["OznacenPodukol"].Value.ToString()))
                    _PodukolyUzivatele.Add(Convert.ToInt32(Ukoly.Rows[i].Cells["IDUzivatelUkol"].Value.ToString()));
            }

            var _PodukolySkupin = new List<int>();
            for (int i = 0; i < Skupiny.RowCount; i++)
            {
                if (Convert.ToBoolean(Skupiny.Rows[i].Cells[7].Value.ToString()))
                    _PodukolySkupin.Add(Convert.ToInt32(Skupiny.Rows[i].Cells[6].Value.ToString()));

                if (Convert.ToBoolean(Skupiny.Rows[i].Cells[2].Value.ToString()))
                    _Ukoly.Add(Convert.ToInt32(Skupiny.Rows[i].Cells[1].Value.ToString()));
            }

            var uzivatele = string.Join(",", _PodukolyUzivatele);
            var skupiny = string.Join(",", _PodukolySkupin);
            foreach (var ukol in _Ukoly)
                await new UkolDL().SplnitUkol(ukol);

            foreach (var podukol in _PodukolyUzivatele)
                await new UzivatelUkolDL().SplnitUzivatelUkol(podukol);

            //if (!string.IsNullOrEmpty(uzivatele) || !string.IsNullOrEmpty(ukoly) || !string.IsNullOrEmpty(skupiny))
            FillData();
        }

        private void IsValidEmail(object sender, EventArgs e)
        {
            if (!IsValidEmail(Email.Text))
            {
                Email.BackColor = Color.IndianRed;
                return;
            }
            else
            {
                Email.BackColor = Color.White;
                return;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return true;
                var addr = new System.Net.Mail.MailAddress(email);
                Email.BackColor = Color.IndianRed;
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
