(function () {
    appModule.controller('common.views.baseinfo.audioTemplte.detail', [
        '$scope', '$uibModal', '$uibModalInstance', 'uiGridConstants', 'abp.services.app.audioTemplte',
        'entityId', 'abp.services.app.commonLookup',
        function ($scope, $uibModal, $uibModalInstance, uiGridConstants, audioTemplte, entityId, commonLookup) {
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
                        name: '变量/固定语句', 
                        minWidth: 80,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span ng-show="row.entity.isParamter" class="label label-success">变量</span>' +
                            '  <span ng-show="!row.entity.isParamter" class="label label-default">固定语句</span>' +
                            '</div>'
                    },                    
                    {
                        name: '常用语句',
                        field: 'audioConst.content',
                        minWidth: 180,
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span ng-show="row.entity.isParamter" >【{{row.entity.paramterType}}】</span>' +
                            '  <span ng-show="!row.entity.isParamter" >{{row.entity.audioConst.content}}</span>' +
                            '</div>'
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
                    templateUrl: '~/App/common/views/baseinfo/audioTemplte/createOrEditDetail.cshtml',
                    controller: 'common.views.baseinfo.audioTemplte.createOrEditDetail as vm',
                    backdrop: 'static',
                    size: 'md',
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

            vm.addConst = function () {

                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/common/views/baseinfo/audioTemplte/addConst.cshtml',
                    controller: 'common.views.baseinfo.audioTemplte.addConst as vm',
                    backdrop: 'static',
                    size: 'md',
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


            vm.delete = function (detailId) {
                var msg = "";
                abp.message.confirm(
                    msg,
                    "确定删除？",
                    function (isConfirmed) {
                        if (isConfirmed) {
                            audioTemplte.deteleDetail({
                                id: entityId,
                                did: detailId.id
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
                    audioTemplte.get({
                        id: entityId
                    }).then(function (result) {
                        vm.entity = result.data;
                        vm.options.data = vm.entity.details;
                    });
                }
            };
             
            vm.init();
        }
    ]);
})();