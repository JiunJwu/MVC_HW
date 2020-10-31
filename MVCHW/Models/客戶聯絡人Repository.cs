using System;
using System.Linq;
using System.Collections.Generic;

namespace MVCHW.Models
{
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.isDeleted == false);
        }
        public override void Delete(客戶聯絡人 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.isDeleted = true;
        }
        public 客戶聯絡人 Get單一筆客戶聯絡人資料(int ID)
        {
            return this.All().FirstOrDefault(p => p.Id == ID);
        }
        public Boolean 確認信箱不重複(客戶聯絡人 客戶聯絡人)
        {
            var data= base.All().Where(p => p.Email == 客戶聯絡人.Email);
            if (data.Count()>1 )
            {
                return false;
            }
            return true;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}