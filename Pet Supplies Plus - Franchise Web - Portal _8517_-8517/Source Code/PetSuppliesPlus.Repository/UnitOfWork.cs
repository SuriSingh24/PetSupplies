using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSuppliesPlus.Data;
using System.Data.Entity;

namespace PetSuppliesPlus.Repository
{
    /// <summary>
    /// Class for intracting with the database
    /// </summary>
    public class EfUnitOfWork : dbPetSupplies_8517, IUnitOfWork
    {
        #region Private memeber

        IRepository<User> _RepoUser;
        IRepository<Store> _RepoStore;
        IRepository<Market> _RepoMarket;
        IRepository<UserStore> _RepoUserStore;
        IRepository<ADOption> _RepoAdOption;

        IRepository<Coupon> _RepoCoupon { get; }
        //IRepository<CouponAdMonth> _RepoCouponAdMonth { get; }
        IRepository<AdMonth> _RepoAdMonth { get; }
        IRepository<StoreAdChoice> _RepoStoreAdChoice { get; }
        IRepository<LoginHistory> _RepoLoginHistory { get; }
        IRepository<AllowedStoreOption> _RepoAllowedStoreOption { get; }
        IRepository<StoreAdChoiceHistory> _RepoStoreAdChoiceHistory { get; }
        IRepository<ExceptionReport> _RepoExceptionReport { get; }
        IRepository<Document> _RepoDocument { get; }
        #endregion

        #region Constructor
        public EfUnitOfWork()
        {
            _RepoUser = new Repository<User>(Users);
            _RepoStore = new Repository<Store>(Stores);
            _RepoMarket = new Repository<Market>(Markets);
            _RepoUserStore = new Repository<UserStore>(UserStores);
            _RepoAdOption = new Repository<ADOption>(ADOptions);

            _RepoCoupon = new Repository<Coupon>(Coupons);
          //  _RepoCouponAdMonth = new Repository<CouponAdMonth>(CouponAdMonths);
            _RepoAdMonth = new Repository<AdMonth>(AdMonths);
            _RepoStoreAdChoice = new Repository<StoreAdChoice>(StoreAdChoices);
            _RepoLoginHistory = new Repository<LoginHistory>(LoginHistories);
            _RepoAllowedStoreOption = new Repository<AllowedStoreOption>(AllowedStoreOptions);
            _RepoStoreAdChoiceHistory = new Repository<StoreAdChoiceHistory>(StoreAdChoiceHistories);
            _RepoExceptionReport = new Repository<ExceptionReport>(ExceptionReports);
            _RepoDocument = new Repository<Document>(Documents);
        }

        #endregion

        #region IUnitOfWork Implementation
        public IRepository<User> RepoUser { get { return _RepoUser; } }
        public IRepository<Store> RepoStore { get { return _RepoStore; } }
        public IRepository<Market> RepoMarket { get { return _RepoMarket; } }
        public IRepository<UserStore> RepoUserStore { get { return _RepoUserStore; } }
        public IRepository<ADOption> RepoAdOption { get { return _RepoAdOption; } }

        public IRepository<Coupon> RepoCoupon { get { return _RepoCoupon; } }
        //public IRepository<CouponAdMonth> RepoCouponAdMonth { get { return _RepoCouponAdMonth; } }
        public IRepository<AdMonth> RepoAdMonth { get { return _RepoAdMonth; } }
        public IRepository<StoreAdChoice> RepoStoreAdChoice { get { return _RepoStoreAdChoice; } }
        public IRepository<LoginHistory> RepoLoginHistory { get { return _RepoLoginHistory; } }
        public IRepository<AllowedStoreOption> RepoAllowedStoreOption { get { return _RepoAllowedStoreOption; } }
        public IRepository<StoreAdChoiceHistory> RepoStoreAdChoiceHistory { get { return _RepoStoreAdChoiceHistory; } }
        public IRepository<ExceptionReport> RepoExceptionReport { get { return _RepoExceptionReport; } }
        public IRepository<Document> RepoDocument { get { return _RepoDocument; } }
        public void Commit()
        {
            this.SaveChanges();
        }
        #endregion

    }
}



