using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog())
            {

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataTable dt;
                    dt = new DataTable();
                    //dt.Columns.Add("Number", typeof(int));
                    dt.Columns.Add("Timestamp", typeof(DateTime));
                    dt.Columns.Add("Type", typeof(string));
                    dt.Columns.Add("Worker", typeof(string));
                    dt.Columns.Add("Method", typeof(string));
                    dt.Columns.Add("Text", typeof(string));
                    dt.Columns.Add("Importance", typeof(double));
                    //dt.Columns.Add("PerformanceFlag", typeof(double));

                    NumberFormatInfo numberFormatInfo = new NumberFormatInfo()
                    {
                        NumberDecimalSeparator = ".",
                    };

                    using (Microsoft.VisualBasic.FileIO.TextFieldParser csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(ofd.FileName))
                    {
                        csvReader.SetDelimiters(new string[] { "," });
                        csvReader.HasFieldsEnclosedInQuotes = true;
                        string[] fieldData = csvReader.ReadFields(); // игнорируем 1 строку
                        while (!csvReader.EndOfData)
                        {
                            fieldData = csvReader.ReadFields();
                            dt.Rows.Add(fieldData[1], fieldData[2], fieldData[3], fieldData[4], fieldData[5], double.Parse(fieldData[6], numberFormatInfo));
                        }
                    }
                    dvgData.ItemsSource = dt.DefaultView;
                    //System.Windows.Forms.MessageBox.Show("Iam hear!");
                    RowsColor();
                    //System.Windows.Forms.MessageBox.Show("Iam finish!");
                }

            }
        }


        public void RowsColor()
        {
            for (int i = 0; i < dvgData.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dvgData.ItemContainerGenerator.ContainerFromIndex(i);
                if (row != null)
                {
                    TextBlock tbl = dvgData.Columns[5].GetCellContent(row) as TextBlock;
                    double val = Convert.ToDouble(tbl.Text);
                    if (val < 0)
                    {

                        SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 104, 0));
                        row.Background = brush;
                    }
                    else if (val >= 5 && val < 10)
                    {
                        SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 232, 0));
                        row.Background = brush;
                    }
                    else if (val >= 10 && val < 15)
                    {
                        SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 67, 0));
                        row.Background = brush;
                    }
                    else
                    {
                        SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 255, 45, 0));
                        row.Background = brush;
                    }
                }
            }
            dvgData.Items.Refresh();
        }

        private void btn_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnNextClick(object sender, RoutedEventArgs e)
        {

        }
    }
}