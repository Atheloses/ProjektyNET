using DesktopAppVIS.DTOApp;
using DomainLogic;
using DTO;
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
    public class NovyUkol
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

        private TableLayoutPanel TableLayout { get; set; }
        private Label NazevForm { get; set; }
        private TextBox Nazev { get; set; }
        private Label NazevLabel { get; set; }
        private TextBox Popis { get; set; }
        private Label PopisLabel { get; set; }
        private DateTimePicker Termin { get; set; }
        private Label TerminLabel { get; set; }
        private ComboBox Obtiznost { get; set; }
        private Label ObtiznostLabel { get; set; }
        private NumericUpDown Mandays { get; set; }
        private Label MandaysLabel { get; set; }
        private List<UzivatelApp> _UzivateleDGV { get; set; }
        private List<UzivatelApp> UzivateleDGV
        {
            get => _UzivateleDGV;
            set
            {
                _UzivateleDGV = value;
                Uzivatele.DataSource = value;
            }
        }
        private DataGridView Uzivatele { get; set; }
        private TextBox UzivateleSearch { get; set; }
        private Label UzivateleLabel { get; set; }
        private Button Zahodit { get; set; }
        private Button Vytvorit { get; set; }

        public NovyUkol( TabControl p_Zalozky)
        {
            Zalozky = p_Zalozky;
            string title = "Nový úkol     x";
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
            TableLayout = new TableLayoutPanel
            {
                RowCount = 11,
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

            NazevForm = new Label() { Text = "Vytvoření nového úkolu", TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill };

            NazevLabel = new Label() { Text = "Název úkolu:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Nazev = new TextBox();
            Nazev.Validating += new CancelEventHandler(Validating);

            PopisLabel = new Label() { Text = "Popis úkolu:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Popis = new TextBox();

            TerminLabel = new Label() { Text = "Termín splnění úkolu:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Termin = new DateTimePicker();
            Termin.Format = DateTimePickerFormat.Custom;
            Termin.CustomFormat = "dd/MM/yyyy hh:mm:ss";

            ObtiznostLabel = new Label() { Text = "Obtížnost:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Obtiznost = new ComboBox();

            MandaysLabel = new Label() { Text = "Očekávané mandaye:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            Mandays = new NumericUpDown();
            Mandays.Validating += new CancelEventHandler(Validating);

            UzivateleLabel = new Label() { Text = "Vyhledat uživatele:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };
            UzivateleSearch = new TextBox();
            Uzivatele = new DataGridView() { Dock = DockStyle.Fill };
            Uzivatele.MaximumSize = new Size(550, 100);
            Uzivatele.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            Zahodit = new Button { Text = "Zahodit", AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            Zahodit.Click += new System.EventHandler(this.ZahoditUkol);

            Vytvorit = new Button { Text = "Vytvořit", AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            Vytvorit.Click += new System.EventHandler(this.VytvoritUkol);

            TableLayout.Controls.Add(NazevForm, 0, 0);
            TableLayout.SetColumnSpan(NazevForm, 2);
            TableLayout.Controls.Add(NazevLabel, 0, 1);
            TableLayout.Controls.Add(Nazev, 1, 1);
            TableLayout.Controls.Add(PopisLabel, 0, 2);
            TableLayout.Controls.Add(Popis, 1, 2);
            TableLayout.Controls.Add(TerminLabel, 0, 3);
            TableLayout.Controls.Add(Termin, 1, 3);
            TableLayout.Controls.Add(ObtiznostLabel, 0, 4);
            TableLayout.Controls.Add(Obtiznost, 1, 4);
            TableLayout.Controls.Add(MandaysLabel, 0, 5);
            TableLayout.Controls.Add(Mandays, 1, 5);
            TableLayout.Controls.Add(UzivateleLabel, 0, 6);
            TableLayout.Controls.Add(UzivateleSearch, 1, 6);
            TableLayout.Controls.Add(Uzivatele, 0, 7);
            TableLayout.SetColumnSpan(Uzivatele, 2);
            TableLayout.Controls.Add(Zahodit, 1, 10);
            TableLayout.Controls.Add(Vytvorit, 0, 10);
            Zalozka.Controls.Add(TableLayout);
            Zalozky.TabPages.Add(Zalozka);
        }

        private async void FillData()
        {
            Nazev.Text = "";
            Popis.Text = "";
            Termin.Text = DateTime.Now.ToString();
            Obtiznost.Text = "";
            Mandays.Text = "";
            var uzivatele = await new UzivatelDL().ZiskejUzivatele();
            var uzivateleApp = UzivatelApp.GetAppFromDTO(uzivatele);
            uzivateleApp.First(o => o.ID == SelectedUzivatel.ID).Oznacen = true;
            UzivateleDGV = uzivateleApp.OrderBy(o => o.Name).ToList();
            Uzivatele.Columns[0].Visible = false;
            Uzivatele.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var obtiznosti = await new PrioritaDL().ZiskejPriority();
            Obtiznost.DataSource = obtiznosti;
            Obtiznost.DisplayMember = "Nazev";
            Obtiznost.ValueMember = "IDPriorita";
        }

        private void ZahoditUkol(object sender, EventArgs e)
        {
            FillData();
        }

        private void Validating(object sender, CancelEventArgs e)
        {
            if (sender is TextBox _textBox)
                if (!string.IsNullOrEmpty(_textBox.Text.Trim()))
                    _textBox.BackColor = Color.White;
                else
                    _textBox.BackColor = Color.IndianRed;

            if (sender is NumericUpDown _Numeric)
                if (!string.IsNullOrEmpty(_Numeric.Text.Trim()))
                    _Numeric.BackColor = Color.White;
                else
                    _Numeric.BackColor = Color.IndianRed;
        }

        private async void VytvoritUkol(object sender, EventArgs e)
        {
            bool wrong = false;
            if (string.IsNullOrEmpty(Nazev.Text.Trim()))
            {
                Nazev.BackColor = Color.IndianRed;
                wrong = true;
            }

            if (string.IsNullOrEmpty(Mandays.Text.Trim()))
            {
                Mandays.BackColor = Color.IndianRed;
                wrong = true;
            }

            if (wrong)
                return;

            var _Uzivatele = new List<int>();
            for (int i = 0; i < Uzivatele.RowCount; i++)
            {
                if (Convert.ToBoolean(Uzivatele.Rows[i].Cells[3].Value.ToString()))
                    _Uzivatele.Add(Convert.ToInt32(Uzivatele.Rows[i].Cells[0].Value.ToString()));
            }

            var novyUkol = new Ukol()
            {
                Nazev = Nazev.Text,
                Popis = Popis.Text,
                Termin = Convert.ToDateTime(Termin.Text),
                IDPriorita = Convert.ToInt32(Obtiznost.SelectedValue),
                Mandays = Convert.ToInt32(Mandays.Text)
            };

            await new UkolDL().VytvorUkol(novyUkol, _Uzivatele, new List<int>());
        }
    }
}
