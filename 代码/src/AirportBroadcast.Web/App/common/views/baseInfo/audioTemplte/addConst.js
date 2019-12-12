﻿(function () {
    appModule.controller('common.views.baseinfo.audioTemplte.addConst', [
        '$scope', '$uibModalInstance', 'abp.services.app.audioTemplte',  'entityId',
        function ($scope, $uibModalInstance, audioTemplte, entityId) {
            var vm = this;

            vm.loading = false;

            vm.entity = {
                id: entityId,
                detail: {
                    isParamter: false,
                    sort:999
                }
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.save = function () {
                var rows = vm.gridApi.selection.getSelectedRows();
                if (rows.length === 0) {
                    abp.message.warn("请选择一条语句");
                    return;
                }

                if (!vm.entity.detail.sort) {
                    abp.message.warn("请设置排序");
                    return;
                }

                console.log(rows[0]);
                vm.entity.detail.constId = rows[0].id;

                vm.loading = true;
                audioTemplte.updateDetail(vm.entity).then(function (result) {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    $uibModalInstance.close();
                }).finally(function () {
                    vm.loading = false;
                });
            };

            vm.gridOptions = {
                appScopeProvider: vm,
                showGridFooter: true,//时候显示表格的footer  
                enableFiltering: true,              
                enableHorizontalScrollbar: 1,//表格的水平滚动条  
                enableVerticalScrollbar: 1,//表格的垂直滚动条 (两个都是 1-显示,0-不显示)  
                enableFooterTotalSelected: false, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true  
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableFullRowSelection: true, 
                modifierKeysToMultiSelect: false,
                multiSelect: false,// 是否可以选择多个,默认为true;
                columnDefs: [
                    {
                        name: '语种',
                        field: 'audioLanguageName',
                        enableCellEdit: false
                    },
                    {
                        name: '编号',
                        field: 'constNo',
                        enableCellEdit: false
                    },
                    {
                        name: '文件名',
                        field: 'fileName',
                        enableCellEdit: false
                    },
                    {
                        name: '语音文字',
                        field: 'content',
                        enableCellEdit: false
                    } 
                ],
                onRegisterApi: function (gridApi) {
                    vm.gridApi = gridApi;
                    vm.gridApi.grid.registerRowsProcessor(vm.singleFilter, 200);
                },
                data: []
            };
            vm.singleFilter = function (renderableRows) {
                var matcher = new RegExp(vm.filterValue);
                renderableRows.forEach(function (row) {
                    var match = false;
                    ['audioLanguageName', 'constNo', 'fileName', 'content'].forEach(function (field) {
                        if (row.entity[field].match(matcher)) {
                            match = true;
                        }
                    });
                    if (!match) {
                        row.visible = false;
                    }
                });
                return renderableRows;
            };
            vm.filter = function () {
                vm.gridApi.grid.refresh();
            };
            vm.selectItem = function (item) {
                $uibModalInstance.close(item);
            };

            vm.refreshGrid = function () {
                vm.loading = true;
                audioTemplte.getAllAudioConst()
                    .then(function (result) { 
                        vm.gridOptions.data = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.refreshGrid();
        }
    ]);
})();