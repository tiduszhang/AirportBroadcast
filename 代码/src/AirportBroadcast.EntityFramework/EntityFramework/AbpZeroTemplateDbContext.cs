using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using AirportBroadcast.Authorization.Roles;
using AirportBroadcast.Authorization.Users;
using AirportBroadcast.Chat;
using AirportBroadcast.Domain.activeMq;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Domain.playSets;
using AirportBroadcast.Friendships;
using AirportBroadcast.MultiTenancy;
using AirportBroadcast.Storage;


namespace AirportBroadcast.EntityFramework
{
    /* Constructors of this DbContext is important and each one has it's own use case.
     * - Default constructor is used by EF tooling on design time.
     * - constructor(nameOrConnectionString) is used by ABP on runtime.
     * - constructor(existingConnection) is used by unit tests.
     * - constructor(existingConnection,contextOwnsConnection) can be used by ABP if DbContextEfTransactionStrategy is used.
     * See http://www.aspnetboilerplate.com/Pages/Documents/EntityFramework-Integration for more.
     */
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class AbpZeroTemplateDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; }

        #region 基本信息
        public virtual IDbSet<AudioAirLine> AudioAirLines { get; set; }
        public virtual IDbSet<AudioCheckIn> AudioCheckIns { get; set; }
        public virtual IDbSet<AudioCity> AudioCitys { get; set; }
        public virtual IDbSet<AudioConst> AudioConsts { get; set; }
        public virtual IDbSet<AudioDigit> AudioDigits { get; set; }
        public virtual IDbSet<AudioGate> AudioGates { get; set; }
        public virtual IDbSet<AudioHour> AudioHours { get; set; }
        public virtual IDbSet<AudioLanguage> AudioLanguages { get; set; }
        public virtual IDbSet<AudioMinite> AudioMinites { get; set; }
        public virtual IDbSet<AudioReason> AudioReasons { get; set; }

        public virtual IDbSet<AudioTurnPlate> AudioTurnPlates { get; set; }

        public virtual IDbSet<AudioTemplte> AudioTempltes { get; set; }
        public virtual IDbSet<AudioTemplteDetail> AudioTemplteDetails { get; set; }


        #endregion

        #region 设置
        public virtual IDbSet<AudioDevice> AudioDevices { get; set; }

        public virtual IDbSet<TopPwrPort> TopPwrPorts { get; set; }

        public virtual IDbSet<AudioPlaySet> AudioPlaySets { get; set; }

        public virtual IDbSet<AudioPlaySetTemple> AudioPlaySetTemples { get; set; }


        #endregion

        #region 队列消息
        public virtual IDbSet<ReceiveJson> ReceiveJsons { get; set; }

        public virtual IDbSet<AirshowData> AirshowDatas { get; set; }

        public virtual IDbSet<CommAudioFileName> CommAudioFileNames { get; set; }

        public virtual IDbSet<PlayAudioLog> PlayAudioLogs { get; set; }

        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AudioPlaySet>()
                .HasMany<TopPwrPort>(x => x.EnTopPwrPorts).WithMany(x=>x.EnAudioPlaySets);

            modelBuilder.Entity<AudioPlaySet>()
              .HasMany<TopPwrPort>(x => x.CnTopPwrPorts).WithMany(x => x.CnAudioPlaySets);
             
            base.OnModelCreating(modelBuilder);
        }

        public AbpZeroTemplateDbContext()
            : base("Default")
        {

        }

        public AbpZeroTemplateDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public AbpZeroTemplateDbContext(DbConnection existingConnection)
           : base(existingConnection, false)
        {

        }

        public AbpZeroTemplateDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {


        }
    }
}
