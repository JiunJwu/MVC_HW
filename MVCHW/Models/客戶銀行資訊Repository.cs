using System;
using System.Linq;
using System.Collections.Generic;

namespace MVCHW.Models
{
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => p.isDeleted == false);
        }
        public 客戶銀行資訊 Get單一筆客戶銀行資料(int ID)
        {
            return this.All().FirstOrDefault(p => p.客戶Id == ID);
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.isDeleted = true;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}