using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;

namespace WordsToMemory
{
    public partial class FWords : Form
    {
        private string imageName;
        private DataTable dt;
        private string pathToImages;
        public FWords()
        {
            InitializeComponent();
        }

        private void FWords_Load(object sender, EventArgs e)
        {
            this.pathToImages = AppDomain.CurrentDomain.BaseDirectory + "images";

            //DataSet ds = new DataSet();
            DataTable dt = new DataTable("Words");

            dt.Columns.Add("English", typeof(String));
            dt.Columns.Add("Translate", typeof(String));
            dt.Columns.Add("Image", typeof(String));

            this.dt = dt;

            DataGridViewTextBoxColumn dgtbcText = new DataGridViewTextBoxColumn();
            dgtbcText.HeaderText = "Text";

            DataGridViewTextBoxColumn dgtbcTranslate = new DataGridViewTextBoxColumn();
            dgtbcTranslate.HeaderText = "Translate";

            DataGridViewTextBoxColumn dgtbcImage = new DataGridViewTextBoxColumn();
            dgtbcImage.HeaderText = "Image";

            /*dgvWords.Columns.Add(dgtbcText);
            dgvWords.Columns.Add(dgtbcTranslate);
            dgvWords.Columns.Add(dgtbcImage);
            dgvWords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWords.RowTemplate.Height = 20;
            dgvWords.AllowUserToAddRows = false;*/
            dgvWords.DataSource = this.dt;
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog optImage = new OpenFileDialog();
            optImage.Filter = "Choice file (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (optImage.ShowDialog() == DialogResult.OK)
            {
                picImage.Image = Image.FromFile(optImage.FileName);
                this.imageName = optImage.SafeFileName;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(rtbText.Text.Trim() == "")
            {
                MessageBox.Show("Text field is required");
            } else
            {
                DataRow dr      = this.dt.NewRow();
                dr["English"]   = WebUtility.HtmlEncode(rtbText.Text);
                dr["Translate"] = WebUtility.HtmlEncode(rtbTranslate.Text);
                dr["Image"]     = this.imageName;

                this.dt.Rows.Add(dr);

                rtbText.Text = "";
                rtbTranslate.Text = "";
                this.imageName = "";
                picImage.Image = null;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //dgvWords.Rows.RemoveAt(dgvWords.CurrentCell.RowIndex);
            //rtbText.SelectedText.
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dgvWords.RowCount > 0)
            {
                SaveFileDialog saveExcelDialog = new SaveFileDialog();
                saveExcelDialog.Filter = "XML File|*.xml";
                saveExcelDialog.Title = "XML file save";
                saveExcelDialog.ShowDialog();
                if(saveExcelDialog.FileName != "")
                {
                    this.dt.WriteXml(saveExcelDialog.FileName, XmlWriteMode.WriteSchema);  
                }
            }
        }

        private void dgvWords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if(i < 0)
            {
                return;
            }

            DataGridViewRow row = dgvWords.Rows[i];
            rtbText.Text        = WebUtility.HtmlDecode( row.Cells[0].Value.ToString() );
            rtbTranslate.Text   = WebUtility.HtmlDecode( row.Cells[1].Value.ToString() );
            string image        = row.Cells[2].Value.ToString();

            if (image != "")
            {   
                picImage.Image  = Image.FromFile(System.IO.Path.Combine(this.pathToImages, image));
                this.imageName  = image;
            } else
            {
                picImage.Image = null;
                this.imageName = "";
            }
            btnUpdate.Enabled   = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int i = dgvWords.CurrentRow.Index;
            DataGridViewRow row  = dgvWords.Rows[i];

            row.Cells[0].Value  = WebUtility.HtmlEncode(rtbText.Text);
            row.Cells[1].Value  = WebUtility.HtmlEncode(rtbTranslate.Text);
            row.Cells[2].Value  = this.imageName;
            btnUpdate.Enabled   = false;
        }
    }
}
