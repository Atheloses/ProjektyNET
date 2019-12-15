using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DesktopApp
{
    public partial class AppForm : Form
    {
        #region ctor

        public AppForm()
        {
            LoadFromXML();
            InitializeComponent();
            RefreshTable();
            this.FormClosing += AppForm_FormClosing;
        }

        #endregion ctor

        #region Properties

        private TableLayoutPanel _Table;
        public TableLayoutPanel Table{
            get
            {
                return _Table;
            }
            set
            {
                if (_Table != null)
                    Controls.Remove(_Table);

                _Table = value;
                Controls.Add(_Table);
            }
        }

        #endregion Properties

        #region Public

        public void RefreshTable()
        {

            DirectoryInfo pluginsDirectory;
            string pluginsPath = Path.GetFullPath("./Plugins/");

            if (Directory.Exists(pluginsPath))
                pluginsDirectory = new DirectoryInfo(pluginsPath);
            else
                pluginsDirectory = Directory.CreateDirectory(pluginsPath);

            List<Type> plugins = new List<Type>();
            var files = pluginsDirectory.GetFiles("*.dll");

            Assembly assembly;
            Type[] types;
            foreach (var file in files)
            {
                try
                {
                    assembly = Assembly.LoadFile(file.FullName);
                    types = assembly.GetTypes();
                }
                catch
                {
                    continue;
                }

                //foreach (var type in types)
                //    if (typeof(IService).IsAssignableFrom(type))
                //        plugins.Add(type);

                plugins.AddRange(types.Where(i => typeof(IService).IsAssignableFrom(i)));
            }

            Table = RefreshTable(plugins);
        }

        #endregion Public

        #region Private

        private Dictionary<string, string> _Plugins;

        private string XMLName = "settings.xml";

        private void LoadFromXML()
        {
            _Plugins = new Dictionary<string, string>();

            using IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
            if (storage.FileExists(XMLName))
            {
                using FileStream fs = storage.OpenFile(XMLName, FileMode.Open);
                using (StreamReader sw = new StreamReader(fs))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(sw.ReadToEnd());
                    foreach(XmlNode firstLayer in xml.ChildNodes)
                    {
                        if(firstLayer.Name == "settings")
                        {
                            foreach (XmlNode secondLayer in firstLayer.ChildNodes)
                            {
                                if (secondLayer.Name == "plugins")
                                {
                                    foreach (XmlNode thirdLayer in secondLayer.ChildNodes)
                                    {
                                        if (thirdLayer.Name == "plugin")
                                        {
                                            string name = string.Empty, time = string.Empty;

                                            foreach(XmlNode plugin in thirdLayer)
                                            {
                                                if (plugin.Name == "name")
                                                    name = plugin.InnerText;
                                                if (plugin.Name == "time")
                                                    time = plugin.InnerText;
                                            }

                                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(time))
                                                if (!_Plugins.ContainsKey(name))
                                                    _Plugins.Add(name, time);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SaveToXML()
        {
            using IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
            if (storage.FileExists(XMLName))
                storage.DeleteFile(XMLName);
            using FileStream fs = storage.OpenFile(XMLName, FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);

                XmlNode settingsNode = doc.CreateElement("settings");
                doc.AppendChild(settingsNode);

                XmlNode pluginsNode = doc.CreateElement("plugins");
                settingsNode.AppendChild(pluginsNode);

                foreach(var item in _Plugins)
                {
                    XmlNode pluginNode = doc.CreateElement("plugin");
                    pluginsNode.AppendChild(pluginNode);

                    XmlNode nameNode = doc.CreateElement("name");
                    nameNode.AppendChild(doc.CreateTextNode(item.Key));
                    pluginNode.AppendChild(nameNode);

                    XmlNode timeNode = doc.CreateElement("time");
                    timeNode.AppendChild(doc.CreateTextNode(item.Value));
                    pluginNode.AppendChild(timeNode);
                }

                doc.Save(sw);
            }
        }

        private void Refresh_click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToXML();
        }

        #endregion Private

    }
}
