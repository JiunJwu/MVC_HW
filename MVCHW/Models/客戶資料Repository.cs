using System;
using System.Linq;
using System.Collections.Generic;

namespace MVCHW.Models
{
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Get單一筆客戶資料(int ID)
        {
            return this.All().FirstOrDefault(p => p.Id == ID);
        }
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.isDeleted == false);
        }
        public override void Delete(客戶資料 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.isDeleted = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}