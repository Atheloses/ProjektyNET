using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DesktopApp
{
    partial class AppForm
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
            Label label = new Label() { Text = "Načíst knihovny, reflexí", AutoSize = true };
            label.Location = new System.Drawing.Point(5, 12);
            Controls.Add(label);

            Button Refresh = new Button { Text = "Refresh", AutoSize = true};
            Refresh.Click += new System.EventHandler(this.Refresh_click);
            Refresh.Location = new System.Drawing.Point(125, 5);
            Controls.Add(Refresh);


            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "AppForm";
            this.Text = "AT.NET Desktop Application";
            this.ResumeLayout(false);
        }

        private TableLayoutPanel RefreshTable(IEnumerable<Type> p_Services)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                RowCount = p_Services.Count(),
                ColumnCount = 5
            };
            table.Height = 35;
            table.AutoSize = true;
            table.AutoScroll = true;
            table.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            table.Location = new System.Drawing.Point(0, 35);


            int row = 0;
            foreach (var service in p_Services)
            {
                object instance = Activator.CreateInstance(service);
                string serviceName = service.GetProperty("ServiceName").GetValue(instance, null).ToString();
                Label label = new Label() { Text = serviceName, AutoSize = true };
                label.Margin = new Padding(0, 8, 0, 0);

                Button Spustit = new Button { Text = "Spustit", AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
                Spustit.Click += (o, e) => Trace.WriteLine($"Spustit - {serviceName}");
                Spustit.Click += (o, e) => service.GetMethod("Start").Invoke(instance, new object[] { });

                Button Pozastavit = new Button { Text = "Pozastavit", AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
                Pozastavit.Click += (o, e) => Trace.WriteLine($"Pozastavit - {serviceName}");
                Pozastavit.Click += (o, e) => service.GetMethod("Pause").Invoke(instance, new object[] { });

                Button Zastavit = new Button { Text = "Zastavit", AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
                Zastavit.Click += (o, e) => Trace.WriteLine($"Zastavit - {serviceName}");
                Zastavit.Click += (o, e) => service.GetMethod("Stop").Invoke(instance, new object[] { });

                string opakovani = string.Empty;
                if (_Plugins.ContainsKey(serviceName))
                    opakovani = _Plugins[serviceName];

                TextBox OpakovatMs = new TextBox {Text = opakovani, AutoSize = true, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
                OpakovatMs.TextChanged += (o, e) => ValueChanged(serviceName,o);

                int column = 0;
                table.Controls.Add(label, column++, row);
                table.Controls.Add(Spustit, column++, row);
                table.Controls.Add(Pozastavit, column++, row);
                table.Controls.Add(Zastavit, column++, row);
                table.Controls.Add(OpakovatMs, column++, row);
                row++;
            }

            return table;
        }

        #endregion

        private void ValueChanged(string p_PluginName,object p_Object)
        {
            if (p_Object != null)
            {
                TextBox txtBox = p_Object as TextBox;
                if (txtBox != null)
                {
                    _Plugins.TryGetValue(p_PluginName, out string value);
                    if (value == default(string))
                        _Plugins.Add(p_PluginName, txtBox.Text);
                    else
                        _Plugins[p_PluginName] = txtBox.Text;
                }
            }

        }

    }
}

