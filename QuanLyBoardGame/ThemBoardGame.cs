using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBoardGame
{
    public partial class ThemBoardGame : Form
    {
        static MongoClient client = new MongoClient();
        // static MongoClient client = new MongoClient("mongodb+srv://cnpm:Thuydiem29@cluster0.2jmsamm.mongodb.net/");
        static IMongoDatabase db = client.GetDatabase("BoardGame");
        static IMongoCollection<ThongTinBG> collection_BG = db.GetCollection<ThongTinBG>("BoardGame");
        static IMongoCollection<BoardGame> collection_G = db.GetCollection<BoardGame>("Game");
        static IMongoCollection<KhachHang> collection_KH = db.GetCollection<KhachHang>("KhachHang");
        static IMongoCollection<UuDai> collection_UD = db.GetCollection<UuDai>("UuDai");
        static IMongoCollection<DonHang> collection_DH = db.GetCollection<DonHang>("DonHang");
        static IMongoCollection<CTDonHang> collection_CTDH = db.GetCollection<CTDonHang>("CTDonHang");
        static IMongoCollection<CTBaoCao> collection_CTBC = db.GetCollection<CTBaoCao>("CTBaoCao");
        static IMongoCollection<BaoCao> collection_BC = db.GetCollection<BaoCao>("BaoCao");
        static IMongoCollection<LoaiBG> collection_LBG = db.GetCollection<LoaiBG>("LoaiBG");
        static IMongoCollection<BienBan> collection_BB = db.GetCollection<BienBan>("BienBan");
        static IMongoCollection<LoaiPhat> collection_LP = db.GetCollection<LoaiPhat>("LoaiPhat");
        private ThongTinBG ttbg;
        public ThemBoardGame()
        {
            InitializeComponent();
            List<LoaiBG> listLBGs = collection_LBG.AsQueryable().ToList<LoaiBG>();
            cbTheLoai.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var lbg in listLBGs)
            {
                cbTheLoai.Items.Add(lbg.TenLBG);
            }
            tbSoLuong.ReadOnly = true;
            tbTinhTrang.ReadOnly = true;
            tbMaThongTin.ReadOnly = true;
        }

        internal ThemBoardGame(ThongTinBG ttbg)
        {
            InitializeComponent();
            if (ttbg != null)
            {
                this.ttbg = ttbg;
                HienThiTTBG();
            }
            List<LoaiBG> listLBGs = collection_LBG.AsQueryable().ToList<LoaiBG>();
            cbTheLoai.Items.Clear(); // Xóa các phần tử hiện có trong combobox trước khi thêm mới
            foreach (var lbg in listLBGs)
            {
                cbTheLoai.Items.Add(lbg.TenLBG);
            }
            tbSoLuong.ReadOnly = true;
            tbTinhTrang.ReadOnly = true;
            tbMaThongTin.ReadOnly = true;
        }

        public void HienThiTTBG()
        {


            tbMaThongTin.Text = ttbg.MaTTBG.ToString();
            tbTenBoardGame.Text = ttbg.TenBoardGame;
            nudSoNguoiChoi.Text = ttbg.SoNguoiChoi.ToString();
            tbDoTuoi.Text = ttbg.DoTuoi.ToString();
            tbTriGia.Text = ttbg.TriGia.ToString();
            tbGiaThue.Text = ttbg.GiaThue.ToString();
            tbSoLuong.Text = ttbg.SoLuong.ToString();
            tbLuatChoi.Text = ttbg.LuatChoi.ToString();
            tbLinkAnh.Text =ttbg.HinhAnh.ToString();
            tbThoiGianChoi.Text = ttbg.ThoiGianChoi.ToString();
            string imageURL = ttbg.HinhAnh.ToString(); // Lấy đường dẫn URL từ ô Cells[7]

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    // Tải hình ảnh từ đường dẫn URL
                    byte[] imageData = webClient.DownloadData(imageURL);

                    // Chuyển đổi dữ liệu thành đối tượng Image
                    using (var ms = new System.IO.MemoryStream(imageData))
                    {
                        Image image = Image.FromStream(ms);

                        // Gán hình ảnh vào PictureBox
                        pbHinhanh.Image = image;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                MessageBox.Show("Đã xảy ra lỗi khi tải hình ảnh: " + ex.Message);
            }

            var thongTinLBGquery = Builders<LoaiBG>.Filter.Eq("MaLBG", ttbg.MaLBG);
            List<LoaiBG> filteredLBGs = collection_LBG.Find(thongTinLBGquery).ToList();
            if (filteredLBGs.Count > 0)
            {
                LoaiBG lbg = filteredLBGs[0];
                cbTheLoai.Text = lbg.TenLBG;
            }
            bThemTT.Text = "Sửa";
            if (ttbg.SoLuong != 0)
            {
                var tinhTrangMuonOptions = new List<string> { "Đang thuê", "Đang giữ hàng" };
                var tinhTrangBGOptions = new List<string> { "Hỏng" };
                var thongTinBGquery = Builders<BoardGame>.Filter.And(
                    Builders<BoardGame>.Filter.Or(
                        Builders<BoardGame>.Filter.In("TinhTrangMuon", tinhTrangMuonOptions),
                        Builders<BoardGame>.Filter.In("TinhTrangBG", tinhTrangBGOptions)
                    ),
                    Builders<BoardGame>.Filter.Eq("MaTTBG", ttbg.MaTTBG)
                );

                List<BoardGame> filteredBGs = collection_G.Find(thongTinBGquery).ToList();

                if (filteredBGs.Count < ttbg.SoLuong)
                {
                    tbTinhTrang.Text = "Còn hàng";
                }
                else
                { if (filteredBGs.Count == ttbg.SoLuong)
                    {
                        tbTinhTrang.Text = "Hết hàng";
                    }
                }
            }
            else
            {
                tbTinhTrang.Text = "Hết hàng";
            }
        }

        bool isImageChanged = false;
        private void bThemTT_Click(object sender, EventArgs e)
        {
            if (bThemTT.Text == "Sửa")
            {
                if (
                tbTenBoardGame.Text != "" &&
                nudSoNguoiChoi.Text != "" &&
                tbDoTuoi.Text != "" &&
                tbTriGia.Text != "" &&
                tbGiaThue.Text != "" &&
                cbTheLoai.SelectedIndex != -1 &&
                tbLuatChoi.Text != "" &&
                tbThoiGianChoi.Text != "" && tbLinkAnh.Text != "")
                {


                    var thongTinLBGquery = Builders<LoaiBG>.Filter.Eq("TenLBG", cbTheLoai.Text);
                    List<LoaiBG> filteredLBGs = collection_LBG.Find(thongTinLBGquery).ToList();
                    LoaiBG lbg = filteredLBGs[0];
                    //if (pbHinhanh.ImageLocation != null)
                    //{
                        // Đường dẫn của ảnh nếu đã được tải từ một đường dẫn cụ thể
                        string imageURL = tbLinkAnh.Text;

                        // Lưu đường dẫn ảnh vào cơ sở dữ liệu
                        var updateDef = Builders<ThongTinBG>.Update.Set("TenBoardGame", tbTenBoardGame.Text).Set("SoNguoiChoi", nudSoNguoiChoi.Text).Set("DoTuoi", tbDoTuoi.Text).Set("TriGia", tbTriGia.Text).Set("GiaThue", tbGiaThue.Text).Set("SoLuong", tbSoLuong.Text).Set("MaLBG", lbg.MaLBG).Set("HinhAnh", imageURL);
                        collection_BG.UpdateOneAsync(ttbg => ttbg.MaTTBG == ObjectId.Parse(tbMaThongTin.Text), updateDef);
                        MessageBox.Show("Cập nhật thông tin Board Game thành công!");
                        this.Hide();

                   // }
                   /* else
                    {
                        // Xử lý trường hợp ảnh đã được tải từ máy tính


                        if (isImageChanged == true)
                        {
                            string imageName = Guid.NewGuid().ToString() + ".jpg"; // Tạo tên ngẫu nhiên cho tệp tin ảnh

                            string rootFolder = Directory.GetCurrentDirectory(); // Đường dẫn đến thư mục gốc của phần mềm

                            string imageFolder = Path.Combine(rootFolder, "Image");

                            // Kiểm tra và tạo thư mục "Image" nếu chưa tồn tại
                            if (!Directory.Exists(imageFolder))
                                if (!Directory.Exists(imageFolder))
                                    if (!Directory.Exists(imageFolder))
                                        if (!Directory.Exists(imageFolder))
                                        {
                                            Directory.CreateDirectory(imageFolder);
                                        }

                            // Cấp quyền ghi cho thư mục "Image"
                            DirectoryInfo imageDirectoryInfo = new DirectoryInfo(imageFolder);
                            DirectorySecurity imageDirectorySecurity = imageDirectoryInfo.GetAccessControl();
                            imageDirectorySecurity.AddAccessRule(new FileSystemAccessRule(
                                new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                                FileSystemRights.Write, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            imageDirectoryInfo.SetAccessControl(imageDirectorySecurity);


                            string imagePath = Path.Combine(imageFolder, imageName); // Đường dẫn đầy đủ của tệp tin ảnh
                            // Lưu tệp tin ảnh từ PictureBox vào thư mục "Image"
                            pbHinhanh.Image.Save(imagePath);
                            string imageURL = imagePath;

                            // Lưu đường dẫn ảnh vào cơ sở dữ liệu
                            var updateDef = Builders<ThongTinBG>.Update.Set("TenBoardGame", tbTenBoardGame.Text).Set("SoNguoiChoi", nudSoNguoiChoi.Text).Set("DoTuoi", tbDoTuoi.Text).Set("TriGia", tbTriGia.Text).Set("GiaThue", tbGiaThue.Text).Set("SoLuong", tbSoLuong.Text).Set("MaLBG", lbg.MaLBG).Set("HinhAnh", imageURL);
                            collection_BG.UpdateOneAsync(ttbg => ttbg.MaTTBG == ObjectId.Parse(tbMaThongTin.Text), updateDef);
                            MessageBox.Show("Cập nhật thông tin Board Game thành công!");
                            this.Hide();
                        }
                        else
                        {
                            var updateDef = Builders<ThongTinBG>.Update.Set("TenBoardGame", tbTenBoardGame.Text).Set("SoNguoiChoi", nudSoNguoiChoi.Text).Set("DoTuoi", tbDoTuoi.Text).Set("TriGia", tbTriGia.Text).Set("GiaThue", tbGiaThue.Text).Set("SoLuong", tbSoLuong.Text).Set("MaLBG", lbg.MaLBG);
                            collection_BG.UpdateOneAsync(ttbg => ttbg.MaTTBG == ObjectId.Parse(tbMaThongTin.Text), updateDef);
                            MessageBox.Show("Cập nhật thông tin Board Game thành công!");
                            this.Hide();
                        }

                    }*/
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin board game!");
                }
            }
            else

            {
                if (
                tbTenBoardGame.Text != "" &&
                nudSoNguoiChoi.Text != "" &&
                tbDoTuoi.Text != "" &&
                tbTriGia.Text != "" &&
                tbGiaThue.Text != "" &&
                cbTheLoai.SelectedIndex != -1 &&
                tbLuatChoi.Text != "" &&
                tbThoiGianChoi.Text != "" && tbLinkAnh.Text != "")
                {
                    if (int.Parse(tbDoTuoi.Text) > 10 && nudSoNguoiChoi.Value >= 2)
                    {
                        var thongTinLBGquery = Builders<LoaiBG>.Filter.Eq("TenLBG", cbTheLoai.Text);
                        List<LoaiBG> filteredLBGs = collection_LBG.Find(thongTinLBGquery).ToList();
                        LoaiBG lbg = filteredLBGs[0];
                        //if (pbHinhanh.Image == null)
                        //{
                            // Kiểm tra xem ảnh có được tải từ máy tính hay từ đường dẫn cụ thể

                            // Đường dẫn của ảnh nếu đã được tải từ một đường dẫn cụ thể
                            string imageURL =tbLinkAnh.Text;

                            // Lưu đường dẫn ảnh vào cơ sở dữ liệu
                            var ttbg = new ThongTinBG(tbTenBoardGame.Text, int.Parse(nudSoNguoiChoi.Text),
                                int.Parse(tbDoTuoi.Text), int.Parse(tbTriGia.Text), int.Parse(tbGiaThue.Text), imageURL,
                               tbLuatChoi.Text, int.Parse(tbThoiGianChoi.Text), lbg.MaLBG);

                            collection_BG.InsertOne(ttbg);

                            // Hiển thị thông báo lưu thành công (tuỳ chỉnh theo nhu cầu)
                            MessageBox.Show("Lưu thông tin Board Game thành công!");
                            this.Hide();

                            // Tiếp tục các thao tác khác sau khi lưu vào cơ sở dữ liệu
                        /*}
                        else
                        {
                            //MessageBox.Show("Nếu không thêm link ảnh thì sẽ bị lỗi hiển thị!");

                            // Xử lý trường hợp ảnh đã được tải từ máy tính
                            string imageName = Guid.NewGuid().ToString() + ".jpg"; // Tạo tên ngẫu nhiên cho tệp tin ảnh

                            string rootFolder = Directory.GetCurrentDirectory(); // Đường dẫn đến thư mục gốc của phần mềm

                            string imageFolder = Path.Combine(rootFolder, "Image");





                            // Kiểm tra và tạo thư mục "Image" nếu chưa tồn tại
                            if (!Directory.Exists(imageFolder))
                            {
                                Directory.CreateDirectory(imageFolder);
                            }

                            // Cấp quyền ghi cho thư mục "Image"
                            DirectoryInfo imageDirectoryInfo = new DirectoryInfo(imageFolder);
                            DirectorySecurity imageDirectorySecurity = imageDirectoryInfo.GetAccessControl();
                            imageDirectorySecurity.AddAccessRule(new FileSystemAccessRule(
                                new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                                FileSystemRights.Write, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            imageDirectoryInfo.SetAccessControl(imageDirectorySecurity);


                            string imagePath = Path.Combine(imageFolder, imageName); // Đường dẫn đầy đủ của tệp tin ảnh

                            // Lưu tệp tin ảnh từ PictureBox vào thư mục "Image"
                            pbHinhanh.Image.Save(imagePath);

                            // Lưu đường dẫn tệp tin ảnh vào cơ sở dữ liệu
                            string imageURL = imagePath;



                            // Lưu đường dẫn ảnh vào cơ sở dữ liệu
                            var ttbg = new ThongTinBG(tbTenBoardGame.Text, int.Parse(nudSoNguoiChoi.Text),
                                int.Parse(tbDoTuoi.Text), int.Parse(tbTriGia.Text), int.Parse(tbGiaThue.Text), imageURL,
                                 tbLuatChoi.Text, int.Parse(tbThoiGianChoi.Text), lbg.MaLBG);

                            collection_BG.InsertOne(ttbg);

                            // Hiển thị thông báo lưu thành công (tuỳ chỉnh theo nhu cầu)
                            MessageBox.Show("Lưu thông tin Board Game thành công!");
                            this.Hide();
                        }*/
                       
                    }
                    else
                    {
                        MessageBox.Show("Độ tuổi không được dưới 10 và số lượng người chơi phải lớn hơn hoặc bằng 2");
                    }
                }
                else
                {

                    MessageBox.Show("Vui lòng nhập đủ thông tin board game");


                }

            }
        }

        private void bMDTT_Click(object sender, EventArgs e)
        {
            if (bThemTT.Text == "Thêm")
            {
                tbMaThongTin.ReadOnly = true;
                tbMaThongTin.Text = "";
                tbTenBoardGame.Text = "";
                nudSoNguoiChoi.Text = "";
                tbDoTuoi.Text = "";
                tbTriGia.Text = "";
                tbGiaThue.Text = "";
                tbSoLuong.Text = "";
                tbLuatChoi.Text = "";
                tbThoiGianChoi.Text = "";
                pbHinhanh.Image = null;
                cbTheLoai.Text = "";
                tbLinkAnh.Text = "";
            }else
            {
                HienThiTTBG();
            }
        }

        /*private void bTaiAnh_Click(object sender, EventArgs e)
        {
            isImageChanged = true;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg, *.png, *.bmp)|*.jpg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    try
                    {
                        // Tải ảnh từ file được chọn
                        Image image = Image.FromFile(selectedFilePath);

                        // Gán ảnh vào PictureBox
                        pbHinhanh.Image = image;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể tải ảnh. Lỗi: " + ex.Message);
                    }
                }
            }
        }*/

        private void tbTriGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho ký tự được hiển thị trong text box
            }
        }
    }
}