(function () {
    appModule.controller('common.views.audioSets.playSet.detail', [
        '$scope', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'abp.services.app.audioPlaySet',
        'entityId', 'abp.services.app.commonLookup',
        function ($scope, $uibModal, $uibModalInstance, uiGridConstants, audioPlaySet, entityId, commonLookup) {
            var vm = this;
            vm.languages = []; 
             
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
               
            vm.options = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,              
                appScopeProvider: vm,
                columnDefs: [
                     {
                        name: '操作',
                        enableSorting: false,
                        width: 80,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\" >' +
                            '    <button class="btn btn-xs btn-info"  ng-click="grid.appScope.delete(row.entity)"><i class="fa fa-bars"></i>删除</button>' +
                            '</div>'
                    }, 
                    {
                        name: '语种',
                        field: 'audioLanguageName',
                        minWidth: 100
                    },             
                    {
                        name: '类型编号',
                        field: 'type',
                        minWidth: 80
                    },  
                    {
                        name: '内容',
                        field: 'content',
                        minWidth: 200
                    },
                    {
                        name: '排序',
                        field: 'sort',
                        minWidth: 80
                    }


                ] ,
                data: []
            };
             
            vm.cancel = function () {
                $uibModalInstance.close();
              //  $uibModalInstance.dismiss();

            };

            vm.addParamter = function () {

                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/audioSets/playSet/addTemple.cshtml',
                    controller: 'common.views.audioSets.playSet.addTemple as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        entityId: function () {
                            return entityId;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.init();
                });
            };

        


            vm.delete = function (entity) {
                var msg = "";
                abp.message.confirm(
                    msg,
                    "确定删除？",
                    function (isConfirmed) {
                        if (isConfirmed) {
                            audioPlaySet.deleteTempleList({
                                id: entity.tid
                            }).then(function () {
                                vm.init();
                                abp.notify.success(app.localize('SuccessfullyDeleted'));
                            });
                        }
                    }
                );
            };


            vm.init = function () {
                if (entityId) {
                    audioPlaySet.getTempleList({
                        id: entityId
                    }).then(function (result) {
                        vm.entity = result.data;
                        vm.options.data = vm.entity;
                    });
                }
            };
             
            vm.init();
        }
    ]);
})();