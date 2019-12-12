namespace AirportBroadcast.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AirshowDatas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Airshowtype = c.String(unicode: false),
                        Routeoid = c.String(unicode: false),
                        Shareflightno = c.String(unicode: false),
                        FlightDateTime = c.DateTime(nullable: false, precision: 0),
                        ExecutionDateTime = c.DateTime(nullable: false, precision: 0),
                        FlightNo2 = c.String(unicode: false),
                        FlightNo3 = c.String(unicode: false),
                        AirlineCode2 = c.String(unicode: false),
                        AirlineCode3 = c.String(unicode: false),
                        FlightNum = c.String(unicode: false),
                        FlightMssion = c.String(unicode: false),
                        FlightType = c.String(unicode: false),
                        DepFIV1NO3 = c.String(unicode: false),
                        DepFIV1NO4 = c.String(unicode: false),
                        DepFIV2NO3 = c.String(unicode: false),
                        DepFIV2NO4 = c.String(unicode: false),
                        ArrFIV1NO3 = c.String(unicode: false),
                        ArrFIV1NO4 = c.String(unicode: false),
                        ArrFIV2NO3 = c.String(unicode: false),
                        ArrFIV2NO4 = c.String(unicode: false),
                        Fiv1No3 = c.String(unicode: false),
                        Fiv1No4 = c.String(unicode: false),
                        Fiv2No3 = c.String(unicode: false),
                        Fiv2No4 = c.String(unicode: false),
                        ForgNo3 = c.String(unicode: false),
                        ForgNo4 = c.String(unicode: false),
                        FestNo3 = c.String(unicode: false),
                        FestNo4 = c.String(unicode: false),
                        DepPlanTime = c.DateTime(nullable: false, precision: 0),
                        DepForecastTime = c.DateTime(nullable: false, precision: 0),
                        DepartTime = c.DateTime(nullable: false, precision: 0),
                        ArrPlanTime = c.DateTime(nullable: false, precision: 0),
                        ArrForecastTime = c.DateTime(nullable: false, precision: 0),
                        ArriveTime = c.DateTime(nullable: false, precision: 0),
                        FlightStatus = c.String(unicode: false),
                        Gate = c.String(unicode: false),
                        Gateopentime = c.DateTime(nullable: false, precision: 0),
                        Gateclosetime = c.DateTime(nullable: false, precision: 0),
                        Carousel = c.String(unicode: false),
                        CheckinLoad = c.String(unicode: false),
                        CheckinCounter = c.String(unicode: false),
                        CheckinTimeStart = c.DateTime(nullable: false, precision: 0),
                        CheckinTimeEnd = c.DateTime(nullable: false, precision: 0),
                        FlightCirculationStatus = c.String(unicode: false),
                        FlightAbnormalReason = c.String(unicode: false),
                        Freg = c.String(unicode: false),
                        DeporArrCode = c.String(unicode: false),
                        LocalAirportCode = c.String(unicode: false),
                        Xlsjtime = c.DateTime(nullable: false, precision: 0),
                        Xlxjtime = c.DateTime(nullable: false, precision: 0),
                        RouteType = c.String(unicode: false),
                        Playtime = c.String(unicode: false),
                        Playarea = c.String(unicode: false),
                        Playtype = c.String(unicode: false),
                        Dlytype = c.String(unicode: false),
                        Dlytime = c.DateTime(nullable: false, precision: 0),
                        Counterno = c.String(unicode: false),
                        TableName = c.String(unicode: false),
                        TableSchema = c.String(unicode: false),
                        Ismixed = c.String(unicode: false),
                        FooterSql = c.String(unicode: false),
                        WhereSql = c.String(unicode: false),
                        ClientIp = c.String(unicode: false),
                        ClientMachineName = c.String(unicode: false),
                        GroupSql = c.String(unicode: false),
                        OrderSql = c.String(unicode: false),
                        UserId = c.String(unicode: false),
                        UserName = c.String(unicode: false),
                        IsPrintAll = c.String(unicode: false),
                        Operator = c.String(unicode: false),
                        Rid = c.Long(nullable: false),
                        ReciveTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReceiveJsons", t => t.Rid, cascadeDelete: true)
                .Index(t => t.Rid);
            
            CreateTable(
                "dbo.CommAudioFileNames",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AirshowDataId = c.Long(),
                        FileName = c.String(unicode: false),
                        FileTotalTime = c.Int(nullable: false),
                        Remark = c.String(unicode: false),
                        PlayStatus = c.Int(nullable: false),
                        PlayPort = c.String(unicode: false),
                        DeporArrCode = c.String(unicode: false),
                        StartPlayTime = c.DateTime(precision: 0),
                        EndPlayTime = c.DateTime(precision: 0),
                        TotalPlayTime = c.Int(nullable: false),
                        WaitForPlay = c.DateTime(precision: 0),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AirshowDatas", t => t.AirshowDataId)
                .Index(t => t.AirshowDataId);
            
            CreateTable(
                "dbo.ReceiveJsons",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Content = c.String(unicode: false),
                        ReciveTime = c.DateTime(nullable: false, precision: 0),
                        Remark = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AudioAirLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioAirLine_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        code = c.Int(nullable: false),
                        Name = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioCheckIns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioCheckIn_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioCity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioConsts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConstNo = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioConst_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioDevices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Name = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AudioPlaySets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        AutoPlay = c.Boolean(nullable: false),
                        Remark = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                        TopPwrPort_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioPlaySet_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TopPwrPorts", t => t.TopPwrPort_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.TopPwrPort_Id);
            
            CreateTable(
                "dbo.TopPwrPorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Name = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        AudioPlaySet_Id = c.Int(),
                        AudioPlaySet_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioPlaySets", t => t.AudioPlaySet_Id)
                .ForeignKey("dbo.AudioPlaySets", t => t.AudioPlaySet_Id1)
                .Index(t => t.AudioPlaySet_Id)
                .Index(t => t.AudioPlaySet_Id1);
            
            CreateTable(
                "dbo.AudioPlaySetTemples",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TempleId = c.Int(nullable: false),
                        PlayId = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioPlaySetTemple_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioPlaySets", t => t.PlayId, cascadeDelete: true)
                .ForeignKey("dbo.AudioTempltes", t => t.TempleId, cascadeDelete: true)
                .Index(t => t.TempleId)
                .Index(t => t.PlayId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioTempltes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioTemplte_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioTemplteDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsParamter = c.Boolean(nullable: false),
                        ParamterType = c.Int(),
                        ConstId = c.Int(),
                        TemplteId = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioTemplteDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioConsts", t => t.ConstId)
                .ForeignKey("dbo.AudioTempltes", t => t.TemplteId, cascadeDelete: true)
                .Index(t => t.ConstId)
                .Index(t => t.TemplteId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioDigits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioDigit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioGates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioGate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioHours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioHour_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioMinites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioMinite_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioReasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioReason_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioTurnPlates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Path = c.String(unicode: false),
                        Content = c.String(unicode: false),
                        ContentRemark = c.String(unicode: false),
                        LanguageId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioTurnPlate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AudioLanguages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AbpAuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(maxLength: 256, storeType: "nvarchar"),
                        MethodName = c.String(maxLength: 256, storeType: "nvarchar"),
                        Parameters = c.String(maxLength: 1024, storeType: "nvarchar"),
                        ReturnValue = c.String(unicode: false),
                        ExecutionTime = c.DateTime(nullable: false, precision: 0),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 128, storeType: "nvarchar"),
                        BrowserInfo = c.String(maxLength: 512, storeType: "nvarchar"),
                        Exception = c.String(maxLength: 2000, storeType: "nvarchar"),
                        ImpersonatorUserId = c.Long(),
                        ImpersonatorTenantId = c.Int(),
                        CustomData = c.String(maxLength: 2000, storeType: "nvarchar"),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpBackgroundJobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        JobType = c.String(nullable: false, maxLength: 512, storeType: "nvarchar"),
                        JobArgs = c.String(nullable: false, unicode: false),
                        TryCount = c.Short(nullable: false),
                        NextTryTime = c.DateTime(nullable: false, precision: 0),
                        LastTryTime = c.DateTime(precision: 0),
                        IsAbandoned = c.Boolean(nullable: false),
                        Priority = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.IsAbandoned, t.NextTryTime });
            
            CreateTable(
                "dbo.AppBinaryObjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        Bytes = c.Binary(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BinaryObject_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AppChatMessages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        TenantId = c.Int(),
                        TargetUserId = c.Long(nullable: false),
                        TargetTenantId = c.Int(),
                        Message = c.String(nullable: false, maxLength: 4096, storeType: "nvarchar"),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        Side = c.Int(nullable: false),
                        ReadState = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChatMessage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpFeatures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Value = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                        EditionId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EditionFeatureSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FeatureSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TenantFeatureSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.EditionId);
            
            CreateTable(
                "dbo.AbpEditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AbpEntityChanges",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChangeTime = c.DateTime(nullable: false, precision: 0),
                        ChangeType = c.Byte(nullable: false),
                        EntityChangeSetId = c.Long(nullable: false),
                        EntityId = c.String(maxLength: 48, storeType: "nvarchar"),
                        EntityTypeFullName = c.String(maxLength: 192, storeType: "nvarchar"),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EntityChange_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEntityChangeSets", t => t.EntityChangeSetId, cascadeDelete: true)
                .Index(t => t.EntityChangeSetId)
                .Index(t => new { t.EntityTypeFullName, t.EntityId })
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpEntityPropertyChanges",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityChangeId = c.Long(nullable: false),
                        NewValue = c.String(maxLength: 512, storeType: "nvarchar"),
                        OriginalValue = c.String(maxLength: 512, storeType: "nvarchar"),
                        PropertyName = c.String(maxLength: 96, storeType: "nvarchar"),
                        PropertyTypeFullName = c.String(maxLength: 192, storeType: "nvarchar"),
                        TenantId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EntityPropertyChange_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpEntityChanges", t => t.EntityChangeId, cascadeDelete: true)
                .Index(t => t.EntityChangeId)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpEntityChangeSets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BrowserInfo = c.String(maxLength: 512, storeType: "nvarchar"),
                        ClientIpAddress = c.String(maxLength: 64, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 128, storeType: "nvarchar"),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        ExtensionData = c.String(unicode: false),
                        ImpersonatorTenantId = c.Int(),
                        ImpersonatorUserId = c.Long(),
                        Reason = c.String(maxLength: 256, storeType: "nvarchar"),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EntityChangeSet_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CreationTime, name: "IX_TenantId_CreationTime")
                .Index(t => new { t.TenantId, t.Reason })
                .Index(t => t.TenantId)
                .Index(t => t.UserId, name: "IX_TenantId_UserId");
            
            CreateTable(
                "dbo.AppFriendships",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        TenantId = c.Int(),
                        FriendUserId = c.Long(nullable: false),
                        FriendTenantId = c.Int(),
                        FriendUserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        FriendTenancyName = c.String(unicode: false),
                        FriendProfilePictureId = c.Guid(),
                        State = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Friendship_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Icon = c.String(maxLength: 128, storeType: "nvarchar"),
                        IsDisabled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AbpLanguageTexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        LanguageName = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Source = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Key = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Value = c.String(nullable: false, unicode: false),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NotificationName = c.String(nullable: false, maxLength: 96, storeType: "nvarchar"),
                        Data = c.String(unicode: false),
                        DataTypeName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityTypeName = c.String(maxLength: 250, storeType: "nvarchar"),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityId = c.String(maxLength: 96, storeType: "nvarchar"),
                        Severity = c.Byte(nullable: false),
                        UserIds = c.String(unicode: false),
                        ExcludedUserIds = c.String(unicode: false),
                        TenantIds = c.String(unicode: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AbpNotificationSubscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        NotificationName = c.String(maxLength: 96, storeType: "nvarchar"),
                        EntityTypeName = c.String(maxLength: 250, storeType: "nvarchar"),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityId = c.String(maxLength: 96, storeType: "nvarchar"),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotificationSubscriptionInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId)
                .Index(t => new { t.NotificationName, t.EntityTypeName, t.EntityId, t.UserId });
            
            CreateTable(
                "dbo.AbpOrganizationUnitRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        RoleId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnitRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        ParentId = c.Long(),
                        Code = c.String(nullable: false, maxLength: 95, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.ParentId)
                .Index(t => t.TenantId)
                .Index(t => t.ParentId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AbpPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IsGranted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                        RoleId = c.Int(),
                        UserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RolePermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserPermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AbpRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NormalizedName = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32, storeType: "nvarchar"),
                        DisplayName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        IsStatic = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.TenantId)
                .Index(t => t.IsDeleted)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProfilePictureId = c.Guid(),
                        ShouldChangePasswordOnNextLogin = c.Boolean(nullable: false),
                        NormalizedUserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        NormalizedEmailAddress = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        AuthenticationSource = c.String(maxLength: 64, storeType: "nvarchar"),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        TenantId = c.Int(),
                        EmailAddress = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Surname = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        EmailConfirmationCode = c.String(maxLength: 328, storeType: "nvarchar"),
                        PasswordResetCode = c.String(maxLength: 328, storeType: "nvarchar"),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        AccessFailedCount = c.Int(nullable: false),
                        IsLockoutEnabled = c.Boolean(nullable: false),
                        PhoneNumber = c.String(maxLength: 32, storeType: "nvarchar"),
                        IsPhoneNumberConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(maxLength: 128, storeType: "nvarchar"),
                        IsTwoFactorEnabled = c.Boolean(nullable: false),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.TenantId)
                .Index(t => t.IsDeleted)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUserClaims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(maxLength: 256, storeType: "nvarchar"),
                        ClaimValue = c.String(unicode: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserClaim_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLogin_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        Value = c.String(maxLength: 2000, storeType: "nvarchar"),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Setting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.UserId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AbpTenantNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        NotificationName = c.String(nullable: false, maxLength: 96, storeType: "nvarchar"),
                        Data = c.String(unicode: false),
                        DataTypeName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityTypeName = c.String(maxLength: 250, storeType: "nvarchar"),
                        EntityTypeAssemblyQualifiedName = c.String(maxLength: 512, storeType: "nvarchar"),
                        EntityId = c.String(maxLength: 96, storeType: "nvarchar"),
                        Severity = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId);
            
            CreateTable(
                "dbo.AbpTenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomCssId = c.Guid(),
                        LogoId = c.Guid(),
                        LogoFileType = c.String(maxLength: 64, storeType: "nvarchar"),
                        EditionId = c.Int(),
                        TenancyName = c.String(nullable: false, maxLength: 64, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ConnectionString = c.String(maxLength: 1024, storeType: "nvarchar"),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("dbo.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("dbo.AbpEditions", t => t.EditionId)
                .ForeignKey("dbo.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.EditionId)
                .Index(t => t.IsDeleted)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.AbpUserAccounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        UserLinkId = c.Long(),
                        UserName = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailAddress = c.String(maxLength: 256, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AbpUserLoginAttempts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        TenancyName = c.String(maxLength: 64, storeType: "nvarchar"),
                        UserId = c.Long(),
                        UserNameOrEmailAddress = c.String(maxLength: 255, storeType: "nvarchar"),
                        ClientIpAddress = c.String(maxLength: 64, storeType: "nvarchar"),
                        ClientName = c.String(maxLength: 128, storeType: "nvarchar"),
                        BrowserInfo = c.String(maxLength: 512, storeType: "nvarchar"),
                        Result = c.Byte(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLoginAttempt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId)
                .Index(t => new { t.UserId, t.TenantId })
                .Index(t => new { t.TenancyName, t.UserNameOrEmailAddress, t.Result });
            
            CreateTable(
                "dbo.AbpUserNotifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        TenantNotificationId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId)
                .Index(t => new { t.UserId, t.State, t.CreationTime });
            
            CreateTable(
                "dbo.AbpUserOrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserOrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TenantId)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.AudioPlaySetAudioDevices",
                c => new
                    {
                        AudioPlaySet_Id = c.Int(nullable: false),
                        AudioDevice_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AudioPlaySet_Id, t.AudioDevice_Id })
                .ForeignKey("dbo.AudioPlaySets", t => t.AudioPlaySet_Id, cascadeDelete: true)
                .ForeignKey("dbo.AudioDevices", t => t.AudioDevice_Id, cascadeDelete: true)
                .Index(t => t.AudioPlaySet_Id)
                .Index(t => t.AudioDevice_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpTenants", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.AbpTenants", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpTenants", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "RoleId", "dbo.AbpRoles");
            DropForeignKey("dbo.AbpRoles", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpRoles", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpSettings", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserRoles", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpPermissions", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserLogins", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "LastModifierUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "DeleterUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUsers", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpUserClaims", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.AbpOrganizationUnits", "ParentId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.AbpEntityChanges", "EntityChangeSetId", "dbo.AbpEntityChangeSets");
            DropForeignKey("dbo.AbpEntityPropertyChanges", "EntityChangeId", "dbo.AbpEntityChanges");
            DropForeignKey("dbo.AbpFeatures", "EditionId", "dbo.AbpEditions");
            DropForeignKey("dbo.AudioTurnPlates", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioReasons", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioMinites", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioHours", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioGates", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioDigits", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioPlaySetTemples", "TempleId", "dbo.AudioTempltes");
            DropForeignKey("dbo.AudioTemplteDetails", "TemplteId", "dbo.AudioTempltes");
            DropForeignKey("dbo.AudioTemplteDetails", "ConstId", "dbo.AudioConsts");
            DropForeignKey("dbo.AudioTempltes", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioPlaySetTemples", "PlayId", "dbo.AudioPlaySets");
            DropForeignKey("dbo.TopPwrPorts", "AudioPlaySet_Id1", "dbo.AudioPlaySets");
            DropForeignKey("dbo.TopPwrPorts", "AudioPlaySet_Id", "dbo.AudioPlaySets");
            DropForeignKey("dbo.AudioPlaySets", "TopPwrPort_Id", "dbo.TopPwrPorts");
            DropForeignKey("dbo.AudioPlaySetAudioDevices", "AudioDevice_Id", "dbo.AudioDevices");
            DropForeignKey("dbo.AudioPlaySetAudioDevices", "AudioPlaySet_Id", "dbo.AudioPlaySets");
            DropForeignKey("dbo.AudioConsts", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioCities", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioCheckIns", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AudioAirLines", "LanguageId", "dbo.AudioLanguages");
            DropForeignKey("dbo.AirshowDatas", "Rid", "dbo.ReceiveJsons");
            DropForeignKey("dbo.CommAudioFileNames", "AirshowDataId", "dbo.AirshowDatas");
            DropIndex("dbo.AudioPlaySetAudioDevices", new[] { "AudioDevice_Id" });
            DropIndex("dbo.AudioPlaySetAudioDevices", new[] { "AudioPlaySet_Id" });
            DropIndex("dbo.AbpUserOrganizationUnits", new[] { "IsDeleted" });
            DropIndex("dbo.AbpUserOrganizationUnits", new[] { "TenantId" });
            DropIndex("dbo.AbpUserNotifications", new[] { "UserId", "State", "CreationTime" });
            DropIndex("dbo.AbpUserNotifications", new[] { "TenantId" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "UserId", "TenantId" });
            DropIndex("dbo.AbpUserLoginAttempts", new[] { "TenantId" });
            DropIndex("dbo.AbpUserAccounts", new[] { "IsDeleted" });
            DropIndex("dbo.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpTenants", new[] { "IsDeleted" });
            DropIndex("dbo.AbpTenants", new[] { "EditionId" });
            DropIndex("dbo.AbpTenantNotifications", new[] { "TenantId" });
            DropIndex("dbo.AbpSettings", new[] { "UserId" });
            DropIndex("dbo.AbpSettings", new[] { "TenantId" });
            DropIndex("dbo.AbpUserRoles", new[] { "UserId" });
            DropIndex("dbo.AbpUserRoles", new[] { "TenantId" });
            DropIndex("dbo.AbpUserLogins", new[] { "UserId" });
            DropIndex("dbo.AbpUserLogins", new[] { "TenantId" });
            DropIndex("dbo.AbpUserClaims", new[] { "UserId" });
            DropIndex("dbo.AbpUserClaims", new[] { "TenantId" });
            DropIndex("dbo.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpUsers", new[] { "IsDeleted" });
            DropIndex("dbo.AbpUsers", new[] { "TenantId" });
            DropIndex("dbo.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("dbo.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("dbo.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("dbo.AbpRoles", new[] { "IsDeleted" });
            DropIndex("dbo.AbpRoles", new[] { "TenantId" });
            DropIndex("dbo.AbpPermissions", new[] { "UserId" });
            DropIndex("dbo.AbpPermissions", new[] { "RoleId" });
            DropIndex("dbo.AbpPermissions", new[] { "TenantId" });
            DropIndex("dbo.AbpOrganizationUnits", new[] { "IsDeleted" });
            DropIndex("dbo.AbpOrganizationUnits", new[] { "ParentId" });
            DropIndex("dbo.AbpOrganizationUnits", new[] { "TenantId" });
            DropIndex("dbo.AbpOrganizationUnitRoles", new[] { "TenantId" });
            DropIndex("dbo.AbpNotificationSubscriptions", new[] { "NotificationName", "EntityTypeName", "EntityId", "UserId" });
            DropIndex("dbo.AbpNotificationSubscriptions", new[] { "TenantId" });
            DropIndex("dbo.AbpLanguageTexts", new[] { "TenantId" });
            DropIndex("dbo.AbpLanguages", new[] { "IsDeleted" });
            DropIndex("dbo.AbpLanguages", new[] { "TenantId" });
            DropIndex("dbo.AppFriendships", new[] { "TenantId" });
            DropIndex("dbo.AbpEntityChangeSets", "IX_TenantId_UserId");
            DropIndex("dbo.AbpEntityChangeSets", new[] { "TenantId" });
            DropIndex("dbo.AbpEntityChangeSets", new[] { "TenantId", "Reason" });
            DropIndex("dbo.AbpEntityChangeSets", "IX_TenantId_CreationTime");
            DropIndex("dbo.AbpEntityPropertyChanges", new[] { "TenantId" });
            DropIndex("dbo.AbpEntityPropertyChanges", new[] { "EntityChangeId" });
            DropIndex("dbo.AbpEntityChanges", new[] { "TenantId" });
            DropIndex("dbo.AbpEntityChanges", new[] { "EntityTypeFullName", "EntityId" });
            DropIndex("dbo.AbpEntityChanges", new[] { "EntityChangeSetId" });
            DropIndex("dbo.AbpEditions", new[] { "IsDeleted" });
            DropIndex("dbo.AbpFeatures", new[] { "EditionId" });
            DropIndex("dbo.AbpFeatures", new[] { "TenantId" });
            DropIndex("dbo.AppChatMessages", new[] { "TenantId" });
            DropIndex("dbo.AppBinaryObjects", new[] { "TenantId" });
            DropIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            DropIndex("dbo.AbpAuditLogs", new[] { "TenantId" });
            DropIndex("dbo.AudioTurnPlates", new[] { "IsDeleted" });
            DropIndex("dbo.AudioTurnPlates", new[] { "LanguageId" });
            DropIndex("dbo.AudioReasons", new[] { "IsDeleted" });
            DropIndex("dbo.AudioReasons", new[] { "LanguageId" });
            DropIndex("dbo.AudioMinites", new[] { "IsDeleted" });
            DropIndex("dbo.AudioMinites", new[] { "LanguageId" });
            DropIndex("dbo.AudioHours", new[] { "IsDeleted" });
            DropIndex("dbo.AudioHours", new[] { "LanguageId" });
            DropIndex("dbo.AudioGates", new[] { "IsDeleted" });
            DropIndex("dbo.AudioGates", new[] { "LanguageId" });
            DropIndex("dbo.AudioDigits", new[] { "IsDeleted" });
            DropIndex("dbo.AudioDigits", new[] { "LanguageId" });
            DropIndex("dbo.AudioTemplteDetails", new[] { "IsDeleted" });
            DropIndex("dbo.AudioTemplteDetails", new[] { "TemplteId" });
            DropIndex("dbo.AudioTemplteDetails", new[] { "ConstId" });
            DropIndex("dbo.AudioTempltes", new[] { "IsDeleted" });
            DropIndex("dbo.AudioTempltes", new[] { "LanguageId" });
            DropIndex("dbo.AudioPlaySetTemples", new[] { "IsDeleted" });
            DropIndex("dbo.AudioPlaySetTemples", new[] { "PlayId" });
            DropIndex("dbo.AudioPlaySetTemples", new[] { "TempleId" });
            DropIndex("dbo.TopPwrPorts", new[] { "AudioPlaySet_Id1" });
            DropIndex("dbo.TopPwrPorts", new[] { "AudioPlaySet_Id" });
            DropIndex("dbo.AudioPlaySets", new[] { "TopPwrPort_Id" });
            DropIndex("dbo.AudioPlaySets", new[] { "IsDeleted" });
            DropIndex("dbo.AudioConsts", new[] { "IsDeleted" });
            DropIndex("dbo.AudioConsts", new[] { "LanguageId" });
            DropIndex("dbo.AudioCities", new[] { "IsDeleted" });
            DropIndex("dbo.AudioCities", new[] { "LanguageId" });
            DropIndex("dbo.AudioCheckIns", new[] { "IsDeleted" });
            DropIndex("dbo.AudioCheckIns", new[] { "LanguageId" });
            DropIndex("dbo.AudioLanguages", new[] { "IsDeleted" });
            DropIndex("dbo.AudioAirLines", new[] { "IsDeleted" });
            DropIndex("dbo.AudioAirLines", new[] { "LanguageId" });
            DropIndex("dbo.CommAudioFileNames", new[] { "AirshowDataId" });
            DropIndex("dbo.AirshowDatas", new[] { "Rid" });
            DropTable("dbo.AudioPlaySetAudioDevices");
            DropTable("dbo.AbpUserOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserOrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserOrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserNotifications",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserLoginAttempts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLoginAttempt_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserAccounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserAccount_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpTenantNotifications",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantNotificationInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpSettings",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Setting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserLogins",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserLogin_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUserClaims",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserClaim_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpPermissions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RolePermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserPermissionSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOrganizationUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_OrganizationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpOrganizationUnitRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_OrganizationUnitRole_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotificationSubscriptions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotificationSubscriptionInfo_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpNotifications");
            DropTable("dbo.AbpLanguageTexts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguageText_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpLanguages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApplicationLanguage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApplicationLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AppFriendships",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Friendship_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEntityChangeSets",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EntityChangeSet_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEntityPropertyChanges",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EntityPropertyChange_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEntityChanges",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EntityChange_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpEditions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Edition_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpFeatures",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EditionFeatureSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FeatureSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TenantFeatureSetting_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AppChatMessages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChatMessage_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AppBinaryObjects",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BinaryObject_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AbpBackgroundJobs");
            DropTable("dbo.AbpAuditLogs",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AuditLog_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioTurnPlates",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioTurnPlate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioReasons",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioReason_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioMinites",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioMinite_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioHours",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioHour_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioGates",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioGate_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioDigits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioDigit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioTemplteDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioTemplteDetail_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioTempltes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioTemplte_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioPlaySetTemples",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioPlaySetTemple_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.TopPwrPorts");
            DropTable("dbo.AudioPlaySets",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioPlaySet_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioDevices");
            DropTable("dbo.AudioConsts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioConst_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioCities",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioCity_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioCheckIns",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioCheckIn_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioLanguages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioLanguage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AudioAirLines",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AudioAirLine_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ReceiveJsons");
            DropTable("dbo.CommAudioFileNames");
            DropTable("dbo.AirshowDatas");
        }
    }
}
