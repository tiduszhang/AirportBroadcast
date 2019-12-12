(function () {
    //设备管理
    appModule.controller('common.views.play.broadlist.dep', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.audioGate',
        'abp.services.app.commonLookup',
        function ($scope, $uibModal, $stateParams, uiGridConstants, audioGate, commonLookup) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.requestParams = {
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
            };

       //起飞
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
                        width: 80,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                            '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span></button>' +
                            '    <ul uib-dropdown-menu>' +
                            '      <li><a  ng-click="grid.appScope.edit(row.entity)">修改</a></li>' +
                            '      <li><a ng-click="grid.appScope.delete(row.entity)">删除</a></li>' +
                            '    </ul>' +
                            '  </div>' +
                            '</div>'
                    },
                    {
                        name: '航班号',
                        field: 'audioLanguageName',
                        minWidth: 100
                    },
                    {
                        name: '预计起飞时间',
                        enableSorting: false,
                        minWidth: 80,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '    <button class="btn btn-xs btn-info"  ng-click="grid.appScope.play(row.entity)"><i class="fa fa-play-circle"></i>播放</button>' +
                            '</div>'
                    },
                    {
                        name: '目的站',
                        field: 'code',
                        minWidth: 80
                    },

                    {
                        name: '经停站',
                        field: 'fileName',
                        minWidth: 120
                    },
                    {
                        name: '值机柜台',
                        field: 'path',
                        minWidth: 140
                    },
                    {
                        name: '值机开始时间',
                        field: 'content',
                        minWidth: 200
                    }, {
                        name: '登机口',
                        field: 'contentRemark',
                        minWidth: 200
                    },
                    {
                        name: '登机时间',
                        field: 'remark',
                        minWidth: 200
                    },
                    {
                        name: '航班状态',
                        field: 'remark',
                        minWidth: 200
                    },
                    {
                        name: '延误原因',
                        field: 'remark',
                        minWidth: 200
                    },
                    {
                        name: '延误时间',
                        field: 'remark',
                        minWidth: 200
                    }

                ],
                data: []
            };



            vm.getAll = function () {
                vm.loading = true;
                audioGate.getAll(vm.requestParams)
                    .then(function (result) {
                        vm.options.data = result.data.items;
                        vm.options.totalItems = result.data.totalCount;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.edit = function (entity) {
                openCreateOrEditModal(entity.id);
            };

            vm.create = function () { 
                openCreateOrEditModal(null);
            };

            vm.delete = function (entity) {
              
                var msg = "ID：" + entity.id + "，名称：" + entity.name;
                abp.message.confirm(
                    msg,
                    "确定删除？",
                    function (isConfirmed) {
                        if (isConfirmed) {
                            audioGate.delete({
                                id: entity.id
                            }).then(function () {
                                vm.getAll();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
            }; 

            vm.play = function (entity) {

                commonLookup.checkFileIsExist({ path: entity.path, fileName: entity.fileName }).then(function (data) {
                    var a = document.getElementById('audio');
                    a.src = '/Content/Audios/' + entity.path + entity.fileName;

                    //  "/Content/Audios/11750.wav";
                    a.play();
                });
                 
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

            vm.init = function() {
                commonLookup.getAudioLanguageForCombobox().then(function (result) {
                    vm.languages = result.data.items;
                });
                vm.getAll();
            };
            vm.init();
             
        }]);
})();