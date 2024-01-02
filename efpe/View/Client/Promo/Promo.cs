using efpe.Controller;
using efpe.Model.Entity;
using efpe.Repository;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace efpe.View.Client.Promo
{
    public partial class Promo : Form
    {
        private readonly PromoController _promoController = new PromoController();
        private readonly PromoRepository _promoRepository = new PromoRepository();
        private List<PromoEntity> _allItems;
        private List<PromoEntity> _filteredItems;

        public Promo()
        {
            InitializeComponent();
            LoadDataAndCreatePanels();
        }

        private void LoadDataAndCreatePanels()
        {
            _allItems = _promoRepository.GetPromo();
            _filteredItems = new List<PromoEntity>(_allItems);

            if (_allItems != null && _allItems.Count > 0)
            {
                foreach (PromoEntity item in _allItems)
                {
                    Panel newPanel = CreatePanel(item);
                    flowLayoutPanelPromo.Controls.Add(newPanel);
                    AdjustFlowLayoutPanelHeight();
                }
            }
            else
            {
                MessageBox.Show("No data found.");
            }
        }

        private Panel CreatePanel(PromoEntity item)
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new System.Drawing.Size(500, 150);
            SetRoundedPanel(panel, 20);
            Color[] panelColors = { Color.FromArgb(0, 2, 114), Color.FromArgb(52, 22, 119), Color.FromArgb(163, 47, 128) };

            // Use a counter to cycle through the array for each panel
            int colorIndex = flowLayoutPanelPromo.Controls.Count % panelColors.Length;
            panel.BackColor = panelColors[colorIndex];

            Label labelTitle = new Label();
            labelTitle.Text = $"Promo {item.Nomor} untuk durasi {item.Durasi} Jam";
            labelTitle.Location = new System.Drawing.Point(10, 40);
            labelTitle.AutoSize = true;
            labelTitle.ForeColor = Color.White;
            labelTitle.Font = new Font(labelTitle.Font.FontFamily, 9, FontStyle.Regular);

            Label labelDiskon = new Label();
            labelDiskon.Text = $"DISKON SEBESAR {item.Diskon}% !!!";
            labelDiskon.Location = new System.Drawing.Point(10, 10);
            labelDiskon.AutoSize = true;
            labelDiskon.ForeColor = Color.White;
            labelDiskon.Font = new Font(labelDiskon.Font.FontFamily, 14, FontStyle.Bold);



            Label labelExpired = new Label();
            labelExpired.Text = $"Berakhir dalam :\n{item.Expired}";
            labelExpired.Location = new System.Drawing.Point(315, 30);
            labelExpired.AutoSize = true;
            labelExpired.ForeColor = Color.White;
            labelExpired.Font = new Font(labelExpired.Font.FontFamily, 14, FontStyle.Regular);

            Label lineLabel = new Label();
            lineLabel.BackColor = Color.White;
            lineLabel.Size = new System.Drawing.Size(2, panel.Height - 20); // Set the height as needed
            lineLabel.Location = new System.Drawing.Point(285, 10);

            Button buttonPesan = new Button();
            buttonPesan.Text = "Gunakan";
            buttonPesan.Font = new Font(buttonPesan.Font, FontStyle.Bold);
            buttonPesan.Location = new System.Drawing.Point(330, 100);
            buttonPesan.Click += buttonPesan_Click;
            buttonPesan.Tag = item;
            
            buttonPesan.ForeColor = Color.White;
            buttonPesan.Width = 150;
            buttonPesan.Height = 30;
            buttonPesan.AutoSize = false;
            buttonPesan.BackColor = Color.FromArgb(255, 99, 99);
            buttonPesan.FlatAppearance.BorderSize = 0;
            buttonPesan.FlatStyle = FlatStyle.Flat;
            SetRoundedButton(buttonPesan, 20);


            panel.Controls.Add(labelTitle);
            panel.Controls.Add(labelDiskon);
            panel.Controls.Add(lineLabel);
            panel.Controls.Add(labelExpired);
            panel.Controls.Add(buttonPesan);

            return panel;
        }
        private void buttonPesan_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            PromoEntity selectedItem = (PromoEntity)clickedButton.Tag;
            if (selectedItem.KodePromo != null)
            {
                MessageBox.Show($"Kode Promo anda adalah  >> {selectedItem.KodePromo} << Tekan OK untuk lanjut!", "Informasi", MessageBoxButtons.OKCancel);
            }
            else
            {
                MessageBox.Show("PC ini sedang digunakan, Mohon pilih PC lainnya", "Informasi", MessageBoxButtons.OK);
            }
        }
        private void SetRoundedButton(Button button, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(button.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, button.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            Region region = new Region(path);
            button.Region = region;
        }
        private void SetRoundedPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            Region region = new Region(path);
            panel.Region = region;
        }

        private void AdjustFlowLayoutPanelHeight()
        {
            int totalHeight = flowLayoutPanelPromo.Controls.Cast<Control>().Sum(control => control.Height + flowLayoutPanelPromo.Padding.Vertical);
            flowLayoutPanelPromo.Height = totalHeight;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Client.Beranda.Beranda beranda = new View.Client.Beranda.Beranda();
            beranda.Show();
        }
    }
}
