using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using efpe.Controller;
using efpe.Model.Entity;

namespace efpe.View.Admin.CRUD.EditItem
{
    public partial class EditItem : Form
    {
        private ItemEntity selectedItem;
        private readonly ItemController _itemController = new ItemController();
        private byte[] imageBytes;

        public EditItem(ItemEntity selectedItem)
        {
            InitializeComponent();
            this.selectedItem = selectedItem;
            InitializeFormData();
            comboBoxVipAtauReguler.Items.Add("VIP");
            comboBoxVipAtauReguler.Items.Add("Reguler");
        }

        private void InitializeFormData()
        {
            if (selectedItem != null)
            {
                txtNomorKomputer.Text = selectedItem.NomorKomputer.ToString();

                DisplayImage(selectedItem.Image);
            }
        }

        private void DisplayImage(byte[] imageBytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image originalImage = Image.FromStream(ms);

                    float aspectRatio = (float)originalImage.Width / originalImage.Height;

                    int maxHeight = 150;
                    int newHeight = (int)(maxHeight / aspectRatio);

                    Bitmap resizedImage = new Bitmap(originalImage, new Size(maxHeight, newHeight));

                    pictureBoxImage.Image = resizedImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void pictureBox14_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void pictureBoxImage_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            openFileDialog1.Title = "Select an Image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxImage.ImageLocation = openFileDialog1.FileName;

                string imagePath = openFileDialog1.FileName;
                imageBytes = File.ReadAllBytes(imagePath);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedItem != null)
                {
                    selectedItem.NomorKomputer = Convert.ToInt32(txtNomorKomputer.Text);

                    selectedItem.Image = imageBytes; 
                    
                    if (comboBoxVipAtauReguler.Text == "VIP")
                    {
                        selectedItem.VipAtauReguler = "VIP";
                    }
                    else if (comboBoxVipAtauReguler.Text == "Reguler")
                    {
                        selectedItem.VipAtauReguler = "Reguler";
                    }
                    else
                    {
                        MessageBox.Show("Pilih jenis VIP atau Reguler.");
                        return;
                    }

                    bool updateSuccess = _itemController.UpdateItem(selectedItem);

                    if (updateSuccess)
                    {
                        MessageBox.Show("Item updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
