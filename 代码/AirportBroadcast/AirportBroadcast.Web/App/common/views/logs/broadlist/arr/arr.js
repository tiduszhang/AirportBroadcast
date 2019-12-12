(function () {
    //设备管理
    appModule.controller('common.views.play.broadlist.arr', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.receiveJson',
        function ($scope, $uibModal, $stateParams, uiGridConstants, receiveJson) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.playingid = 0;

            //到达
            vm.options = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                appScopeProvider: vm,
                columnDefs: [
                    {
                        name: app.localize('Actions'),
                        enableSorting: false,
                        width: 150,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +                           
                            '    <button class="btn btn-xs " ng-class="{\'btn-primary\':row.entity.id!=grid.appScope.playingid,\'btn-danger\':row.entity.id==grid.appScope.playingid}"  ng-click="grid.appScope.playlocal(row.entity)"><i class="fa fa-play-circle"></i>试听<span ng-if="row.entity.id==grid.appScope.playingid">（播放中...）</span></span></button>' +                   
                            '</div>'
                    },
                    {
                        name: '航班号',
                        field: 'flightNo2',
                        minWidth: 100
                    },
                    {
                        name: '预计到达时间',
                        field: 'arrForecastTimeStr',
                        minWidth: 140,
                        DataFormat: 'yyyy-mm-dd'

                    },
                    {
                        name: '始发站',
                        field: 'forgNo4',
                        minWidth: 80

                    },

                    {
                        name: '经停站',
                        field: 'festNo4',
                        minWidth: 120
                    },
                    {
                        name: '行李转盘',
                        field: 'carousel',
                        minWidth: 140
                    },
                    {
                        name: '提取时间',
                        field: 'xlxjtimeStr',
                        minWidth: 140
                    }, {
                        name: '航班状态',
                        field: 'flightStatus',
                        minWidth: 80
                    },
                    {
                        name: '延误原因',
                        field: 'flightAbnormalReason',
                        minWidth: 200
                    },
                    {
                        name: '延误时间',
                        field: 'dlytimeStr',
                        minWidth: 140
                    }
                ],
                data: []
            };
             
            vm.getAll = function () {
                vm.loading = true;
                receiveJson.getAllArr()
                    .then(function (result) {
                        vm.options.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };
             
            vm.playlocal = function (entity) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/play/broadlist/playing.cshtml',
                    controller: 'common.views.play.broadlist.playing as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        entityId: function () {
                            return entity.id;
                        }
                    }
                });

                modalInstance.result.then(function (result) {

                });

                //commonLookup.checkFileIsExist({ path: entity.path, fileName: entity.fileName }).then(function (data) {
                //    var a = document.getElementById('audio');
                //    a.src = '/Content/Audios/' + entity.path + entity.fileName;

                //    a.play();
                //});

            };

            function openCreateOrEditModal(entityId) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/baseinfo/audioGate/createOrEdit.cshtml',
                    controller: 'common.views.baseinfo.audioGate.createOrEdit as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        entityId: function () {
                            return entityId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getAll();
                });
            }

            vm.init = function () {
                vm.getAll();
            };
            vm.init();

        }]);
})();