using PetSuppliesPlus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Repository
{
    /// <summary>
    /// Interface for implementing unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {

        IRepository<User> RepoUser { get; }
        IRepository<Store> RepoStore { get; }
        IRepository<Market> RepoMarket { get; }
        IRepository<UserStore> RepoUserStore { get; }
        IRepository<Coupon> RepoCoupon { get; }
        //IRepository<CouponAdMonth> RepoCouponAdMonth { get; }
        IRepository<AdMonth> RepoAdMonth { get; }
        IRepository<StoreAdChoice> RepoStoreAdChoice { get; }
        IRepository<ADOption> RepoAdOption { get; }
        IRepository<LoginHistory> RepoLoginHistory { get; }
        IRepository<AllowedStoreOption> RepoAllowedStoreOption { get; }
        IRepository<StoreAdChoiceHistory> RepoStoreAdChoiceHistory { get; }
        IRepository<ExceptionReport> RepoExceptionReport { get; }
        IRepository<Document> RepoDocument { get; }
        void Commit();
    }
}
