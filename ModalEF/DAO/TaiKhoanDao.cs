using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModalEF.EF;
using PagedList;

namespace ModalEF.DAO
{
    public class TaiKhoanDao
    {
        ContextDB db = null;
        public TaiKhoanDao()
        {
            db = new ContextDB();
        }

        public TaiKhoan GetById(string userMail)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.Email.Contains(userMail));
        }

        public TaiKhoan ViewDetail(int id)
        {
            return db.TaiKhoans.Find(id);
        }

        //Đăng nhập
        public int Login(string userMail, string passWord)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.Email.Contains(userMail));
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.MatKhau.Contains(passWord))
                { return 1; }
                else
                { return 0; }
            }
        }

        //Phân trang
        public IEnumerable<TaiKhoan> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Ten.Contains(searchString));
            }

            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }

        //Sửa
        public bool Update(TaiKhoan entity)
        {
            try
            {
                var taiKhoan = db.TaiKhoans.Find(entity.MaTaiKhoan);
                taiKhoan.Ten = entity.Ten;
                if (!string.IsNullOrEmpty(entity.MatKhau))
                {
                    taiKhoan.MatKhau = entity.MatKhau;
                }
                taiKhoan.MaSinhVien = entity.MaSinhVien;
                taiKhoan.Ten = entity.Ten;
                taiKhoan.GioiTinh = entity.GioiTinh;
                taiKhoan.SDT = entity.SDT;
                taiKhoan.NgaySinh = entity.NgaySinh;
                taiKhoan.AnhDaiDien = entity.AnhDaiDien;
                taiKhoan.AnhTheSV = entity.AnhTheSV;
                taiKhoan.TrangThai = entity.TrangThai;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Lỗi đăng nhập
                return false;
            }

        }

        //Thêm
        public long Insert(TaiKhoan taiKhoan)
        {
            db.TaiKhoans.Add(taiKhoan);
            db.SaveChanges();
            return taiKhoan.MaTaiKhoan;
        }

        //Xoá
        public bool Delete(int id)
        {
            try
            {
                var nv = db.TaiKhoans.Find(id);
                db.TaiKhoans.Remove(nv);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //Đổi trạng thái
        public bool ChangeStatus(int id)
        {
            var taiKhoan = db.TaiKhoans.Find(id);
            taiKhoan.TrangThai = !taiKhoan.TrangThai;
            db.SaveChanges();
            return taiKhoan.TrangThai;
        }
    }
}
