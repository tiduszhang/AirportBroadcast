(function () {
    appModule.controller('tenant.views.dashboard.index', [
        '$scope', 'abp.services.app.receiveJson', 'abp.services.app.commAudioTemple', '$state', '$uibModal', 'uiGridConstants',
        function ($scope, receiveJson, commAudioTemple, $state, $uibModal, uiGridConstants) {
            var vm = this;
            vm.editSchd = false; 
             
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
                vm.loading = false;
                vm.tabSelected = function (url) {
                    vm.editSchd = false; 
                    $state.go(url);
                };
            }); 
            vm.optionsArr = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableGridMenu: false,
                showGridFooter: false,
                appScopeProvider: vm,
                columnDefs: [              
                    {
                        name: '进港航班',
                        field: 'flightNo2',
                        enableColumnMenu: false,
                        minWidth: 75
                    },
                    {
                        name: '计落',
                        field: 'arrPlanTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '预落',
                        field: 'arrForecastTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '实落',
                        field: 'arriveTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '起场',
                        field: 'forgNo4',
                        enableColumnMenu: false,
                        minWidth: 60
                    },

                    //{
                    //    name: '经停',
                    //    field: 'festNo4',
                    //    enableColumnMenu: false,
                    //    minWidth: 60
                    //},
                    {
                        name: '状态',
                        enableColumnMenu: false,
                        field: 'flightStatus_Cn',
                        minWidth: 60
                    },
                    {
                        name: '流转状态',
                        enableColumnMenu: false,
                        field: 'flightCirculationStatus_Cn',
                        minWidth: 60
                    },
                    {
                        name: '行李转盘',
                        enableColumnMenu: false,
                        field: 'carousel',
                        minWidth: 60
                    },
                    {
                        name: '提取时间',
                        enableColumnMenu: false,
                        field: 'xlxjtimeStr',
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '延误时间',
                        enableColumnMenu: false,
                        field: 'dlytime',
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '延误原因',
                        enableColumnMenu: false,
                        field: 'flightAbnormalReason',
                        minWidth: 200
                    }
                   
                ],
                data: []
            };
              
            vm.optionsDep = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableGridMenu: false,
                showGridFooter: false,
                appScopeProvider: vm,
                columnDefs: [                   
                    {
                        name: '出港航班',
                        field: 'flightNo2',
                        enableColumnMenu: false,
                        minWidth: 75
                    },
                    {
                        name: '计起',
                        field: 'depPlanTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '预起',
                        field: 'depForecastTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '实起',
                        field: 'departTime',
                        enableColumnMenu: false,
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '落场',
                        field: 'festNo4',
                        minWidth: 60
                    },
                    //{
                    //    name: '经停',
                    //    field: 'fiv1No4',
                    //    minWidth: 60
                    //},
                    {
                        name: '状态',
                        field: 'flightStatus_Cn',
                        minWidth: 60
                    }, {
                        name: '流转状态',
                        enableColumnMenu: false,
                        field: 'flightCirculationStatus_Cn',
                        minWidth: 60
                    },
                    {
                        name: '值机柜台',
                        field: 'checkinCounter',
                        minWidth: 60
                    },
                    {
                        name: '值机开始时间',
                        field: 'checkinTimeStart',
                        minWidth:60,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    }, {
                        name: '登机口',
                        field: 'gate',
                        minWidth: 80
                    },
                    {
                        name: '登机时间',
                        field: 'gateopentime',
                        minWidth: 60,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '延误时间',
                        enableColumnMenu: false,
                        field: 'dlytime',
                        minWidth: 50,
                        cellFilter: 'momentFormat: \'HH:mm\''
                    },
                    {
                        name: '延误原因',
                        enableColumnMenu: false,
                        field: 'flightAbnormalReason',
                        minWidth: 200
                    }
                ],
                data: []
            };

            vm.options = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                appScopeProvider: vm,
                columnDefs: [
                    {
                        name: '等待/开始播放时间',
                        field: 'startPlayTime',
                        width: 160,
                        cellFilter: 'date: \'yyyy-MM-dd HH:mm\''
                    },
                    {
                        name: '播放状态',
                        width: 160,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span ng-show="row.entity.playStatus==\'等待播放\'" class="label label-success">{{row.entity.playStatus}}</span>' +
                            '  <span ng-show="row.entity.playStatus==\'开始播放\'" class="label label-danger">{{row.entity.playStatus}}</span>' +
                            '  <span ng-show="row.entity.playStatus==\'播放完成\'" class="label label-default">{{row.entity.playStatus}}</span>' +
                            '</div>'
                    },
                    {
                        name: '播放内容',
                        field: 'remark',
                        minWidth: 180
                    }

                ],
                data: []
            };
             
            vm.UnAutoPlay = function () {   
                
                vm.editSchd = !vm.editSchd;
                abp.event.trigger('app.playaudio.UnAutoPlay', vm.editSchd );  
            };

            vm.HandPlay = function () {
                vm.port.p2 = !vm.port.p2;
            };

            vm.ClearPlayQuenue = function () {
                vm.loading = true; 
                commAudioTemple.clearPlayQuenue()
                    .then(function (result) { 
                        vm.getall();
                    }).finally(function () {
                        vm.loading = false;
                    });


            };

            vm.port = {
                p1: false,
                p2: false,
                p3: false,
                p4: false,
                p5: false,
                p6: false,
                p7: false,
                p8: false
            };

            vm.getimgurl = function (isplay) {
                return isplay ? "~/Common/Images/boradcast1.png" : "~/Common/Images/boradcast2.png";
            };

            abp.event.on('app.playaudio.RefreshNowPlayAudio', function (message) {
                console.log("sinalr RefreshNowPlayAudio rc:" + message);
                vm.port = message;
                vm.getall();
            }); 

            abp.event.on('app.playaudio.RefreshData', function (message) {
                console.log("sinalr RefreshData:" + message);
                vm.getall();
            });
             
            vm.getall = function () {
                vm.loading = true; 
                receiveJson.getAllPlaying()
                    .then(function (result) { 
                        vm.options.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
                 
                receiveJson.getAllArr()
                    .then(function (result) {
                        vm.optionsArr.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
                 
                receiveJson.getAllDep()
                    .then(function (result) {
                        vm.optionsDep.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });

            };
            vm.getall();
           
        }
    ]);
})();