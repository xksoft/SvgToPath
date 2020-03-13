using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SvgToPath
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button_Click(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var xml=new XmlDocument();
                xml.LoadXml(TextBox1.Text.Trim());
                var json = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(xml));
                var data = "";
                if (json["svg"]["path"] is JObject)
                {
                   data= json["svg"]["path"]["@d"].ToString();
              
                }

                else
                {
                    var list=new List<string>();
                    foreach (var j in (JArray)json["svg"]["path"])
                    {
                        list.Insert(0,j["@d"].ToString());
                    }
    
                    data =string.Join(" ",list);

                }
                MyPath.Data = Geometry.Parse(data);
                TextBox2.Text =
                    "<Path x:Name=\"MyPath\" Width=\"64\" Height=\"64\" Fill=\"Black\" Stretch=\"Uniform\" Data=\"" +
                    data + "\"/>";

            }
            catch (Exception exception)
            {
                MessageBox.Show("转换出错：" + exception.Message, "出错了");
            }
 
        }
    }
}
