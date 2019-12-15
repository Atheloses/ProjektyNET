using DesktopAppVIS.DTOApp;

namespace DesktopAppVIS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.uzivatelAppBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Zalozky = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uživatelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailUživateleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ukolyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vytvořitNovýÚkolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prioritaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.uzivatelAppBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.DataSource = this.uzivatelAppBindingSource;
            this.comboBox1.DisplayMember = "Prezdivka";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(282, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(235, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.ValueMember = "ID";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // uzivatelAppBindingSource
            // 
            this.uzivatelAppBindingSource.DataSource = typeof(UzivatelApp);
            // 
            // Zalozky
            // 
            this.Zalozky.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.Zalozky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Zalozky.Location = new System.Drawing.Point(4, 28);
            this.Zalozky.Name = "Zalozky";
            this.Zalozky.SelectedIndex = 0;
            this.Zalozky.Size = new System.Drawing.Size(513, 502);
            this.Zalozky.TabIndex = 2;
            this.Zalozky.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uživatelToolStripMenuItem,
            this.ukolyToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(4, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2);
            this.menuStrip1.Size = new System.Drawing.Size(155, 27);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uživatelToolStripMenuItem
            // 
            this.uživatelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailUživateleToolStripMenuItem});
            this.uživatelToolStripMenuItem.Name = "uživatelToolStripMenuItem";
            this.uživatelToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.uživatelToolStripMenuItem.Size = new System.Drawing.Size(60, 23);
            this.uživatelToolStripMenuItem.Text = "Uživatel";
            // 
            // detailUživateleToolStripMenuItem
            // 
            this.detailUživateleToolStripMenuItem.Name = "detailUživateleToolStripMenuItem";
            this.detailUživateleToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.detailUživateleToolStripMenuItem.Text = "Detail uživatele";
            this.detailUživateleToolStripMenuItem.Click += new System.EventHandler(this.DetailUzivateleToolStripMenuItem_Click);
            // 
            // ukolyToolStripMenuItem
            // 
            this.ukolyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vytvořitNovýÚkolToolStripMenuItem});
            this.ukolyToolStripMenuItem.Name = "ukolyToolStripMenuItem";
            this.ukolyToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.ukolyToolStripMenuItem.Text = "Ukoly";
            // 
            // vytvořitNovýÚkolToolStripMenuItem
            // 
            this.vytvořitNovýÚkolToolStripMenuItem.Name = "vytvořitNovýÚkolToolStripMenuItem";
            this.vytvořitNovýÚkolToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.vytvořitNovýÚkolToolStripMenuItem.Text = "Vytvořit nový úkol";
            this.vytvořitNovýÚkolToolStripMenuItem.Click += new System.EventHandler(this.NovyUkolToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prioritaToolStripMenuItem});
            this.testToolStripMenuItem.Name = "skupinyToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 23);
            this.testToolStripMenuItem.Text = "Skupiny";
            // 
            // prioritaToolStripMenuItem
            // 
            this.prioritaToolStripMenuItem.Name = "novaSkupinaToolStripMenuItem";
            this.prioritaToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.prioritaToolStripMenuItem.Text = "Nová skupina";
            this.prioritaToolStripMenuItem.Click += new System.EventHandler(this.NovaSkupinaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 531);
            this.Controls.Add(this.Zalozky);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Úkolníček";
            ((System.ComponentModel.ISupportInitialize)(this.uzivatelAppBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabControl Zalozky;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uživatelToolStripMenuItem;
        private System.Windows.Forms.BindingSource uzivatelAppBindingSource;
        private System.Windows.Forms.ToolStripMenuItem detailUživateleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ukolyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vytvořitNovýÚkolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prioritaToolStripMenuItem;

        #endregion
    }
}

