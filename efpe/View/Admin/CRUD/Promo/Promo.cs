using efpe.Controller;
using efpe.Model.Entity;
using efpe.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace efpe.View.Admin.CRUD.Promo
{
    public partial class Promo : Form
    {
        private readonly PromoRepository _promoRepository = new PromoRepository();
        private string currentSortColumn = string.Empty;
        private SortOrder currentSortOrder = SortOrder.Ascending;
        private PromoController _promoController;

        public Promo()
        {
            InitializeComponent();
            _promoController = new PromoController();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            btnTambah.FlatStyle = FlatStyle.Flat;
            btnTambah.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnHapus.FlatStyle = FlatStyle.Flat;
            btnHapus.FlatAppearance.BorderSize = 0;
            SetRoundedButton(btnTambah, 20);
            SetRoundedPictureBox(refreshFormBtn, 10);
            comboBoxSortBy.Items.Add("Nomor");
            comboBoxSortBy.Items.Add("Durasi");
            comboBoxSortBy.Items.Add("Diskon");
            comboBoxSortBy.Items.Add("KodePromo");
            comboBoxSortBy.Items.Add("Id");

            comboBoxSearchBy.Items.Add("Nomor");
            comboBoxSearchBy.Items.Add("Durasi");
            comboBoxSearchBy.Items.Add("Diskon");
            comboBoxSearchBy.Items.Add("KodePromo");
            comboBoxSearchBy.Items.Add("Id");

            comboBoxOrderBy.Items.Add("ASC");
            comboBoxOrderBy.Items.Add("DESC");
            comboBoxOrderBy.SelectedIndex = 0;
        }

        public void Tampil()
        {
            dataGridView1.DataSource = _promoRepository.GetPromo();
        }

        private void CRUD_Load(object sender, EventArgs e)
        {
            Tampil();
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

        private void SetRoundedPictureBox(PictureBox pictureBox, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(pictureBox.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(pictureBox.Width - radius, pictureBox.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, pictureBox.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            Region region = new Region(path);
            pictureBox.Region = region;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Admin.CRUD.Promo.AddPromo.AddPromo form = new View.Admin.CRUD.Promo.AddPromo.AddPromo();
            form.Show();
        }

        private void refreshFormBtn_Click(object sender, EventArgs e)
        {
            Tampil();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void comboBoxSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSortColumn = comboBoxSortBy.SelectedItem.ToString();
            ApplyFilterAndSort();
        }

        private void comboBoxOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSortOrder = comboBoxOrderBy.SelectedItem.ToString() == "ASC" ? SortOrder.Ascending : SortOrder.Descending;
            ApplyFilterAndSort();
        }

        private void comboBoxSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsValidSearchProperty())
            {
                MessageBox.Show("Pencarian tidak ditemukan.", "Search Property Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxSearchBy.SelectedIndex = -1;
            }
            else
            {
                ApplyFilterAndSort();
            }
        }

        private bool IsValidSearchProperty()
        {
            string selectedSearchProperty = comboBoxSearchBy.SelectedItem?.ToString();

            return string.IsNullOrEmpty(selectedSearchProperty) ||
                   selectedSearchProperty == "Nomor" ||
                   selectedSearchProperty == "Durasi" ||
                   selectedSearchProperty == "KodePromo" ||
                   selectedSearchProperty == "Id";
        }

        private void ApplyFilterAndSort()
        {
            string searchKeyword = searchBox.Text.ToLower();
            string searchBy = comboBoxSearchBy.SelectedItem?.ToString();

            var filteredItems = _promoRepository.GetPromo().Where(item =>
            {
                if (string.IsNullOrEmpty(searchBy))
                {
                    return item.Nomor.ToString().Contains(searchKeyword) ||
                           item.Durasi.ToString().Contains(searchKeyword) ||
                           item.Diskon.ToString().Contains(searchKeyword) ||
                           item.KodePromo.ToLower().Contains(searchKeyword) ||
                           item.Id.ToString().Contains(searchKeyword);
                }

                switch (searchBy)
                {
                    case "Nomor":
                        return item.Nomor.ToString().Contains(searchKeyword);
                    case "Durasi":
                        return item.Durasi.ToString().Contains(searchKeyword);
                    case "Diskon":
                        return item.Diskon.ToString().Contains(searchKeyword);
                    case "KodePromo":
                        return item.KodePromo.ToLower().Contains(searchKeyword);
                    case "Id":
                        return item.Id.ToString().Contains(searchKeyword);
                    default:
                        return false;
                }
            }).ToList();

            filteredItems = ApplySorting(filteredItems);

            if (filteredItems.Count == 0)
            {
                MessageBox.Show("Pencarian tidak ditemukan.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dataGridView1.DataSource = filteredItems;
        }

        private List<PromoEntity> ApplySorting(List<PromoEntity> items)
        {
            if (!string.IsNullOrEmpty(currentSortColumn))
            {
                switch (currentSortColumn)
                {
                    case "Nomor":
                        return currentSortOrder == SortOrder.Ascending
                            ? items.OrderBy(item => item.Nomor).ToList()
                            : items.OrderByDescending(item => item.Nomor).ToList();
                    case "Durasi":
                        return currentSortOrder == SortOrder.Ascending
                            ? items.OrderBy(item => item.Durasi).ToList()
                            : items.OrderByDescending(item => item.Durasi).ToList();
                    case "KodePromo":
                        return currentSortOrder == SortOrder.Ascending
                            ? items.OrderBy(item => item.KodePromo).ToList()
                            : items.OrderByDescending(item => item.KodePromo).ToList();
                    case "Id":
                        return currentSortOrder == SortOrder.Ascending
                            ? items.OrderBy(item => item.Id).ToList()
                            : items.OrderByDescending(item => item.Id).ToList();
                    default:
                        return items;
                }
            }
            return items;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                List<PromoEntity> items = _promoRepository.GetPromo();

                if (selectedIndex >= 0 && selectedIndex < items.Count)
                {

                    PromoEntity selecteditem = items[selectedIndex];
                    this.Hide();
                    View.Admin.CRUD.Promo.EditPromo.EditPromo form = new View.Admin.CRUD.Promo.EditPromo.EditPromo(selecteditem);
                    form.Show();
                    Tampil();
                }
            }
            else
            {
                MessageBox.Show("Tolong pilih baris untuk mengedit.", "Edit Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    List<PromoEntity> items = _promoRepository.GetPromo();

                    if (selectedIndex >= 0 && selectedIndex < items.Count)
                    {
                        PromoEntity selectedItem = items[selectedIndex];

                        DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            bool deleteSuccess = _promoController.DeletePromo(selectedItem.Id);

                            if (deleteSuccess)
                            {
                                MessageBox.Show("Item deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Tampil();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Admin.Beranda.Beranda beranda = new View.Admin.Beranda.Beranda();
            beranda.Show();
        }
    }
}
